using System.Threading.Tasks;
using eConLab.Models.TokenAuth;
using eConLab.Web.Controllers;
using Shouldly;
using Xunit;

namespace eConLab.Web.Tests.Controllers
{
    public class HomeController_Tests: eConLabWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}