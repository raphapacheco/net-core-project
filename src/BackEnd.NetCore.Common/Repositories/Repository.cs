using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Common.Repositories
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

        public virtual void Update(TModelo item, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            Context.Set<TModelo>().Update(item);
        }

        public Task UpdateAsync(TModelo item)
        {
            return UpdateAsync(item, null);
        }

        public Task UpdateAsync(TModelo item, int? commandTimeout)
        {
            return Task.Run(() => Update(item, commandTimeout));
        }

        public virtual bool Commit()
        {
            return Context.SaveChanges() > 0;
        }

        public virtual IEnumerable<TModelo> GetByExpression(Expression<System.Func<TModelo, bool>> expression)
        {
            return GetByExpression(expression, null);
        }

        public IEnumerable<TModelo> GetByExpression(Expression<System.Func<TModelo, bool>> expression, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            return Context.Set<TModelo>().Where(expression).ToList();
        }

        public Task<IEnumerable<TModelo>> GetByExpressionAsync(Expression<System.Func<TModelo, bool>> expression)
        {
            return GetByExpressionAsync(expression, null);
        }

        public Task<IEnumerable<TModelo>> GetByExpressionAsync(Expression<System.Func<TModelo, bool>> expression, int? commandTimeout)
        {
            return Task.Run(() => GetByExpression(expression, commandTimeout));
        }

        /*
        public virtual PaginationResponse<TModelo, TIdentificador> GetAll(Pagination pagination)
        {
            return GetAll(pagination, null);
        }

        public PaginationResponse<TModelo, TIdentificador> GetAll(Pagination pagination, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            var resultado = Context.Set<TModelo>().Skip(pagination.Skip).Take(pagination.Size).ToList();

            return new PaginationResponse<TModelo, TIdentificador>(resultado, pagination.Page);
        }

        public Task<PaginationResponse<TModelo, TIdentificador>> GetAllAsync(Pagination pagination)
        {
            return GetAllAsync(pagination, null);
        }

        public Task<PaginationResponse<TModelo, TIdentificador>> GetAllAsync(Pagination pagination, int? commandTimeout)
        {
            return Task.Run(() => GetAll(pagination, commandTimeout));
        }

        */
    }
}
