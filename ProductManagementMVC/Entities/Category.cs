using ProductManagementMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementMVC.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public CodeStatus Status { get; set; }
        public Subject Subject { get; set; }
        public string Image { get; set; }
    }
}
