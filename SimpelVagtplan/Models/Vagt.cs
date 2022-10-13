using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpelVagtplan.Models
{
    public class Vagt
    {
        [Key]
        public int Id { get; set; }
        public Medarbejder Medarbejder { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
