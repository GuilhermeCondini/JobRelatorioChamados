using JobRelatorioChamados.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using ClosedXML.Excel;

namespace JobRelatorioChamados.Business
{
    public class GerarRelatorioBusiness
    {

        public List<TabelaChamadosViewModel> GerarRelatorioBrasilSeg()
        {
            try
            {
                var resultBrasilSeg = new List<TabelaChamadosViewModel>();

                var diretorio = new DirectoryInfo(@"C:\Users\guilherme.santos\Downloads");
                var arquivos = diretorio.GetFiles().Where(x => x.FullName.EndsWith(".csv")).OrderByDescending(f => f.LastWriteTime).First();
                var reader = File.ReadAllLines(arquivos.FullName);

                foreach (var line in reader)
                {
                    var linha = line.Replace(@"""\""", string.Empty).Replace(@"""", string.Empty).Split(';');
                    if (linha[16] == "APP VISTORIA AGRO" || linha[16] == "IRISK")
                    {
                        var relatorioBrasilSeg = new TabelaChamadosViewModel();
                        relatorioBrasilSeg.Cliente = "BrasilSeg";
                        relatorioBrasilSeg.Id = linha[0];
                        relatorioBrasilSeg.Status = linha[3];
                        relatorioBrasilSeg.Titulo = linha[4];
                        relatorioBrasilSeg.Prioridade = linha[5];
                        relatorioBrasilSeg.DataAbertura = Convert.ToDateTime(linha[6]);
                        relatorioBrasilSeg.DiasCorridos = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(linha[6])).TotalDays);
                        if (linha[12] != "" && linha[12] != null)
                        {
                            relatorioBrasilSeg.DataViolacao = Convert.ToDateTime(linha[12]);
                        }
                        else
                        {
                            relatorioBrasilSeg.DataViolacao = null;
                        }
                        if (relatorioBrasilSeg.DataViolacao <= DateTime.Today && linha[12] != "" && linha[12] != null)
                        {
                            relatorioBrasilSeg.Violado = true;
                        }
                        resultBrasilSeg.Add(relatorioBrasilSeg);
                    }
                }

                return resultBrasilSeg;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
        public List<TabelaChamadosViewModel> GerarRelatorioPorto()
        {
            try
            {
                var diretorio = new DirectoryInfo(@"C:\Users\guilherme.santos\Downloads");
                var arquivo = diretorio.GetFiles().Where(x => x.FullName.EndsWith(".xls")).OrderByDescending(f => f.LastWriteTime).First();
                var reader = File.ReadAllLines(arquivo.FullName).Where(x => x.Contains("<Data ss"));
                var tabelaPorto = new TabelaChamadosViewModel();
                var resultTabelaPorto = new List<TabelaChamadosViewModel>();
                int count = 1;

                foreach (var line in reader.Skip(22))
                {
                    string result = FindTextBetween(line, "[CDATA[", "]]");
                    switch (count)
                    {
                        case 1:
                            {
                                tabelaPorto.Id = result;
                                count++;
                                break;
                            }
                        case 2:
                            {
                                tabelaPorto.Titulo = result;
                                count++;
                                break;
                            }
                        case 3:
                            {
                                tabelaPorto.Prioridade = result;
                                count++;
                                break;
                            }
                        case 5:
                            {
                                tabelaPorto.Status = result;
                                count++;
                                break;
                            }
                        case 8:
                            {
                                if (!string.IsNullOrEmpty(result))
                                    tabelaPorto.DataViolacao = Convert.ToDateTime(result);
                                else
                                    tabelaPorto.DataViolacao = null;
                                count++;
                                break;
                            }
                        case 9:
                            {
                                if (result == "0")
                                    tabelaPorto.Violado = false;
                                else
                                    tabelaPorto.Violado = true;
                                count++;
                                break;
                            }
                        case 11:
                            {
                                tabelaPorto.DataAbertura = Convert.ToDateTime(result);
                                tabelaPorto.DiasCorridos = Convert.ToInt32((DateTime.Now - tabelaPorto.DataAbertura).TotalDays);
                                tabelaPorto.Cliente = "Porto";
                                resultTabelaPorto.Add(tabelaPorto);
                                tabelaPorto = new TabelaChamadosViewModel();
                                count++;
                                break;
                            }
                        default:
                            {
                                count++;
                                if (count > 22)
                                {
                                    count = 1;
                                }
                                break;
                            }
                    }
                }

                return resultTabelaPorto;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }

        public List<TabelaChamadosViewModel> RelatorioResolvidos()
        { 
            try
            {
                var tabelaRelatorio = new TabelaChamadosViewModel();
                var listaTabela = new List<TabelaChamadosViewModel>();
                var diaSemana = Convert.ToInt32(DateTime.Today.DayOfWeek);

                using (var workbook = new XLWorkbook(@"\\confitecsp12\usuarios\caio.silva\Inspeções mesmo dia.xlsx"))
                {
                    var ws = workbook.Worksheet("Planilha1");
                    
                    if (diaSemana < Convert.ToInt32(ws.Cell(1, 11).Value))
                    {
                        ws.Range(2, 1, ws.RowsUsed().Count(), 9).Clear();
                    }

                    var relatorioPlanilha = ws.RangeUsed().AsTable().AsDynamicEnumerable();
                    relatorioPlanilha = relatorioPlanilha.Where(x => x.Id != null && x.Id != "").ToList();
                    foreach (var relatorio in relatorioPlanilha)
                    {
                        tabelaRelatorio = new TabelaChamadosViewModel();
                        tabelaRelatorio.Cliente = "BrasilSeg";
                        tabelaRelatorio.Id = relatorio.Id;
                        tabelaRelatorio.Status = relatorio.Status;
                        tabelaRelatorio.Titulo = relatorio.Titulo;
                        tabelaRelatorio.Prioridade = relatorio.Prioridade;
                        tabelaRelatorio.DataAbertura = Convert.ToDateTime(relatorio.Abertura);
                        tabelaRelatorio.DiasCorridos = Convert.ToInt32((DateTime.Now - tabelaRelatorio.DataAbertura).TotalDays);
                        if (relatorio.Violado == "Não" || relatorio.DataViolacao > tabelaRelatorio.DataAbertura)
                        {
                            tabelaRelatorio.Violado = false;
                        }
                        else
                        {
                            tabelaRelatorio.Violado = true;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(relatorio.DataViolacao)))
                            tabelaRelatorio.DataViolacao = Convert.ToDateTime(relatorio.DataViolacao);
                        else
                            tabelaRelatorio.DataViolacao = null;

                        tabelaRelatorio.Observacao = relatorio.Observacoes;
                        listaTabela.Add(tabelaRelatorio);
                    }
                    ws.Cell(1, 11).SetValue<int>(diaSemana);
                    workbook.Save();
                }
                return listaTabela;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<TabelaChamadosViewModel> RelatorioResolvidosPorto(List<TabelaChamadosViewModel> relatorioPorto)
        {
            try
            {
                var listaTabela = new List<TabelaChamadosViewModel>();
                var tabelaRelatorio = new TabelaChamadosViewModel();
                var diaSemana = Convert.ToInt32(DateTime.Today.DayOfWeek);

                using (var workbook = new XLWorkbook(@"\\confitecsp12\Usuarios\Victor.Valentim\InspecoesResolvidasPorto.xlsx"))
                {
                    var ws = workbook.Worksheet("Planilha1");
                    if (diaSemana < Convert.ToInt32(ws.Cell(1, 11).Value))
                    {
                        ws.Range(2, 1, ws.RowsUsed().Count(), 9).Clear();
                        workbook.Save();
                    }

                    ws.Cell(ws.RowsUsed().Count()+1, 1).InsertData(relatorioPorto.Where(x => x.Status == "Resolvido"));
                    var relatorioPlanilha = ws.RangeUsed().AsTable().AsDynamicEnumerable();
                    if (relatorioPlanilha.First().Id != "")
                    {
                        foreach (var relatorio in relatorioPlanilha)
                        {
                            tabelaRelatorio = new TabelaChamadosViewModel();
                            tabelaRelatorio.Cliente = "Porto";
                            tabelaRelatorio.Id = relatorio.Id;
                            tabelaRelatorio.Status = relatorio.Status;
                            tabelaRelatorio.Titulo = relatorio.Titulo;
                            tabelaRelatorio.Prioridade = relatorio.Prioridade;
                            tabelaRelatorio.DataAbertura = Convert.ToDateTime(relatorio.Abertura);
                            tabelaRelatorio.DiasCorridos = Convert.ToInt32((DateTime.Now - tabelaRelatorio.DataAbertura).TotalDays);
                            if (!relatorio.Violado)
                            {
                                tabelaRelatorio.Violado = false;
                            }
                            else
                            {
                                tabelaRelatorio.Violado = true;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(relatorio.DataViolacao)))
                                tabelaRelatorio.DataViolacao = Convert.ToDateTime(relatorio.DataViolacao);
                            else
                                tabelaRelatorio.DataViolacao = null;

                            tabelaRelatorio.Observacao = relatorio.Observacoes;
                            relatorioPorto.Add(tabelaRelatorio);

                            ws.Cell(1, 11).SetValue<int>(diaSemana);
                            workbook.Save();
                        }
                    }
                }
                listaTabela = relatorioPorto.GroupBy(i => i.Id).Select(x => x.First()).ToList();
                return listaTabela;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public string FindTextBetween(string text, string left, string right)
        {
            int beginIndex = text.IndexOf(left);
            if (beginIndex == -1)
                return string.Empty;

            beginIndex += left.Length;

            int endIndex = text.IndexOf(right, beginIndex);
            if (endIndex == -1)
                return string.Empty;

            return text.Substring(beginIndex, endIndex - beginIndex).Trim();
        }
    }
}