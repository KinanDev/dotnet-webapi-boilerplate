namespace FSH.WebApi.Application.Catalog.Activities;

public class CreateActivityRequest : IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? City { get; set; }
    public string? Veneu { get; set; }
}

public class CreateActivityRequestValidator : CustomValidator<CreateActivityRequest>
{
    public CreateActivityRequestValidator(IReadRepository<Activity> repository, IStringLocalizer<CreateActivityRequestValidator> T)
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ActivityByTitleAsync(name), ct) is null)
                .WithMessage((_, name) => T["Activity {0} already Exists.", name]);

        RuleFor(a => a.Description)
            .MaximumLength(200);
    }
}

public class CreateActivityRequestHandler : IRequestHandler<CreateActivityRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Activity> _repository;

    public CreateActivityRequestHandler(IRepositoryWithEvents<Activity> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateActivityRequest request, CancellationToken cancellationToken)
    {
        var activity = new Activity(request.Title, request.Description, request.Category, request.City, request.Veneu);

        await _repository.AddAsync(activity, cancellationToken);

        return activity.Id;
    }
}