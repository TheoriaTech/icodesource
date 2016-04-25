using System.Collections.Generic;

namespace Skunkworks.Ics.Web.Interfaces
{
    public interface IRepository<T>
    {
        bool Insert(T model);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T model);
        void Delete(int id);
    }
}