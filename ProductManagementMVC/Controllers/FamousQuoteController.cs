using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementMVC.Data;
using ProductManagementMVC.Entities;

namespace ProductManagementMVC.Controllers
{
    public class FamousQuoteController : Controller
    {
        private readonly ProductManagementMVCContext _context;

        public FamousQuoteController(ProductManagementMVCContext context)
        {
            _context = context;
        }

        //// GET: FamousQuoteViewModels
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.FamousQuotes.ToListAsync());
        //}

       public async Task<IActionResult> StartQuiz(
       string sortOrder,
       string currentFilter,
       string searchString,
       int? pageNumber)

        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
         
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var quotes = from s in _context.FamousQuotes
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                quotes = quotes.Where(s => s.FamousQuoteText.Contains(searchString)
                                       || s.FamousQuoteAuthor.Contains(searchString));
            }
            switch (sortOrder)
            {

                default:
                    quotes = quotes.OrderBy(s => s.QuoteId);
                    break;
            }

            int pageSize = 1;
            return View(PaginatedList<FamousQuoteViewModel>.Create(quotes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)

        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["QuoteSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Quote_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewData["IsCorrectSortParm"] = sortOrder == "IsCorrect" ? "IsCorrect_desc" : "IsCorrect";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var quotes = from s in _context.FamousQuotes
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                quotes = quotes.Where(s => s.FamousQuoteText.Contains(searchString)
                                       || s.FamousQuoteAuthor.Contains(searchString));
            }
        
            switch (sortOrder)
            {
                case "Quote_desc":
                    quotes = quotes.OrderByDescending(s => s.FamousQuoteText);
                    break;
                case "Author_desc":
                    quotes = quotes.OrderBy(s => s.FamousQuoteAuthor);
                    break;
                case "IsCorrect_desc":
                    quotes = quotes.OrderBy(s => s.IsCorrect);
                    break;
            }


            int pageSize = 10;
            return View(PaginatedList<FamousQuoteViewModel>.Create(quotes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: FamousQuoteViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var famousQuoteViewModel = await _context.FamousQuotes
                .FirstOrDefaultAsync(m => m.QuoteId == id);
            if (famousQuoteViewModel == null)
            {
                return NotFound();
            }

            return View(famousQuoteViewModel);
        }

        // GET: FamousQuoteViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FamousQuoteViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuoteId,FamousQuoteText,FamousQuoteAuthor,IsCorrect")] FamousQuoteViewModel famousQuoteViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(famousQuoteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(famousQuoteViewModel);
        }

        // GET: FamousQuoteViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var famousQuoteViewModel = await _context.FamousQuotes.FindAsync(id);
            if (famousQuoteViewModel == null)
            {
                return NotFound();
            }
            return View(famousQuoteViewModel);
        }

        // POST: FamousQuoteViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuoteId,FamousQuoteText,FamousQuoteAuthor,IsCorrect")] FamousQuoteViewModel famousQuoteViewModel)
        {
            if (id != famousQuoteViewModel.QuoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(famousQuoteViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamousQuoteViewModelExists(famousQuoteViewModel.QuoteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(famousQuoteViewModel);
        }

        // GET: FamousQuoteViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var famousQuoteViewModel = await _context.FamousQuotes
                .FirstOrDefaultAsync(m => m.QuoteId == id);
            if (famousQuoteViewModel == null)
            {
                return NotFound();
            }

            return View(famousQuoteViewModel);
        }

        // POST: FamousQuoteViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var famousQuoteViewModel = await _context.FamousQuotes.FindAsync(id);
            _context.FamousQuotes.Remove(famousQuoteViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool FamousQuoteViewModelExists(int id)
        {
            return _context.FamousQuotes.Any(e => e.QuoteId == id);
        }
    }
}
