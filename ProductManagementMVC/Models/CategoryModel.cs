using ProductManagementMVC.Entities;
using ProductManagementMVC.Enums;

namespace ProductManagementMVC.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public CodeStatus Status { get; set; }
        public Subject Subject { get; set; }
        public string Description { get; set; }
    }
}

