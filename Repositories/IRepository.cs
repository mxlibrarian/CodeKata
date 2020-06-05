using CodeKata.Models;
using System.Collections.Generic;

namespace CodeKata.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void Add(T driver);
        T GetByName(string name);        
    }
}