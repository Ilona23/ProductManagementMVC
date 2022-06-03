using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ProductManagementMVC.Areas.Identity.Data;
using ProductManagementMVC.Data;
using ProductManagementMVC.Entities;

namespace ProductManagementMVC.Controllers
{
    public class UserAchievementsController : Controller
    {
        private readonly ProductManagementMVCContext _context;

        public UserAchievementsController(ProductManagementMVCContext context)
        {
            _context = context;
        }

        // GET: UserAchievementsViewModels
        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = _context.ApplicationUsers.ToList();
            List<FamousQuoteViewModel> quotes = _context.FamousQuotes.ToList();
            return View(await _context.UserAchievements.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Answer(UserAchievementsViewModel userAchievementsViewModel, int? pageNumber, int pageSize)
        {
            var userID  = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var quoteID = userAchievementsViewModel.QuoteId;
            var quotes  = from s in _context.FamousQuotes select s;

            IList<UserAchievementsViewModel> existingItems = _context.UserAchievements
                .Where(cm => cm.UserId == userID).Where(cm => cm.QuoteId == quoteID).ToList();

            ApplicationUser applicationUser = _context.ApplicationUsers.Single(c => c.Id == userID);
            FamousQuoteViewModel famousQuoteViewModel = _context.FamousQuotes.Single(m => m.QuoteId == quoteID);

            if (existingItems.Count == 0)
            {               
                UserAchievementsViewModel userAchievements = new UserAchievementsViewModel
                {                 
                    Id = Guid.NewGuid().ToString(), 
                    UserId = userID,
                    QuoteId = quoteID,
                    Answer = userAchievementsViewModel.Answer,
                    ApplicationUser = applicationUser, 
                    Quotes = famousQuoteViewModel,

                };
                    _context.Add(userAchievements);
                    await _context.SaveChangesAsync();

                //await _context.Response.WriteAsync("DivideByZeroException occured!");
              //  return View(await PaginatedList<FamousQuoteViewModel>.CreateAsync(quotes.AsNoTracking(), pageNumber ?? 1, pageSize)); (((

            }
            return new EmptyResult();

           // return View(await PaginatedList<FamousQuoteViewModel>.CreateAsync(quotes.AsNoTracking(), pageNumber ?? 1, pageSize)); (((
            //return RedirectToAction("FamousQuote", "StartQuiz", new { quoteID = famousQuoteViewModel.QuoteId });
            //  return RedirectToAction(nameof(Index));
        }
    }
}