using Cards.Validators;

namespace Cards.DTOs;
public class CardsRequest
{
    [StatusValidator("Name", "Color", "Status", "DateCreated")]
    public string? SortBy { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public SearchParameters? SearchParameters { get; set; }
}
