namespace ArticlesManager.Controllers.v1;

using ArticlesManager.Domain.SizeTableLines.Features;
using ArticlesManager.Domain.SizeTableLines.Dtos;
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
[Route("api/sizetablelines")]
[ApiVersion("1.0")]
public class SizeTableLinesController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHeimGuardClient _heimGuard;
    public SizeTableLinesController(IMediator mediator, IHeimGuardClient heimGuard)
    {
        _heimGuard = heimGuard;
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new SizeTableLine record.
    /// </summary>
    /// <response code="201">SizeTableLine created.</response>
    /// <response code="400">SizeTableLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SizeTableLine.</response>
    [ProducesResponseType(typeof(SizeTableLineDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddSizeTableLine")]
    public async Task<ActionResult<SizeTableLineDto>> AddSizeTableLine([FromBody]SizeTableLineForCreationDto sizeTableLineForCreation)
    {
        var command = new AddSizeTableLine.AddSizeTableLineCommand(sizeTableLineForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetSizeTableLine",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single SizeTableLine by ID.
    /// </summary>
    /// <response code="200">SizeTableLine record returned successfully.</response>
    /// <response code="400">SizeTableLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SizeTableLine.</response>
    [ProducesResponseType(typeof(SizeTableLineDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetSizeTableLine")]
    public async Task<ActionResult<SizeTableLineDto>> GetSizeTableLine(Guid id)
    {
        var query = new GetSizeTableLine.SizeTableLineQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all SizeTableLines.
    /// </summary>
    /// <response code="200">SizeTableLine list returned successfully.</response>
    /// <response code="400">SizeTableLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SizeTableLine.</response>
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
    [ProducesResponseType(typeof(IEnumerable<SizeTableLineDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetSizeTableLines")]
    public async Task<IActionResult> GetSizeTableLines([FromQuery] SizeTableLineParametersDto sizeTableLineParametersDto)
    {
        var query = new GetSizeTableLineList.SizeTableLineListQuery(sizeTableLineParametersDto);
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
    /// Updates an entire existing SizeTableLine.
    /// </summary>
    /// <response code="204">SizeTableLine updated.</response>
    /// <response code="400">SizeTableLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SizeTableLine.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateSizeTableLine")]
    public async Task<IActionResult> UpdateSizeTableLine(Guid id, SizeTableLineForUpdateDto sizeTableLine)
    {
        var command = new UpdateSizeTableLine.UpdateSizeTableLineCommand(id, sizeTableLine);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing SizeTableLine record.
    /// </summary>
    /// <response code="204">SizeTableLine deleted.</response>
    /// <response code="400">SizeTableLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the SizeTableLine.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteSizeTableLine")]
    public async Task<ActionResult> DeleteSizeTableLine(Guid id)
    {
        var command = new DeleteSizeTableLine.DeleteSizeTableLineCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
