using DomainValidation.Validation;
using EP.CursoMvc.Infra.Data.UoW;

namespace EP.CursoMvc.Application.Services
{
    public abstract class BaseService
    {
        private readonly IUnitofWork _uow;
        protected ValidationResult ValidacaoProcesso = new ValidationResult();

        protected BaseService(IUnitofWork uow)
        {
            _uow = uow;
        }

        protected void AdicionarResultadoProcessamento(ValidationResult validationResult)
        {
            ValidacaoProcesso.Add(validationResult);
        }

        protected bool Commit()
        {
            return _uow.Commit();
        }
    }
}