using CodeKata.Repositories;

namespace CodeKata
{
    public interface ICommandParser<T> 
    {
        IRepository<T> GetData();
    }
}
