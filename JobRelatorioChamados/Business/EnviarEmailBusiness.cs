using JobRelatorioChamados.Models;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace JobRelatorioChamados.Business
{
    public class EnviarEmailBusiness
    {
        public bool EnvioEmailRelatorio(List<TabelaChamadosViewModel> relatorio)
        {
            try
            {

                string validaViolacaoLinha;
                MailMessage message = new MailMessage();

                message.IsBodyHtml = true;
                message.From = new MailAddress("willian.silva@confitec.com.br", "Willian Silva");
                message.Subject = String.Concat("iRisk - Report de chamados - Brasilseg e Porto - ", DateTime.Today.ToString("dd/MM"));
                message.To.Add(new MailAddress("willian.silva@confitec.com.br"));

                //cor header porto #5B9BD5
                //cor header brasilseg #FFD966
                //cor violado #FF5454
                //cor 50% SLA #FFE699

                string corpoEmail = @"<html xmlns:v=" + @"""urn:schemas-microsoft-com:vml"""+"xmlns:o="+@"""urn:schemas-microsoft-com:office:office"""+
                                    "xmlns:w="+ @"""urn:schemas-microsoft-com:office:word"""+"xmlns:x="+@"""urn:schemas-microsoft-com:office:excel"""+
                                    "xmlns:m="+ @"""http://schemas.microsoft.com/office/2004/12/omml"""+"xmlns="+@"""http://www.w3.org/TR/REC-html40"""+">"+
                                    @"<head>
                                        <meta http-equiv=Content-Type content="+@"""text/html; charset=iso-8859-1"""+">"+
                                        "<meta name=Generator content="+@"""Microsoft Word 15 (filtered medium)"""+">"+
                                        @"<!--[if !mso]><style>v\:* {behavior:url(#default#VML);}
                                    o\:* {behavior:url(#default#VML);}
                                    w\:* {behavior:url(#default#VML);}
                                    .shape {behavior:url(#default#VML);}
                                    </style><![endif]-->
                                        <style>
                                            < !--
                                    
                                            /* Font Definitions */
                                            @font-face {
                                                font-family:"+@"""Cambria Math"""+@"
                                                panose-1: 2 4 5 3 5 4 6 3 2 4;
                                            }
                                    
                                            @font-face {
                                                font-family: Calibri;
                                                panose-1: 2 15 5 2 2 2 4 3 2 4;
                                            }
                                    
                                            /* Style Definitions */
                                            p.MsoNormal,
                                            li.MsoNormal,
                                            div.MsoNormal {
                                                margin: 0cm;
                                                margin-bottom: .0001pt;
                                                font-size: 11.0pt;
                                                font-family:"+@"""Calibri"""+@", sans-serif;
                                                mso-fareast-language: EN-US;
                                            }
                                    
                                            a:link,
                                            span.MsoHyperlink {
                                                mso-style-priority: 99;
                                                color: #0563C1;
                                                text-decoration: underline;
                                            }
                                    
                                            a:visited,
                                            span.MsoHyperlinkFollowed {
                                                mso-style-priority: 99;
                                                color: #954F72;
                                                text-decoration: underline;
                                            }
                                    
                                            span.EstiloDeEmail17 {
                                                mso-style-type: personal-compose;
                                                font-family:"+@"""Calibri"""+@",sans-serif;
                                                color: windowtext;
                                            }
                                    
                                            .MsoChpDefault {
                                                mso-style-type: export-only;
                                                font-family:"+@"""Calibri"""+@", sans-serif;
                                                mso-fareast-language: EN-US;
                                            }
                                    
                                            @page WordSection1 {
                                                size: 612.0pt 792.0pt;
                                                margin: 70.85pt 3.0cm 70.85pt 3.0cm;
                                            }
                                    
                                            div.WordSection1 {
                                                page: WordSection1;
                                            }
                                    
                                            -->
                                        </style>
                                        <!--[if gte mso 9]><xml>
                                    <o:shapedefaults v:ext="+@"""edit"""+"spidmax="+@"""1026"""+@"/>
                                    </xml><![endif]-->
                                        <!--[if gte mso 9]><xml>
                                    <o:shapelayout v:ext="+@"""edit"""+@">
                                    <o:idmap v:ext="+@"""edit"""+@"data="+@"""1"""+@"/>
                                    </o:shapelayout></xml><![endif]-->
                                    </head>
                                    
                                    <body lang=PT-BR link="+@"""#0563C1"""+@"vlink="+@"""#954F72"""+@""">
                                        <div class=WordSection1>
                                            <p class=MsoNormal>Time,<o:p></o:p>
                                            </p>
                                            <p class=MsoNormal>
                                                <o:p>&nbsp;</o:p>
                                            </p>
                                            <p class=MsoNormal>Segue status dos chamados.<o:p></o:p>
                                            </p>
                                            <p class=MsoNormal>
                                                <o:p>&nbsp;</o:p>
                                            </p>
                                            <p class=MsoNormal><b><span lang=EN-US>BRASILSEG&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <o:p></o:p>
                                                        </span></b></p>";
                                                    
                string tabelaRelatorioBrasilseg =
                   @"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
                      style='margin-left:.1pt;border-collapse:collapse'>
                      <tr style='background:#FFD966'>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>ID do Incidente<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Status<o:p></o:p></span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Título<o:p></o:p></span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Prioridade<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Abertura<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Dias corridos<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Violado?<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Viola em<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Status Atuação<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Categoria Erro<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Ações<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Observações<o:p></o:p>
                                      </span></p>
                          </td>
                      </tr>";

                string tabelaRelatorioPorto =
                    @"</table>
                    <p class=MsoNormal>
                    <o:p>&nbsp;</o:p>
                    </p>
                    <p class=MsoNormal><b><span lang=EN-US>PORTO SEGURO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <o:p></o:p>
                    </span></b></p>
                      <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
                      style='margin-left:.1pt;border-collapse:collapse'>
                      <tr style='background:#5B9BD5'>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>ID do Incidente<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Status<o:p></o:p></span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Título<o:p></o:p></span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Prioridade<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Abertura<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Dias corridos<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Violado?<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Viola em<o:p></o:p></span>
                              </p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Status Atuação<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Categoria Erro<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Ações<o:p></o:p>
                                      </span></p>
                          </td>
                          <td nowrap
                              style='border:solid windowtext 1.0pt;border-left:none;padding:0cm 3.5pt 0cm 3.5pt'>
                              <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>Observações<o:p></o:p>
                                      </span></p>
                          </td>
                      </tr>";

                tabelaRelatorioBrasilseg = String.Concat(corpoEmail, tabelaRelatorioBrasilseg);

                foreach (var registro in relatorio)
                {
                    bool slaConsumido = false;
                    if (registro.DataViolacao != null)
                    {
                        slaConsumido = contaSLA(registro.DataViolacao, registro.Prioridade, registro.Cliente);
                    }

                    if (slaConsumido && !registro.Violado)
                    {
                        validaViolacaoLinha = @"<tr style=" + "background-color:#FFE699>";
                    }
                    else if (registro.Violado)
                    {
                        validaViolacaoLinha = @"<tr style=" + "background-color:#FF5454>";
                    }
                    else
                    {
                        validaViolacaoLinha = @"<tr>";
                    }
                    if (registro.Cliente == "BrasilSeg")
                    {
                        string linhasBrasilSeg =
                                @"<td
                                      style='border:solid windowtext 1.0pt;border-top:none;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>"+registro.Id+@"<o:p></o:p></span>
                                      </p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>"+registro.Status+ @"<o:p></o:p>
                                              </span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Titulo+@"<o:p>
                                              </o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>"+registro.Prioridade+@"<o:p></o:p></span>
                                      </p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal align=right style='text-align:right'><span
                                              style='color:black;mso-fareast-language:PT-BR'>"+registro.DataAbertura+@"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal align=right style='text-align:right'><span
                                              style='color:black;mso-fareast-language:PT-BR'>"+registro.DiasCorridos+@"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + (registro.Violado ? "Sim" : "Não") + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal align=right style='text-align:right'><span
                                              style='color:black;mso-fareast-language:PT-BR'>"+registro.DataViolacao+@"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>"+registro.StatusAtuacao+ @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.CategoriaErro + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Acoes + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Observacao + @"<o:p></o:p>
                                              </span></p>
                                  </td>
                                  </tr>";
                        linhasBrasilSeg = String.Concat(validaViolacaoLinha, linhasBrasilSeg);
                        tabelaRelatorioBrasilseg = String.Concat(tabelaRelatorioBrasilseg, linhasBrasilSeg);
                    }
                    else
                    {
                        string linhasPorto =
                        @"<td
                                      style='border:solid windowtext 1.0pt;border-top:none;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Id + @"<o:p></o:p></span>
                                      </p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Status + @"<o:p></o:p>
                                              </span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Titulo + @"<o:p>
                                              </o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Prioridade + @"<o:p></o:p></span>
                                      </p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal align=right style='text-align:right'><span
                                              style='color:black;mso-fareast-language:PT-BR'>" + registro.DataAbertura + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal align=right style='text-align:right'><span
                                              style='color:black;mso-fareast-language:PT-BR'>" + registro.DiasCorridos + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + (registro.Violado ? "Sim" : "Não") + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal align=right style='text-align:right'><span
                                              style='color:black;mso-fareast-language:PT-BR'>" + registro.DataViolacao + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.StatusAtuacao + @"<o:p></o:p></span></p>
                                  </td>                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.CategoriaErro + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Acoes + @"<o:p></o:p></span></p>
                                  </td>
                                  <td 
                                      style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0cm 3.5pt 0cm 3.5pt'>
                                      <p class=MsoNormal><span style='color:black;mso-fareast-language:PT-BR'>" + registro.Observacao + @"<o:p></o:p>
                                              </span></p>
                                  </td>
                                  </tr>";
                        linhasPorto = String.Concat(validaViolacaoLinha, linhasPorto);
                        tabelaRelatorioPorto = String.Concat(tabelaRelatorioPorto, linhasPorto);
                    }
                }

                corpoEmail = String.Concat(tabelaRelatorioBrasilseg, tabelaRelatorioPorto);
                corpoEmail = String.Concat(corpoEmail, @"</table>
                                                       <p class=MsoNormal>
                                                           <o:p>&nbsp;</o:p>
                                                       </p>
                                                       <p class=MsoNormal>Atenciosamente.<o:p></o:p>
                                                       </p>");

                message.Body = corpoEmail;

                using (var client = new SmtpClient("smtp-mail.outlook.com"))
                {
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    NetworkCredential credentials = new NetworkCredential("willian.silva@confitec.com.br", "Batata123");
                    client.EnableSsl = true;
                    client.Credentials = credentials;
                    client.Send(message);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool contaSLA(DateTime? dataViolacao, string prioridade, string cliente)
        {
            try
            {
                var countDays = Convert.ToInt32((Convert.ToDateTime(dataViolacao) - DateTime.Now).TotalDays);
                var diasValidos = new List<DateTime>();
                TimeSpan tempoTotal = TimeSpan.Parse(@"00:00:00");
                var validaSla = false;
                for(int index = 0; index < countDays; index ++)
                {
                    DateTime validaDia;
                    if (index > 0)
                    {
                        validaDia = DateTime.Today.AddDays(index);
                    }
                    else
                    {
                        validaDia = DateTime.Now;
                    }
                    if (validaDia.DayOfWeek != DayOfWeek.Saturday && validaDia.DayOfWeek != DayOfWeek.Sunday && !DateSystem.IsPublicHoliday(validaDia, CountryCode.BR))
                    {
                        diasValidos.Add(validaDia);
                    }
                }

                foreach(var dias in diasValidos)
                {
                    if (cliente == "BrasilSeg")
                    {
                        if (dias.TimeOfDay > TimeSpan.Parse("09:00:00"))
                        {
                            tempoTotal += TimeSpan.Parse(@"18:00:00").Subtract(dias.TimeOfDay);
                        }
                        else
                        {
                            tempoTotal += TimeSpan.Parse(@"08:00:00");
                        }
                    }
                    else
                    {
                        if (dias.TimeOfDay > TimeSpan.Parse("08:00:00"))
                        {
                            tempoTotal += TimeSpan.Parse(@"18:00:00").Subtract(dias.TimeOfDay);
                        }
                        else
                        {
                            tempoTotal += TimeSpan.Parse(@"08:00:00");
                        }
                    }
                }

                tempoTotal += Convert.ToDateTime(dataViolacao).TimeOfDay.Subtract(TimeSpan.Parse(@"09:00:00"));

                if (cliente == "Porto" && tempoTotal < TimeSpan.Parse("10:00:00"))
                {
                    validaSla = true;
                }
                else if(cliente == "BrasilSeg" && prioridade == "4 - Baixa" && tempoTotal < TimeSpan.Parse("20:00:00"))
                {
                    validaSla = true;
                }
                else if (cliente == "BrasilSeg" && prioridade == "3 - Media" && tempoTotal < TimeSpan.Parse("12:00:00"))
                {
                    validaSla = true;
                }

                return validaSla;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}