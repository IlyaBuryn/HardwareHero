namespace Storage.BusinessLogic.Services
{
    public class FileEventService : IFileEventService
    {
        private readonly IFileRepositoryAsync _fileStorageRepo;

        public FileEventService(IFileRepositoryAsync fileStorageRepo)
        {
            _fileStorageRepo = fileStorageRepo;
        }


        public async Task<ReturnFileUrlEvent> UploadFileAsync(UploadFileEvent uploadFileEvent)
        {
            if (uploadFileEvent.File == null || uploadFileEvent.FileName == null)
            {
                return new ReturnFileUrlEvent()
                {
                    Success = false,
                    Error = new ArgumentNullException(
                        uploadFileEvent.File == null ? 
                            nameof(uploadFileEvent.File) :
                            nameof(uploadFileEvent.FileName)).Message
                };
            }

            var result = await _fileStorageRepo.UploadFileAsync(
                uploadFileEvent.File, uploadFileEvent.FileName);
            
            if (result.Errors.Any())
            {
                return new ReturnFileUrlEvent()
                {
                    Success = false,
                    Error = result.Errors.First().Message,
                };
            }

            return new ReturnFileUrlEvent()
            {
                Success = true,
                FileName = uploadFileEvent.FileName,
                FileUrl = result.Value,
            };
        }


        public async Task<ReturnFileUrlEvent> ChangeFileAsync(ChangeFileEvent changeFileEvent)
        {
            if (changeFileEvent.NewFile == null ||
                changeFileEvent.OldFileName == null ||
                changeFileEvent.NewFileName == null)
            {
                return new ReturnFileUrlEvent()
                {
                    Success = false,
                    Error = new ArgumentNullException(
                        changeFileEvent.NewFile == null ?
                            nameof(changeFileEvent.NewFile) :
                            changeFileEvent.OldFileName == null ?
                                nameof(changeFileEvent.OldFileName) :
                                nameof(changeFileEvent.NewFile)).Message
                };
            }

            var result = await _fileStorageRepo.ReplaceFileAsync(
                changeFileEvent.OldFileName, changeFileEvent.NewFile, changeFileEvent.NewFileName);

            if (result.Errors.Any())
            {
                return new ReturnFileUrlEvent()
                {
                    Success = false,
                    Error = result.Errors.First().Message,
                };
            }

            return new ReturnFileUrlEvent()
            {
                Success = true,
                FileName = changeFileEvent.NewFileName,
                FileUrl = result.Value,
            };
        }


        public async Task<DeleteFileResultEvent> DeleteFileAsync(DeleteFileEvent deleteFileEvent)
        {
            if (deleteFileEvent.FileName == null)
            {
                return new DeleteFileResultEvent()
                {
                    Success = false,
                    Error = new ArgumentNullException(nameof(deleteFileEvent.FileName)).Message
                };
            }

            var result = await _fileStorageRepo.DeleteFileAsync(deleteFileEvent.FileName);

            if (result.Errors.Any())
            {
                return new DeleteFileResultEvent()
                {
                    Success = false,
                    Error = result.Errors.First().Message,
                };
            }

            return new DeleteFileResultEvent() { Success = true };
        }
    }
}
