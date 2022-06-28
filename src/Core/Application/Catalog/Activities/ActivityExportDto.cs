namespace FSH.WebApi.Application.Catalog.Activities;
public class ActivityExportDto
{
    public string Title { get; set; } = default!;
    public DateTime Date { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Venue { get; set; } = default!;
}
