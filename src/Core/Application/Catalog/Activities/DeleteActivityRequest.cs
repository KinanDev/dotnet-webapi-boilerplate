using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Activities;

public class DeleteActivityRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteActivityRequest(Guid id) => Id = id;
}

public class DeleteActivityRequestHandler : IRequestHandler<DeleteActivityRequest, Guid>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;

    public DeleteActivityRequestHandler(IRepository<Product> repository, IStringLocalizer<DeleteActivityRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteActivityRequest request, CancellationToken cancellationToken)
    {
        var activity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = activity ?? throw new NotFoundException(_t["Activity {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        activity.DomainEvents.Add(EntityDeletedEvent.WithEntity(activity));

        await _repository.DeleteAsync(activity, cancellationToken);

        return request.Id;
    }
}