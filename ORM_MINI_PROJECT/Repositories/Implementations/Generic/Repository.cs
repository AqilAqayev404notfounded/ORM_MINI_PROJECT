using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Context;
using ORM_MINI_PROJECT.Models.Common;
using ORM_MINI_PROJECT.Repositories.Interfaces.Generic;
using System.Linq.Expressions;

namespace ORM_MINI_PROJECT.Repositories.Implementations.Generic
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AppDbContext _appDbContext;


        public Repository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(params string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            var result = await query.ToListAsync();


            return result;
        }

        public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _appDbContext.Set<T>().Where(expression);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            var result = await query.ToListAsync();


            return result;
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _appDbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            var result = await query.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _appDbContext.Set<T>().AnyAsync(predicate);

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }
    }
}
