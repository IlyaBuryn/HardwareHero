using EventDriven.Shared.Services;
using Identity.Shared.Events;
using Mail.DTOs.Events;
using Users.Api.Contracts;
using static Identity.Shared.Requests.UsersRequestRecords;

namespace Users.Api.Handlers
{
    public class UserEventsHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReplyService<CreateUserEvent, UserResultEvent> _createUserService;
        private readonly IReplyService<UpdateUserEvent, UserResultEvent> _updateUserService;
        private readonly IReplyService<DeleteUserEvent, UserResultEvent> _deleteUserService;

        private readonly IConsumerService<FindUserToMailSagaEvent> _userMailSaga;
        private readonly IProducerService<SendMailEvent> _mailService;

        private readonly ILogger<UserEventsHandler> _logger;

        public UserEventsHandler(
            IServiceProvider serviceProvider,
            IReplyService<CreateUserEvent, UserResultEvent> createUserService,
            IReplyService<UpdateUserEvent, UserResultEvent> updateUserService,
            IReplyService<DeleteUserEvent, UserResultEvent> deleteUserService,
            IConsumerService<FindUserToMailSagaEvent> userMailSaga,
            IProducerService<SendMailEvent> mailService,
            ILogger<UserEventsHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _createUserService = createUserService;
            _updateUserService = updateUserService;
            _deleteUserService = deleteUserService;
            _userMailSaga = userMailSaga;
            _mailService = mailService;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _createUserService.HandleRequestAsync(async createEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<IUsersService>();
                        var rolesService = scope.ServiceProvider.GetRequiredService<IRolesService>();
                        var result = await userService.CreateUserAsync(createEvent.Model);
                        var role = await rolesService.SetupUserRolesAsync(
                            new UserRolesRequest(new List<string> { Roles.User }, result.Id));

                        return new UserResultEvent()
                        {
                            Success = true,
                            User = result
                        };
                    }
                    catch (Exception ex)
                    {
                        return new UserResultEvent()
                        {
                            Success = false,
                            Error = ex.Message
                        };
                    }
                }
            }, stoppingToken);

            await _updateUserService.HandleRequestAsync(async updateEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<IUsersService>();
                        var result = await userService.UpdateUserAsync(updateEvent.Model);

                        return new UserResultEvent()
                        {
                            Success = true,
                            User = result
                        };
                    }
                    catch (Exception ex)
                    {
                        return new UserResultEvent()
                        {
                            Success = false,
                            Error = ex.Message
                        };
                    }
                }
            }, stoppingToken);

            await _deleteUserService.HandleRequestAsync(async deleteEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IUsersService>();
                        var result = await service.RemoveUserAsync(deleteEvent.UserId);

                        return new UserResultEvent()
                        {
                            Success = true
                        };
                    }
                    catch (Exception ex)
                    {
                        return new UserResultEvent()
                        {
                            Success = false,
                            Error = ex.Message
                        };
                    }
                }
            }, stoppingToken);

            await _userMailSaga.ConsumeAsync(async message =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IUsersService>();
                        var user = await service.GetUserByIdAsync(message.UserId);
                        message.Message!.Username = user.Username;
                        message.Message!.RecipientMailAddress = user.Email;

                        await _mailService.ProduceAsync(message.Message, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Can't find user by Id");
                    }
                }
            }, stoppingToken);
        }
    }
}
