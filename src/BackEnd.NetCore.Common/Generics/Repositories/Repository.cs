using BackEnd.NetCore.Common.Models;
using BackEnd.NetCore.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Common.Generics.Repositories
{
    public class Repository<TModelo> where TModelo : Model
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync(id, null);
        }

        public virtual async Task DeleteAsync(int id, int? commandTimeout)
        {
            var modelo = await GetByIdAsync(id, commandTimeout);

            Context.Database.SetCommandTimeout(commandTimeout);
            Context.Set<TModelo>().Remove(modelo);
        }
        
        public Task<TModelo> GetByIdAsync(int id)
        {
            return GetByIdAsync(id, null);
        }

        public virtual Task<TModelo> GetByIdAsync(int id, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            return Context.Set<TModelo>().FindAsync(id).AsTask();
        }      

        public Task<int> InsertAsync(TModelo item)
        {
            return InsertAsync(item, null);
        }

        public virtual async Task<int> InsertAsync(TModelo item, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            await Context.Set<TModelo>().AddAsync(item);
            return item.Id;
        }

        public Task UpdateAsync(TModelo item)
        {
            return UpdateAsync(item, null);
        }

        public virtual async Task<int> UpdateAsync(TModelo item, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            var model = await Context.FindAsync<TModelo>(item.Id);
            Context.Entry(model).CurrentValues.SetValues(item);
            return item.Id;
        }       

        public Task<IEnumerable<TModelo>> GetByExpressionAsync(Expression<System.Func<TModelo, bool>> expression)
        {
            return GetByExpressionAsync(expression, null);
        }

        public async Task<IEnumerable<TModelo>> GetByExpressionAsync(Expression<System.Func<TModelo, bool>> expression, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            return await Context.Set<TModelo>().Where(expression).OrderBy(x => x.Id).ToListAsync();
        }          

        public Task<PaginationResponse<TModelo>> GetAllAsync(Pagination pagination)
        {
            return GetAllAsync(pagination, null);
        }

        public async Task<PaginationResponse<TModelo>> GetAllAsync(Pagination pagination, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            var resultado = await Context.Set<TModelo>().Skip(pagination.Skip).Take(pagination.Size).OrderBy(x => x.Id).ToListAsync();
            var count = await Context.Set<TModelo>().CountAsync();            

            return new PaginationResponse<TModelo>(resultado, count, pagination.Page, pagination.Size);
        }       

        public virtual bool Commit()
        {
            return Context.SaveChanges() > 0;
        }
    }
}
