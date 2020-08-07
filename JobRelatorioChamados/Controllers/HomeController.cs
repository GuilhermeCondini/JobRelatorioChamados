using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure.Interception;
using JobRelatorioChamados.Models;
using JobRelatorioChamados.Business;

namespace JobRelatorioChamados.Controllers
{
    [RoutePrefix("Home/relatorio")]
    public class HomeController : Controller
    {
        public static List<TabelaChamadosViewModel> relatorio = new List<TabelaChamadosViewModel>();
        [Route("criarRelatorio")]
        [HttpGet]
        public async Task<HttpResponseMessage> Criar()
        {
            try
            {
                var gerarRelatorio = new GerarRelatorioBusiness();
                var relatorioPorto = gerarRelatorio.GerarRelatorioPorto();
                var relatorioBrasilseg = gerarRelatorio.GerarRelatorioBrasilSeg();
                var resolvidosBrasilSeg = gerarRelatorio.RelatorioResolvidos();
                var resolvidosPorto = gerarRelatorio.RelatorioResolvidosPorto(relatorioPorto);
                relatorio = resolvidosPorto.Concat(relatorioBrasilseg.Concat(resolvidosBrasilSeg).ToList()).ToList();

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        [Route("enviarEmail")]
        [HttpPost]
        public async Task<HttpResponseMessage> Enviar()
        {
            try
            {
                var envioEmail = new EnviarEmailBusiness();
                envioEmail.EnvioEmailRelatorio(relatorio);

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
}