using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using BackEnd.NetCore.Common.Generics.Repositories;
using FluentValidation;
using BackEnd.NetCore.Common.ValueObjects;
using BackEnd.NetCore.Common.Models;

namespace BackEnd.NetCore.Common.Generics.Services
{
    public class Service<TModelo>
        where TModelo : Model
    {
        protected readonly Repository<TModelo> _repositorio;

        public Service(DbContext contexto)
        {
            _repositorio = new Repository<TModelo>(contexto);
        }

        public Service(Repository<TModelo> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual async Task<int> InserirAsync(TModelo modelo)
        {
            if (!modelo.Valido(out var resultadoValidacao))
            {
                throw new ValidationException("Modelo inválido", resultadoValidacao.Errors);
            }

            var id = await _repositorio.InsertAsync(modelo);

            _repositorio.Commit();

            return id;
        }

        public virtual async Task AtualizarAsync(TModelo modelo)
        {
            if (!modelo.Valido(out var resultadoValidacao))
            {
                throw new ValidationException("Modelo inválido", resultadoValidacao.Errors);                
            }

            await _repositorio.UpdateAsync(modelo);
            _repositorio.Commit();
        }

        public virtual async Task ExcluirAsync(int id)
        {
            await _repositorio.DeleteAsync(id);
            _repositorio.Commit();
        }

        public virtual async Task<TModelo> ConsultarPorIdentificadorAsync(int id)
        {
            return await _repositorio.GetByIdAsync(id);
        }
        
        public virtual async Task<IEnumerable<TModelo>> ConsultarPorExpressaoAsync(Expression<Func<TModelo, bool>> expression)
        {
            return await _repositorio.GetByExpressionAsync(expression);
        }

        public virtual async Task<PaginationResponse<TModelo>> ConsultarTodosAsync(Pagination pagination)
        {
            return await _repositorio.GetAllAsync(pagination);
        }
    }
}

