namespace Cart.Core.Repositories.Contexts.Interfaces
{
    public interface IConnection<T> where T : class
    {
        T GetConnection();
    }
}
