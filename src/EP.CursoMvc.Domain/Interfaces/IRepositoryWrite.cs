using System;
using EP.CursoMvc.Domain.Models;

namespace EP.CursoMvc.Domain.Interfaces
{
    public interface IRepositoryWrite<TEntity> : IDisposable where TEntity : Entity
    {
        TEntity Adicionar(TEntity obj);
        TEntity Atualizar(TEntity obj);
        void Remover(Guid id);
        int SaveChanges();
    }
}