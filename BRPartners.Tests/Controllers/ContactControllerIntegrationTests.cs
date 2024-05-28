using BRPartners.Application.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;


namespace BRPartners.Tests.Controllers
{
    public class ContactControllerIntegrationTests : IClassFixture<WebApplicationFactory<Presentation.API.Program>>
    {
        private readonly WebApplicationFactory<Presentation.API.Program> _factory;

        public ContactControllerIntegrationTests(WebApplicationFactory<Presentation.API.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateContactCommand { Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890" };
            var content = new StringContent(JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/contact", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/contact");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
