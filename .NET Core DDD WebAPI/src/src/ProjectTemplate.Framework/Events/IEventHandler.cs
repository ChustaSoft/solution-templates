namespace ProjectTemplate.Framework.Events
{
    public interface IEventHandler<T> where T : DomainEvent
    {
        public void PlayEvent(T domainEvent);
    }
}
