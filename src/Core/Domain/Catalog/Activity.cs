namespace FSH.WebApi.Domain.Catalog;
public class Activity : AuditableEntity, IAggregateRoot
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string? Venue { get; set; }

    public Activity Update(string title, string description, string category, string city, string venue)
    {
        if (title is not null && Title?.Equals(title) is not true) Title = title;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (category is not null && Category?.Equals(category) is not true) Category = category;
        if (city is not null && City?.Equals(city) is not true) City = city;
        if (venue is not null && Venue?.Equals(venue) is not true) Venue = venue;

        // Date = DateTime.Now;
        return this;
    }
}