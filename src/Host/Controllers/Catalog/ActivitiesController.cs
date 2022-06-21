using FSH.WebApi.Application.Catalog.Activities;
using FSH.WebApi.Host.Controllers;

public class ActivitiessController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Activities)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<ActivityDto>> SearchAsync(SearchActivitiesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Activities)]
    [OpenApiOperation("Get activity details.", "")]
    public Task<ActivityDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetActivityRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Activities)]
    [OpenApiOperation("Create a new activity.", "")]
    public Task<Guid> CreateAsync(CreateActivityRequest request)
    {
        return Mediator.Send(request);
    }

    //[HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Activities)]
    //[OpenApiOperation("Update a activity.", "")]
    //public async Task<ActionResult<Guid>> UpdateAsync(UpdateActivityRequest request, Guid id)
    //{
    //    return id != request.Id
    //        ? BadRequest()
    //        : Ok(await Mediator.Send(request));
    //}

    //[HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Activities)]
    //[OpenApiOperation("Delete a activity.", "")]
    //public Task<Guid> DeleteAsync(Guid id)
    //{
    //    return Mediator.Send(new DeleteActivityRequest(id));
    //}
}
