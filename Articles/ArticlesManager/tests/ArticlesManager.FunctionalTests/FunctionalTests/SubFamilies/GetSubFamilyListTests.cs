namespace ArticlesManager.FunctionalTests.FunctionalTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSubFamilyListTests : TestBase
{
    [Test]
    public async Task get_subfamily_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.SubFamilies.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}