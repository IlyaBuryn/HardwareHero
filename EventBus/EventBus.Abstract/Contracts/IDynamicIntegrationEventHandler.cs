namespace EventBus.Abstract.Contracts
{
    public interface IDynamicIntegrationEventHandler
    {
        Task HandleAsync(dynamic eventData);
    }
}
