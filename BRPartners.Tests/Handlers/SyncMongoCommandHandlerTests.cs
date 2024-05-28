using BRPartners.Application.Commands.Handlers;
using BRPartners.Application.Commands;
using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using Moq;

namespace BRPartners.Tests.Handlers
{
    public class SyncMongoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldAddContactToMongoDB()
        {
            // Arrange
            var mongoRepositoryMock = new Mock<IRepository<Contact>>();

            var command = new SyncMongoCommand
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Operation = "Add"
            };

            var handler = new SyncMongoCommandHandler(mongoRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mongoRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Contact>(c =>
                c.Id == command.Id &&
                c.Name == command.Name &&
                c.Email == command.Email &&
                c.Phone == command.Phone)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUpdateContactInMongoDB()
        {
            // Arrange
            var mongoRepositoryMock = new Mock<IRepository<Contact>>();

            var command = new SyncMongoCommand
            {
                Id = Guid.NewGuid(),
                Name = "John Doe Updated",
                Email = "john.doe.updated@example.com",
                Phone = "0987654321",
                Operation = "Update"
            };

            var handler = new SyncMongoCommandHandler(mongoRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mongoRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Contact>(c =>
                c.Id == command.Id &&
                c.Name == command.Name &&
                c.Email == command.Email &&
                c.Phone == command.Phone)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldDeleteContactFromMongoDB()
        {
            // Arrange
            var mongoRepositoryMock = new Mock<IRepository<Contact>>();

            var command = new SyncMongoCommand
            {
                Id = Guid.NewGuid(),
                Operation = "Delete"
            };

            var handler = new SyncMongoCommandHandler(mongoRepositoryMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mongoRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
        }
    }
}
