namespace ArticlesManager.FunctionalTests.FunctionalTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetCollectionListTests : TestBase
{
    [Test]
    public async Task get_collection_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Collections.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}