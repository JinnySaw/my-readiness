using System.Threading.Tasks;
using myreadiness.API.Data;

namespace myreadiness.API.Repositories
{
    public class MyRepository : IRepository
    {
        private readonly DataContext _context;
        public MyRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}