using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Common.Repositories
{
    public class Repository<TEntidade> where TEntidade : Entidade
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
            var entidade = await GetByIdAsync(id, commandTimeout);

            Context.Database.SetCommandTimeout(commandTimeout);
            Context.Set<TEntidade>().Remove(entidade);
        }
        
        public Task<TEntidade> GetByIdAsync(int id)
        {
            return GetByIdAsync(id, null);
        }

        public virtual Task<TEntidade> GetByIdAsync(int id, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            return Context.Set<TEntidade>().FindAsync(id).AsTask();
        }      

        public Task<int> InsertAsync(TEntidade item)
        {
            return InsertAsync(item, null);
        }

        public virtual async Task<int> InsertAsync(TEntidade item, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            await Context.Set<TEntidade>().AddAsync(item);
            return item.Id;
        }

        public virtual void Update(TEntidade item, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            Context.Set<TEntidade>().Update(item);
        }

        public Task UpdateAsync(TEntidade item)
        {
            return UpdateAsync(item, null);
        }

        public Task UpdateAsync(TEntidade item, int? commandTimeout)
        {
            return Task.Run(() => Update(item, commandTimeout));
        }

        public virtual bool Commit()
        {
            return Context.SaveChanges() > 0;
        }

        public virtual IEnumerable<TEntidade> GetByExpression(Expression<System.Func<TEntidade, bool>> expression)
        {
            return GetByExpression(expression, null);
        }

        public IEnumerable<TEntidade> GetByExpression(Expression<System.Func<TEntidade, bool>> expression, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            return Context.Set<TEntidade>().Where(expression).ToList();
        }

        public Task<IEnumerable<TEntidade>> GetByExpressionAsync(Expression<System.Func<TEntidade, bool>> expression)
        {
            return GetByExpressionAsync(expression, null);
        }

        public Task<IEnumerable<TEntidade>> GetByExpressionAsync(Expression<System.Func<TEntidade, bool>> expression, int? commandTimeout)
        {
            return Task.Run(() => GetByExpression(expression, commandTimeout));
        }

        /*
        public virtual PaginationResponse<TEntidade, TIdentificador> GetAll(Pagination pagination)
        {
            return GetAll(pagination, null);
        }

        public PaginationResponse<TEntidade, TIdentificador> GetAll(Pagination pagination, int? commandTimeout)
        {
            Context.Database.SetCommandTimeout(commandTimeout);
            var resultado = Context.Set<TEntidade>().Skip(pagination.Skip).Take(pagination.Size).ToList();

            return new PaginationResponse<TEntidade, TIdentificador>(resultado, pagination.Page);
        }

        public Task<PaginationResponse<TEntidade, TIdentificador>> GetAllAsync(Pagination pagination)
        {
            return GetAllAsync(pagination, null);
        }

        public Task<PaginationResponse<TEntidade, TIdentificador>> GetAllAsync(Pagination pagination, int? commandTimeout)
        {
            return Task.Run(() => GetAll(pagination, commandTimeout));
        }

        */
    }
}
