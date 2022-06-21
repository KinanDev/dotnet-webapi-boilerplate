namespace FSH.WebApi.Application.Catalog.Activities;

public class GetActivityRequest : IRequest<ActivityDto>
{
    public Guid Id { get; set; }
    public GetActivityRequest(Guid id) => Id = id;
}

public class ActivityByIdSpec : Specification<Activity, ActivityDto>, ISingleResultSpecification
{
    public ActivityByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetActivityRequestHandler : IRequestHandler<GetActivityRequest, ActivityDto>
{
    private readonly IRepository<Activity> _repository;
    private readonly IStringLocalizer _t;

    public GetActivityRequestHandler(IRepository<Activity> repository, IStringLocalizer<GetActivityRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<ActivityDto> Handle(GetActivityRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Activity, ActivityDto>)new ActivityByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Activity {0} Not Found.", request.Id]);
}