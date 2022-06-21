namespace FSH.WebApi.Application.Catalog.Activities;

public class SearchActivitiesRequest : PaginationFilter, IRequest<PaginationResponse<ActivityDto>>
{
}

public class ActivitiesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Activity, ActivityDto>
{
    public ActivitiesBySearchRequestSpec(SearchActivitiesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Title, !request.HasOrderBy());
}

public class SearchActivitiesRequestHandler : IRequestHandler<SearchActivitiesRequest, PaginationResponse<ActivityDto>>
{
    private readonly IReadRepository<Activity> _repository;

    public SearchActivitiesRequestHandler(IReadRepository<Activity> repository) => _repository = repository;

    public async Task<PaginationResponse<ActivityDto>> Handle(SearchActivitiesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ActivitiesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}