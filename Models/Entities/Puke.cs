using System.ComponentModel.DataAnnotations;

namespace PukesMVC.Models.Entities
{
    public class Puke
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Notes { get; set; }

        //public virtual State State { get; set; }
    }
}
