namespace ArticlesManager.Controllers.v1;

using ArticlesManager.Domain.Urls.Features;
using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.UrlsWithFilters.Features;
using ArticlesManager.Domain.UrlsWithFilters.Dtos;
using ArticlesManager.Domain.UrlsWithFilters.Dto;
using HeimGuard;

[ApiController]
[Route("api/url-filters")]
[ApiVersion("1.0")]
public class UrlWithFiltersController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHeimGuardClient _heimGuard;
    public UrlWithFiltersController(IMediator mediator, IHeimGuardClient heimGuard)
    {
        _heimGuard = heimGuard;
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a list of all the filters for a givel url.
    /// </summary>
    /// <response code="200">Filters list returned successfully.</response>
    /// <response code="400">Filters has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Filters.</response>
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
    [ProducesResponseType(typeof(UrlsWithFiltersDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "UrlFilters")]
    public async Task<IActionResult> UrlFilters([FromQuery] UrlsWithFiltersParametersDto urlParametersDto)
    {
        var query = new GetUrlsWithSizesAndLinesList.UrlsWithSizesAndLinesListQuery(urlParametersDto);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }
}
