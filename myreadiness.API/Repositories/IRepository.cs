using System.Threading.Tasks;

namespace myreadiness.API.Repositories
{
    public interface IRepository
    {
        void Add<T>(T entity) where T: class; // T is generic Type of method // T: is a type of class
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
       
    }
}