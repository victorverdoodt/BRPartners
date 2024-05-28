using BRPartners.Application.Commands;
using BRPartners.Application.Commands.Handlers;
using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using Moq;

namespace BRPartners.Tests.Handlers
{
    public class CreateContactCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldAddContactAndPublishToRabbitMQ()
        {
            // Arrange
            var efRepositoryMock = new Mock<IContactRepository>();
            var rabbitMQPublisherMock = new Mock<IRabbitMQPublisher>();

            var command = new CreateContactCommand
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890"
            };

            var handler = new CreateContactCommandHandler(efRepositoryMock.Object, rabbitMQPublisherMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            efRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Contact>()), Times.Once);
            rabbitMQPublisherMock.Verify(pub => pub.Publish(It.IsAny<SyncMongoCommand>()), Times.Once);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task Handle_ShouldNotAddContact_WhenNameIsEmpty()
        {
            // Arrange
            var efRepositoryMock = new Mock<IContactRepository>();
            var rabbitMQPublisherMock = new Mock<IRabbitMQPublisher>();

            var command = new CreateContactCommand
            {
                Name = "",
                Email = "john.doe@example.com",
                Phone = "1234567890"
            };

            var handler = new CreateContactCommandHandler(efRepositoryMock.Object, rabbitMQPublisherMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            efRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Contact>()), Times.Never);
            rabbitMQPublisherMock.Verify(pub => pub.Publish(It.IsAny<SyncMongoCommand>()), Times.Never);

            Assert.Equal(Guid.Empty, result);
        }
    }
}
