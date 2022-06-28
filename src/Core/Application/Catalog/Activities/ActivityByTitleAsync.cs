namespace FSH.WebApi.Application.Catalog.Activities;

public class ActivityByTitleAsync : Specification<Activity>, ISingleResultSpecification
{
    public ActivityByTitleAsync(string title) =>
        Query.Where(a => a.Title.Trim().ToLower() == title.Trim().ToLower());
}
