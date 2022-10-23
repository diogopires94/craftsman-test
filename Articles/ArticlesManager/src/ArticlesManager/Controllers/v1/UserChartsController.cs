namespace ArticlesManager.Controllers.v1;

using ArticlesManager.Domain.UserCharts.Features;
using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using HeimGuard;

[ApiController]
[Route("api/usercharts")]
[ApiVersion("1.0")]
public class UserChartsController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHeimGuardClient _heimGuard;
    public UserChartsController(IMediator mediator, IHeimGuardClient heimGuard)
    {
        _heimGuard = heimGuard;
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new UserChart record.
    /// </summary>
    /// <response code="201">UserChart created.</response>
    /// <response code="400">UserChart has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the UserChart.</response>
    [ProducesResponseType(typeof(UserChartDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddUserChart")]
    public async Task<ActionResult<UserChartDto>> AddUserChart([FromBody]UserChartForCreationDto userChartForCreation)
    {
        var command = new AddUserChart.AddUserChartCommand(userChartForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetUserChart",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single UserChart by ID.
    /// </summary>
    /// <response code="200">UserChart record returned successfully.</response>
    /// <response code="400">UserChart has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the UserChart.</response>
    [ProducesResponseType(typeof(UserChartDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetUserChart")]
    public async Task<ActionResult<UserChartDto>> GetUserChart(Guid id)
    {
        var query = new GetUserChart.UserChartQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all UserCharts.
    /// </summary>
    /// <response code="200">UserChart list returned successfully.</response>
    /// <response code="400">UserChart has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the UserChart.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<UserChartDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetUserCharts")]
    public async Task<IActionResult> GetUserCharts([FromQuery] UserChartParametersDto userChartParametersDto)
    {
        var query = new GetUserChartList.UserChartListQuery(userChartParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Updates an entire existing UserChart.
    /// </summary>
    /// <response code="204">UserChart updated.</response>
    /// <response code="400">UserChart has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the UserChart.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateUserChart")]
    public async Task<IActionResult> UpdateUserChart(Guid id, UserChartForUpdateDto userChart)
    {
        var command = new UpdateUserChart.UpdateUserChartCommand(id, userChart);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing UserChart record.
    /// </summary>
    /// <response code="204">UserChart deleted.</response>
    /// <response code="400">UserChart has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the UserChart.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteUserChart")]
    public async Task<ActionResult> DeleteUserChart(Guid id)
    {
        var command = new DeleteUserChart.DeleteUserChartCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
