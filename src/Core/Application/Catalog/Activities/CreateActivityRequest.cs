using AutoMapper;

namespace FSH.WebApi.Application.Catalog.Activities;

public class CreateActivityRequest : IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? City { get; set; }
    public string? Venue { get; set; }
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

    private readonly IMapper _mapper;

    public CreateActivityRequestHandler(IRepositoryWithEvents<Activity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateActivityRequest request, CancellationToken cancellationToken)
    {
        var activity = _mapper.Map<Activity>(request);

        activity.Date = DateTime.UtcNow;

        await _repository.AddAsync(activity, cancellationToken);

        return activity.Id;
    }
}