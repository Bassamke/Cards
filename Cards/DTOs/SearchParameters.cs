using Cards.Validators;

namespace Cards.DTOs
{
    public class SearchParameters
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        [StatusValidator("Done", "In Progress", "To Do")]
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
    }
}
