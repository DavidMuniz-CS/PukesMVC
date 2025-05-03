using System.ComponentModel.DataAnnotations;

namespace PukesMVC.Models.Entities
{
    public class State
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
