namespace ArticlesManager.Controllers.v1;

using ArticlesManager.Domain.Articles.Features;
using ArticlesManager.Domain.Articles.Dtos;
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
[Route("api/articles")]
[ApiVersion("1.0")]
public class ArticlesController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHeimGuardClient _heimGuard;

    public ArticlesController(IMediator mediator, IHeimGuardClient heimGuard)
    {
        _heimGuard = heimGuard;
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Article record.
    /// </summary>
    /// <response code="201">Article created.</response>
    /// <response code="400">Article has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Article.</response>
    [ProducesResponseType(typeof(ArticleDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddArticle")]
    public async Task<ActionResult<ArticleDto>> AddArticle([FromBody]ArticleForCreationDto articleForCreation)
    {
        var command = new AddArticle.AddArticleCommand(articleForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetArticle",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Article by ID.
    /// </summary>
    /// <response code="200">Article record returned successfully.</response>
    /// <response code="400">Article has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Article.</response>
    [ProducesResponseType(typeof(ArticleDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetArticle")]
    public async Task<ActionResult<ArticleDto>> GetArticle(Guid id)
    {
        var query = new GetArticle.ArticleQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Articles.
    /// </summary>
    /// <response code="200">Article list returned successfully.</response>
    /// <response code="400">Article has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Article.</response>
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
    [ProducesResponseType(typeof(IEnumerable<ArticleDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetArticles")]
    [Authorize]
    public async Task<IActionResult> GetArticles([FromQuery] ArticleParametersDto articleParametersDto)
    {
        var query = new GetArticleList.ArticleListQuery(articleParametersDto);
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
    /// Updates an entire existing Article.
    /// </summary>
    /// <response code="204">Article updated.</response>
    /// <response code="400">Article has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Article.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateArticle")]
    public async Task<IActionResult> UpdateArticle(Guid id, ArticleForUpdateDto article)
    {
        var command = new UpdateArticle.UpdateArticleCommand(id, article);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Article record.
    /// </summary>
    /// <response code="204">Article deleted.</response>
    /// <response code="400">Article has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Article.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteArticle")]
    public async Task<ActionResult> DeleteArticle(Guid id)
    {
        var command = new DeleteArticle.DeleteArticleCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
