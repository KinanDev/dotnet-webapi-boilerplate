namespace FSH.WebApi.Application.Catalog.Activities;
public class ActivityDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string? Venue { get; set; }
}
