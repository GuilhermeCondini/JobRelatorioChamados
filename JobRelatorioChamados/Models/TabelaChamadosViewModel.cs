using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobRelatorioChamados.Models
{
    public class TabelaChamadosViewModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string SubStatus { get; set; }
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public DateTime DataAbertura { get; set; }
        public int DiasCorridos { get; set; }
        public bool Violado { get; set; }
        public DateTime? DataViolacao { get; set; }
        public string Observacao { get; set; }
        public string Acoes { get; set; }
        public string Categoria { get; set; }
        public string Cliente { get; set; }
    }
}