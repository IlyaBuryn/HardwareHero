namespace Storage.BusinessLogic.Contracts
{
    public interface IFileEventService
    {
        Task<ReturnFileUrlEvent> UploadFileAsync(UploadFileEvent uploadFileEvent);
        Task<ReturnFileUrlEvent> ChangeFileAsync(ChangeFileEvent changeFileEvent);
        Task<DeleteFileResultEvent> DeleteFileAsync(DeleteFileEvent deleteFileEvent);
    }
}
