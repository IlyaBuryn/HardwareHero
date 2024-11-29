using EventDriven.Kafka.Services;
using EventDriven.Shared.Services;
using Storage.BusinessLogic.Contracts;
using Storage.DTOs.Events;

namespace Storage.Api.Handlers
{
    public class FileEventsHandler : BackgroundService
    {
        private readonly IReplyService<UploadFileEvent, ReturnFileUrlEvent> _uploadFileService;
        private readonly IReplyService<ChangeFileEvent, ReturnFileUrlEvent> _changeFileService;
        private readonly IReplyService<DeleteFileEvent, DeleteFileResultEvent> _deleteFileService;
        private readonly IServiceProvider _serviceProvider;

        public FileEventsHandler(
            IReplyService<UploadFileEvent, ReturnFileUrlEvent> uploadFileService, 
            IReplyService<ChangeFileEvent, ReturnFileUrlEvent> changeFileService, 
            IReplyService<DeleteFileEvent, DeleteFileResultEvent> deleteFileService,
            IServiceProvider serviceProvider)
        {
            _uploadFileService = uploadFileService;
            _changeFileService = changeFileService;
            _deleteFileService = deleteFileService;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _uploadFileService.HandleRequestAsync(async uploadEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IFileEventService>();
                    var result = await service.UploadFileAsync(uploadEvent);

                    return result;
                }
            }, stoppingToken);

            await _changeFileService.HandleRequestAsync(async changeEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IFileEventService>();
                    var result = await service.ChangeFileAsync(changeEvent);

                    return result;
                }
            }, stoppingToken);

            await _deleteFileService.HandleRequestAsync(async deleteEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IFileEventService>();
                    var result = await service.DeleteFileAsync(deleteEvent);

                    return result;
                }
            }, stoppingToken);
        }
    }
}
