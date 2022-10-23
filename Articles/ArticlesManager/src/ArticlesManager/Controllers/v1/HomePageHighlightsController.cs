namespace ArticlesManager.Controllers.v1;

using ArticlesManager.Domain.HomePageHighlights.Features;
using ArticlesManager.Domain.HomePageHighlights.Dtos;
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
[Route("api/homepagehighlights")]
[ApiVersion("1.0")]
public class HomePageHighlightsController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHeimGuardClient _heimGuard;

    public HomePageHighlightsController(IMediator mediator, IHeimGuardClient heimGuard)
    {
        _heimGuard = heimGuard;
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new HomePageHighlight record.
    /// </summary>
    /// <response code="201">HomePageHighlight created.</response>
    /// <response code="400">HomePageHighlight has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the HomePageHighlight.</response>
    [ProducesResponseType(typeof(HomePageHighlightDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddHomePageHighlight")]
    public async Task<ActionResult<HomePageHighlightDto>> AddHomePageHighlight([FromBody]HomePageHighlightForCreationDto homePageHighlightForCreation)
    {
        var command = new AddHomePageHighlight.AddHomePageHighlightCommand(homePageHighlightForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetHomePageHighlight",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single HomePageHighlight by ID.
    /// </summary>
    /// <response code="200">HomePageHighlight record returned successfully.</response>
    /// <response code="400">HomePageHighlight has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the HomePageHighlight.</response>
    [ProducesResponseType(typeof(HomePageHighlightDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetHomePageHighlight")]
    public async Task<ActionResult<HomePageHighlightDto>> GetHomePageHighlight(Guid id)
    {
        var query = new GetHomePageHighlight.HomePageHighlightQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all HomePageHighlights.
    /// </summary>
    /// <response code="200">HomePageHighlight list returned successfully.</response>
    /// <response code="400">HomePageHighlight has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the HomePageHighlight.</response>
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
    [ProducesResponseType(typeof(IEnumerable<HomePageHighlightDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetHomePageHighlights")]
    public async Task<IActionResult> GetHomePageHighlights([FromQuery] HomePageHighlightParametersDto homePageHighlightParametersDto)
    {
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(homePageHighlightParametersDto);
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
    /// Updates an entire existing HomePageHighlight.
    /// </summary>
    /// <response code="204">HomePageHighlight updated.</response>
    /// <response code="400">HomePageHighlight has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the HomePageHighlight.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateHomePageHighlight")]
    public async Task<IActionResult> UpdateHomePageHighlight(Guid id, HomePageHighlightForUpdateDto homePageHighlight)
    {
        var command = new UpdateHomePageHighlight.UpdateHomePageHighlightCommand(id, homePageHighlight);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing HomePageHighlight record.
    /// </summary>
    /// <response code="204">HomePageHighlight deleted.</response>
    /// <response code="400">HomePageHighlight has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the HomePageHighlight.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteHomePageHighlight")]
    public async Task<ActionResult> DeleteHomePageHighlight(Guid id)
    {
        var command = new DeleteHomePageHighlight.DeleteHomePageHighlightCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
