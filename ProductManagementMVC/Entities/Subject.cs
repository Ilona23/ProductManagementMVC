using System.ComponentModel.DataAnnotations;

namespace ProductManagementMVC.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
