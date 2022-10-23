namespace ArticlesManager.FunctionalTests.FunctionalTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFamilyListTests : TestBase
{
    [Test]
    public async Task get_family_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Families.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}