namespace FSH.WebApi.Application.Catalog.Activities;

public class UpdateActivityRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? City { get; set; }
    public string? Venue { get; set; }
}

public class UpdateActivityRequestValidator : CustomValidator<UpdateActivityRequest>
{
    public UpdateActivityRequestValidator(IReadRepository<Activity> repository, IStringLocalizer<UpdateActivityRequestValidator> T)
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

public class UpdateActivityRequestHandler : IRequestHandler<UpdateActivityRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Activity> _repository;
    private readonly IStringLocalizer _t;

    public UpdateActivityRequestHandler(IRepositoryWithEvents<Activity> repository, IStringLocalizer<UpdateActivityRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateActivityRequest request, CancellationToken cancellationToken)
    {
        var activity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = activity
        ?? throw new NotFoundException(_t["Activity {0} Not Found.", request.Id]);

        _ = activity.Update(request.Title, request.Description, request.Category, request.City, request.Venue);

        await _repository.UpdateAsync(activity, cancellationToken);

        return request.Id;
    }
}