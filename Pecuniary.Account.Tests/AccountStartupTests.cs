using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pecuniary.Account.Query.Controllers;

namespace Pecuniary.Account.Tests
{
    public class AccountStartupTests
    {
        [Fact]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            //  Arrange

            //  Setting up the stuff required for Configuration.GetConnectionString("DefaultConnection")
            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

            IServiceCollection services = new ServiceCollection();
            var target = new Pecuniary.Account.Query.Startup(configurationStub.Object);

            //  Act

            target.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<AccountController>();

            //  Assert

            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<AccountController>();
            controller.Should().NotBeNull();
        }
    }
}
