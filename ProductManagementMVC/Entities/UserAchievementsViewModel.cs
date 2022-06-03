using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductManagementMVC.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementMVC.Entities
{
    public class UserAchievementsViewModel
    {

        [Key]
        [Column(TypeName = "nvarchar(450)")]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        [Display(Name = "User")]
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


        [Display(Name = "Quote")]
        public int QuoteId { get; set; }

        public FamousQuoteViewModel Quotes { get; set; }


        [Display(Name = "Answer")]
        public bool Answer { get; set; }

    }
}
