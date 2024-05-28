using BRPartners.Application.Queries.Handlers;
using BRPartners.Application.Queries;
using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using Moq;

namespace BRPartners.Tests.Queries
{
    public class GetContactsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnListOfContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890" },
                new Contact { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "jane.doe@example.com", Phone = "9876543210" }
            };

            var repositoryMock = new Mock<IRepository<Contact>>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

            var handler = new GetContactsQueryHandler(repositoryMock.Object);

            var query = new GetContactsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(contacts, result);
        }
    }
}
