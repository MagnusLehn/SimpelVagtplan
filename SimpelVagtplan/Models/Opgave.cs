namespace SimpelVagtplan.Models
{
    public class Opgave
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int AgeLimit { get; set; }
        public Vagt? vagt { get; set; }
    }
}
