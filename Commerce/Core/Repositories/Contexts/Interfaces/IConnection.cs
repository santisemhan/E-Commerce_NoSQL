namespace Commerce.Core.Repositories.Contexts.Interfaces
{
    public interface IConnection<T> where T : class
    {
        public T GetConnection();
    }
}
