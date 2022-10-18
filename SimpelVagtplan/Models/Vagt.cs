namespace SimpelVagtplan.Models
{
    public class Vagt
    {
        public int Id { get; set; }
        public Medarbejder Medarbejder { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
    
}
