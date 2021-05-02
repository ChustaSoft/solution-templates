using $ext_safeprojectname$.Framework.Policies;
using $ext_safeprojectname$.Framework.Projections;
using $ext_safeprojectname$.Framework.Test.BusTests.Aggregates;
using $ext_safeprojectname$.Framework.Test.BusTests.Commands;
using $ext_safeprojectname$.Framework.Test.BusTests.Events;
using $ext_safeprojectname$.Framework.Test.BusTests.Policies;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using $ext_safeprojectname$.Framework.Events;
using $ext_safeprojectname$.Framework.Commands;

namespace $ext_safeprojectname$.Framework.Test.BusTests
{
    public class BusTests
    {
        private readonly Mock<IEventStore> eventStore = new();
        private readonly List<DomainEvent> raisedEvents = new();
        private readonly Mock<IProjection<UserCreatedEvent>> userCreatedProjection = new();
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            eventStore
                .Setup(es => es.StoreEvent(It.IsAny<DomainEvent>()))
                .Callback<DomainEvent>(raisedEvents.Add);
            eventStore
                .Setup(es => es.GetEventStream(It.IsAny<Guid>()))
                .ReturnsAsync<Guid, IEventStore, List<DomainEvent>>(id => raisedEvents.Where(e => e.AggregateId == id).ToList());

            serviceProvider = CreateServiceProvider();
        }

        private IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IBus, Bus>();
            serviceCollection.AddSingleton(eventStore.Object);
            serviceCollection.AddSingleton<IPolicy<UserCreatedEvent>, SendEmailPolicy>();
            serviceCollection.AddTransient<ICommandHandler<CreateUserCommand>, User>();
            serviceCollection.AddTransient<ICommandHandler<UpdateUserCommand>, User>();
            serviceCollection.AddTransient<ICommandHandler<SendWelcomeEmailCommand>, User>();
            serviceCollection.AddSingleton(userCreatedProjection.Object);
            serviceCollection.AddTransient<ICommandHandler<CommandThatTriggersUnhandledEvent>, User>();

            return serviceCollection.BuildServiceProvider();
        }

        [Test]
        public async Task Should_handle_commands()
        {
            // Arrange
            var bus = serviceProvider.GetService<IBus>();
            var createUserCommand = new CreateUserCommand { Name = "Jeeves" };

            // Act
            await bus.ProcessCommand(createUserCommand);

            // Assert
            var creationEvent = raisedEvents.FirstOrDefault() as UserCreatedEvent;

            Assert.That(creationEvent, Is.Not.Null);
            Assert.That(creationEvent.AggregateId, Is.Not.EqualTo(default)); Assert.That(creationEvent.AggregateType, Is.EqualTo(typeof(User)));
            Assert.That(creationEvent.Name, Is.EqualTo("Jeeves"));

            var emailSentEvent = raisedEvents.Skip(1).FirstOrDefault() as WelcomeEmailSentEvent;

            Assert.That(emailSentEvent, Is.Not.Null);

            // Arrange
            var updateUserCommand = new UpdateUserCommand(creationEvent.AggregateId) { NewName = "Bertie" };

            // Act
            await bus.ProcessCommand(updateUserCommand);

            // Assert
            var updateEvent = raisedEvents.Skip(2).FirstOrDefault() as UserUpdatedEvent;

            Assert.That(updateEvent, Is.Not.Null);
            Assert.That(updateEvent.AggregateId, Is.Not.EqualTo(default));
            Assert.That(updateEvent.AggregateType, Is.EqualTo(typeof(User)));
            Assert.That(updateEvent.Name, Is.EqualTo("Bertie"));

            Assert.That(raisedEvents.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Should_Run_Projections()
        {
            // Arrange
            var bus = serviceProvider.GetService<IBus>();
            var userCreatedEvent = new UserCreatedEvent { AggregateId = Guid.NewGuid(), AggregateType = typeof(User), Name = "Roderick Glossop" };

            // Act
            await bus.RaiseEvent(userCreatedEvent);

            // Assert
            Assert.DoesNotThrow(
                () => userCreatedProjection.Verify(p => p.Project(userCreatedEvent), Times.Once)
            );
        }

        [Test]
        public void Should_show_error_if_no_command_handler_found()
        {
            // Arrange
            var bus = serviceProvider.GetService<IBus>();
            var commandWithoutHandler = new CommandWithoutHandler();

            // Act
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await bus.ProcessCommand(commandWithoutHandler)
            );

            // Assert
            Assert.That(exception.Message, Is.EqualTo(@"Please make sure that an aggregate that implements ""ICommandHandler<CommandWithoutHandler>"" is both implemented and registered in the DI container"));
        }

        [Test]
        public void Should_show_error_if_no_event_handler_found()
        {
            // Arrange
            var bus = serviceProvider.GetService<IBus>();
            var commandThatTriggersUnhandledEvent = new CommandThatTriggersUnhandledEvent();

            // Act
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await bus.ProcessCommand(commandThatTriggersUnhandledEvent)
            );

            // Assert
            Assert.That(exception.Message, Is.EqualTo(@"Please make sure that the aggregate ""User"" implements ""IEventHandler<EventWithoutHandler>"""));
        }
    }
}
