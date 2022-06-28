using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Catalog.Activities;

public class ExportActivitiesRequest : BaseFilter, IRequest<Stream>
{

}

public class ExportActivitiesRequestHandler : IRequestHandler<ExportActivitiesRequest, Stream>
{
    private readonly IReadRepository<Activity> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportActivitiesRequestHandler(IReadRepository<Activity> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportActivitiesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportActivitiesRequestSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportActivitiesRequestSpecification : EntitiesByBaseFilterSpec<Activity, ActivityExportDto>
{
    public ExportActivitiesRequestSpecification(ExportActivitiesRequest request)
        : base(request)
    {
        _ = Query;
    }
}