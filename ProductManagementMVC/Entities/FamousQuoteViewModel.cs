using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductManagementMVC.Entities
{
    public class FamousQuoteViewModel
    {

        [Key]
        public int QuoteId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        [Display(Name = "Quote")]
        public string FamousQuoteText { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        [Display(Name = "Author")]
        public string FamousQuoteAuthor { get; set; }

        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }

        public List<UserAchievementsViewModel> UserAchievements { get; set; }
    }
}
