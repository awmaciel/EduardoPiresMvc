using System.Web.Mvc;
using DomainValidation.Validation;

namespace EP.CursoMvc.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void PopularModelStateComErros(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Erros)
            {
                ModelState.AddModelError(string.Empty, erro.Message);
            }
        }
    }
}