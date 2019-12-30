using System.Web;
using System.Web.Http;
using EP.CursoMvc.Application.AutoMapper;

namespace EP.CursoMvc.REST.CursoAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
        }
    }
}
