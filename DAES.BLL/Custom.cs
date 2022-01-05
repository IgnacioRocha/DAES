﻿using DAES.Infrastructure;
using DAES.Infrastructure.GestionDocumental;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using FluentDateTime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace DAES.BLL
{
    public class Custom
    {
        private SmtpClient smtpClient = new SmtpClient();
        private MailMessage emailMsg = new MailMessage();
        private GestionDocumentalContext gestionDocumentalContext = new GestionDocumentalContext();
        private void Send()
        {
            try
            {
                smtpClient.Send(emailMsg);
            }
            catch 
            {
                return;
            }
        }

        public void LogAdd(Log log)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                context.Log.Add(log);
                context.SaveChanges();
            }
        }

        public Organizacion GenerarRegistro(Organizacion Organizacion)
        {

            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {

                var returnlist = new List<string>();
                var configuracionsecuencia = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.SecuenciaCorrelativo);
                if (configuracionsecuencia == null)
                {
                    throw new Exception("No se puede generar el correlativo ya que no existe la configuración de secuencia.");
                }

                if (!configuracionsecuencia.Valor.IsInt())
                {
                    throw new Exception("No se puede generar correlativo ya que la configuración del valor del último correlativo no es un número entero válido.");
                }

                var organizacion = context.Organizacion.FirstOrDefault(q => q.OrganizacionId == Organizacion.OrganizacionId);
                if (organizacion == null)
                {
                    organizacion = new Organizacion();
                    organizacion.FechaCreacion = DateTime.Now;
                    organizacion.NumeroRegistro = string.Empty;
                    context.Organizacion.Add(organizacion);
                }

                organizacion.TipoOrganizacionId = Organizacion.TipoOrganizacionId;
                organizacion.RUT = Organizacion.RUT;
                organizacion.RazonSocial = Organizacion.RazonSocial;
                organizacion.Sigla = Organizacion.Sigla;
                organizacion.EstadoId = (int)Infrastructure.Enum.Estado.RolAsignado;
                organizacion.FechaActualizacion = DateTime.Now;

                var cont = 1;
                while (context.Organizacion.Any(q => q.NumeroRegistro == organizacion.NumeroRegistro || organizacion.NumeroRegistro == string.Empty))
                {
                    organizacion.NumeroRegistro = (configuracionsecuencia.Valor.ToInt() + cont).ToString();
                    cont++;
                }

                configuracionsecuencia.Valor = (configuracionsecuencia.Valor.ToInt() + cont).ToString();
                context.SaveChanges();

                return organizacion;
            }
        }

        public List<string> DirectorioUpdate(List<Directorio> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }

                foreach (var item in list)
                {
                    var directorio = context.Directorio.FirstOrDefault(q => q.DirectorioId == item.DirectorioId);
                    if (directorio != null)
                    {
                        directorio.CargoId = item.CargoId;
                        directorio.NombreCompleto = item.NombreCompleto;
                        directorio.Rut = item.Rut;
                        directorio.FechaInicio = item.FechaInicio;
                        directorio.FechaTermino = item.FechaTermino;
                        directorio.GeneroId = item.GeneroId;
                    }
                }

                context.SaveChanges();
                return returnValue;
            }
        }
        public List<string> ModificacionUpdate(List<ModificacionEstatuto> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }

                foreach (var item in list)
                {
                    var modificacion = context.ModificacionEstatutos.FirstOrDefault(q => q.ModificacionEstatutoId == item.ModificacionEstatutoId);
                    if (modificacion != null)
                    {
                        modificacion.Fecha = item.Fecha;
                        modificacion.NumeroOficio = item.NumeroOficio;
                        modificacion.Vigente = item.Vigente;
                    }
                }

                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> ExistenciaUpdate(List<ExistenciaLegal> list, Organizacion or)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }


                foreach (var item in list)
                {
                    var exi = context.ExistenciaLegal.FirstOrDefault(q => q.ExistenciaId == item.ExistenciaId);
                    //var exis = context.ExistenciaLegal.Any(q => q.OrganizacionId == item.OrganizacionId);
                    if (exi != null)
                    {

                        exi.NumeroNorma = item.NumeroNorma;
                        exi.TipoNormaId = item.TipoNormaId;
                        exi.FechaNorma = item.FechaNorma;
                        exi.FechaPublicacionn = item.FechaPublicacionn;
                        exi.FechaPublic = item.FechaPublic;
                        exi.AutorizadoPor = item.AutorizadoPor;
                        exi.FechaConstitutivaSocios = item.FechaConstitutivaSocios;
                        exi.NumeroOficio = item.NumeroOficio;
                        exi.FechaOficio = item.FechaOficio;
                        exi.FechaEscrituraPublica = item.FechaEscrituraPublica;
                        exi.DatosGeneralesNotario = item.DatosGeneralesNotario;
                        exi.Fojas = item.Fojas;
                        exi.FechaInscripcion = item.FechaInscripcion;
                        exi.DatosCBR = item.DatosCBR;
                        exi.AprobacionId = item.AprobacionId;
                    }

                }
                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> ExistenciaAnteriorUpdate(List<ExistenciaAnterior> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }


                foreach (var item in list)
                {
                    var exi = context.ExistenciaLegalAnterior.FirstOrDefault(q => q.IdExistenciaAnterior == item.IdExistenciaAnterior);
                    //var exis = context.ExistenciaLegal.Any(q => q.OrganizacionId == item.OrganizacionId);
                    if (exi != null)
                    {

                        exi.TipoNormaId = item.TipoNormaId;
                        exi.NNorma = item.NNorma;
                        exi.FNorma = item.FNorma;
                        exi.FechaPublicacion = item.FechaPublicacion;
                        exi.Autorizado = item.Autorizado;
                    }

                }
                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> ExistenciaPostUpdate(List<ExistenciaPosterior> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }


                foreach (var item in list)
                {
                    var exi = context.ExistenciaPosterior.FirstOrDefault(q => q.idExistenciaPost == item.idExistenciaPost);
                    //var exis = context.ExistenciaLegal.Any(q => q.OrganizacionId == item.OrganizacionId);
                    if (exi != null)
                    {

                        exi.FechaConstitutivaSocios = item.FechaConstitutivaSocios;
                        exi.FechaEscrituraPublica = item.FechaEscrituraPublica;
                        exi.FechaPublicacionn = item.FechaPublicacionn;
                        exi.DatosGeneralesNotario = item.DatosGeneralesNotario;
                        exi.Fojas = item.Fojas;
                        exi.AnoInscripcion = item.AnoInscripcion;
                        exi.DatosCBR = item.DatosCBR;
                    }

                }
                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> SaneamientoUpdate(List<Saneamiento> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }

                foreach (var item in list)
                {
                    var sane = context.Saneamiento.FirstOrDefault(q => q.IdSaneamiento == item.IdSaneamiento);
                    if (sane != null)
                    {
                        sane.FechaEscrituraPublicaa = item.FechaEscrituraPublicaa;
                        sane.FechaaPublicacionDiario = item.FechaaPublicacionDiario;
                        sane.DatoGeneralesNotario = item.DatoGeneralesNotario;
                        sane.Fojass = item.Fojass;
                        sane.FechaaInscripcion = item.FechaaInscripcion;
                        sane.DatossCBR = item.DatossCBR;
                        //Sane.OrganizacionId = item.OrganizacionId;


                    }
                }
                context.SaveChanges();
                return returnValue;
            }
        }

        //public List<string> CrudMantenedoresActualizar(List<Reforma> list, List<Saneamiento> san, List<ExistenciaLegal> exis) {

        //    var returnValue = new List<string>();

        //    if (list == null || san == null || exis == null)
        //    {
        //        return returnValue;
        //    }

        //    return returnValue;
        //}

        public List<string> ReformaUpdate(List<Reforma> list, Organizacion mod)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }
                

                foreach (var item in list)
                {
                    var reforma = context.Reforma.FirstOrDefault(q => q.IdReforma == item.IdReforma);
                    if (reforma != null )
                    {
                        reforma.FechaReforma = item.FechaReforma;
                        reforma.FechaReformaa = item.FechaReformaa;
                        reforma.UltimaReforma = item.NumeroNormaa == null && reforma.NumeroOficio == null ? (bool)item.UltimaReformaa : item.UltimaReforma;
                        reforma.TipoNormaId = item.TipoNormaId;
                        reforma.NumeroNormaa = item.NumeroNormaa;
                        reforma.FechaNormaa = item.FechaNormaa;
                        reforma.FechaPublicacionDiario = item.FechaPublicacionDiario;
                        reforma.DatosGeneralNotario = item.DatosGeneralNotario;
                        reforma.FechaJuntaGeneral = item.FechaJuntaGeneral;
                        reforma.FechaPublicacion = item.FechaPublicacion;
                        reforma.AnoInscripcion = item.AnoInscripcion;
                        reforma.DatosCBR = item.DatosCBR;
                        reforma.FechaEscrituraPublica = item.FechaEscrituraPublica;
                        reforma.Fojas = item.Fojas;
                        reforma.AsambleaDepId = item.AsambleaDepId;
                        reforma.FechaOficio = item.FechaOficio;
                        reforma.NumeroOficio = item.NumeroOficio;
                        reforma.FechaAsambleaDep = item.FechaAsambleaDep;
                        reforma.AprobacionId = item.AprobacionId;
                    }
                }
                
                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> ReformaUpdateAG(List<ReformaAGAC> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }


                foreach (var item in list)
                {
                    var reforma = context.ReformaAGAC.FirstOrDefault(q => q.IdReformaAGAC == item.IdReformaAGAC);
                    if (reforma != null)
                    {
                        reforma.AsambleaDepId = item.AsambleaDepId;
                        reforma.FechaAsambleaDep = item.FechaAsambleaDep;
                        reforma.NumeroOficio = item.NumeroOficio;
                        reforma.FechaOficio = item.FechaOficio;
                        reforma.AprobacionId = item.AprobacionId;

                    }
                }

                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> ReformaUpdateAnt(List<ReformaAnterior> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }


                foreach (var item in list)
                {
                    var reforma = context.ReformaAnterior.FirstOrDefault(q => q.IdReformaAnterior == item.IdReformaAnterior);
                    if (reforma != null)
                    {
                        reforma.FechaReforma = item.FechaReforma;
                        reforma.TipoNormaId = item.TipoNormaId;
                        reforma.FechaNorma = item.FechaNorma;
                        reforma.NNorma = item.NNorma;
                        reforma.FechaPublicDiario = item.FechaPublicDiario;
                        reforma.DatosNotario = item.DatosNotario;
                    }
                }

                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> ReformaUpdatePost(List<ReformaPosterior> list)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                if (list == null)
                {
                    return returnValue;
                }


                foreach (var item in list)
                {
                    var reforma = context.ReformaPosterior.FirstOrDefault(q => q.IdReformaPost == item.IdReformaPost);
                    if (reforma != null)
                    {
                        reforma.FReforma = item.FReforma;
                        reforma.FechaJuntGeneralSocios = item.FechaJuntGeneralSocios;
                        reforma.FechaEscrituraPublica = item.FechaEscrituraPublica;
                        reforma.FojasNumero = item.FojasNumero;
                        reforma.AnoInscripcion = item.AnoInscripcion;
                        reforma.DatosCBR = item.DatosCBR;
                    }
                }

                context.SaveChanges();
                return returnValue;
            }
        }

        public List<string> DisolucionUpdate(List<Disolucion> listDisolucion, Disolucion disolucionss, List<ComisionLiquidadora> comisionLiquidadoras)
        {
            
            using(SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();
                if (listDisolucion == null)
                {
                    return returnValue;
                }
                /*if(liquidadoras == null)
                {
                    return returnValue;
                }*/

                
                foreach(var item in listDisolucion)
                {                    
                    var disolucion = context.Disolucions.FirstOrDefault(q => q.DisolucionId == item.DisolucionId);
                    var org = context.Organizacion.FirstOrDefault(q => q.OrganizacionId == item.OrganizacionId);
                    var comiLiqui = context.ComisionLiquidadora.FirstOrDefault(q => q.OrganizacionId == org.OrganizacionId);                    
                    // not Sure
                    
                    if (disolucion != null)
                    {
                        disolucion.TipoNormaId = item.TipoNormaId;

                        disolucion.NumeroNorma = item.NumeroNorma;
                        
                        disolucion.FechaNorma = item.FechaNorma;

                        disolucion.NumeroFojas = item.NumeroFojas;

                        disolucion.AñoInscripcion = item.AñoInscripcion;

                        disolucion.MinistroDeFe = item.MinistroDeFe;
                        
                        if(item.FechaPubliAnterior!= null)
                        {
                            disolucion.FechaPubliccionDiarioOficial = item.FechaPubliAnterior;
                        }
                        else if(item.FechaPubliPosterior != null)
                        {
                            disolucion.FechaPubliccionDiarioOficial = item.FechaPubliPosterior;
                        }
                        else
                        {
                            disolucion.FechaPubliccionDiarioOficial = item.FechaPubliccionDiarioOficial;
                        }
                        
                        /*disolucion.FechaPubliccionDiarioOficial = item.FechaPubliccionDiarioOficial;*/
                        
                        disolucion.Autorizacion = item.Autorizacion;
                        
                        if(item.FechaJuntaAnterior != null)
                        {
                            disolucion.FechaJuntaSocios = item.FechaJuntaAnterior;
                        }
                        else if(item.FechaJuntaPosterior != null)
                        {
                            disolucion.FechaJuntaSocios = item.FechaJuntaPosterior;
                        }
                        else
                        {
                            disolucion.FechaJuntaSocios = item.FechaJuntaSocios;
                        }
                        
                        disolucion.Comision = item.Comision;
                        
                        if(item.FechaDisAnterior != null)
                        {
                            disolucion.FechaDisolucion = item.FechaDisAnterior;
                        }
                        else if(item.FechaDisPost != null)
                        {
                            disolucion.FechaDisolucion = item.FechaDisPost;
                        }
                        else
                        {
                            disolucion.FechaDisolucion = item.FechaDisolucion;
                        }                        

                        
                        disolucion.NumeroOficio = item.NumeroOficio;
                        
                        disolucion.FechaOficio = item.FechaOficio;
                        
                        disolucion.FechaAsambleaSocios = item.FechaAsambleaSocios;
                        
                        disolucion.FechaEscrituraPublica = item.FechaEscrituraPublica;
                        
                        disolucion.NombreNotaria = item.NombreNotaria;
                        
                        disolucion.DatosNotario = item.DatosNotario;
                        
                        disolucion.DatosCBR = item.DatosCBR;

                        if (item.Comision)
                        {
                            foreach(var help in comisionLiquidadoras)
                            {
                                var comisionLiqui = context.ComisionLiquidadora.Where(q => q.ComisionLiquidadoraId == help.ComisionLiquidadoraId);
                                if (help.EsMiembro)
                                {
                                    comisionLiqui.FirstOrDefault().DisolucionId=item.DisolucionId;
                                    comisionLiqui.FirstOrDefault().CargoId = help.CargoId;
                                    comisionLiqui.FirstOrDefault().Rut = help.Rut;
                                    comisionLiqui.FirstOrDefault().GeneroId = help.GeneroId;
                                    comisionLiqui.FirstOrDefault().NombreCompleto = help.NombreCompleto;
                                    comisionLiqui.FirstOrDefault().FechaInicio = help.FechaInicio;
                                    comisionLiqui.FirstOrDefault().FechaTermino = help.FechaTermino;
                                    comisionLiqui.FirstOrDefault().EsMiembro = help.EsMiembro;
                                }
                                else
                                {
                                    comisionLiqui.FirstOrDefault().DisolucionId = item.DisolucionId;
                                    comisionLiqui.FirstOrDefault().CargoId = help.CargoId;
                                    comisionLiqui.FirstOrDefault().Rut = help.Rut;
                                    comisionLiqui.FirstOrDefault().GeneroId = help.GeneroId;
                                    comisionLiqui.FirstOrDefault().NombreCompleto = help.NombreCompleto;
                                    comisionLiqui.FirstOrDefault().FechaInicio = help.FechaInicio;
                                    comisionLiqui.FirstOrDefault().FechaTermino = help.FechaTermino;
                                    comisionLiqui.FirstOrDefault().EsMiembro = help.EsMiembro;
                                }
                            }
                        }
                    }
                }                

                context.SaveChanges();
                return returnValue;
            }
        }

        public void SignPDF(int documentoid, int TipoOrganizacionId, string NumeroFolio)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {

                //traer documento original y firmante
                var firmante = context.Firmante.FirstOrDefault(q => q.EsActivo);
                if (firmante == null)
                {
                    throw new Exception("No se encontró un firmante habilitado.");
                }

                var documento = context.Documento.Find(documentoid);
                if (documento == null)
                {
                    throw new Exception("No se encontró el documento a firmar.");
                }

                var tipoOrganizacion = context.TipoOrganizacion.Find(TipoOrganizacionId);
                if (tipoOrganizacion == null)
                {
                    throw new Exception("No se encontró el tipo de organización.");
                }

                //crear nuevo documento
                var nuevodocumento = new Documento();
                nuevodocumento.Activo = true;
                nuevodocumento.Autor = documento.Autor;
                nuevodocumento.Descripcion = documento.Descripcion;
                nuevodocumento.Enviado = documento.Enviado;
                nuevodocumento.FechaRecordatorio = documento.FechaRecordatorio;
                nuevodocumento.FechaResolucion = documento.FechaResolucion;
                nuevodocumento.FechaValidoHasta = documento.FechaValidoHasta;
                nuevodocumento.FirmanteId = documento.FirmanteId;
                nuevodocumento.OrganizacionId = documento.OrganizacionId;
                nuevodocumento.ProcesoId = documento.ProcesoId;
                nuevodocumento.Recordatorio = documento.Recordatorio;
                nuevodocumento.Resuelto = documento.Resuelto;
                nuevodocumento.TipoDocumentoId = documento.TipoDocumentoId;
                nuevodocumento.Url = documento.Url;
                nuevodocumento.WorkflowId = documento.WorkflowId;
                nuevodocumento.TipoPrivacidadId = documento.TipoPrivacidadId;
                //nuevodocumento.NumeroFolio = NumeroFolio;
                nuevodocumento.NumeroFolio = null;
                context.Documento.Add(nuevodocumento);
                context.SaveChanges();

                nuevodocumento.Content = SignPDF(nuevodocumento.DocumentoId, nuevodocumento.NumeroFolio, documento.Content, documento.FileName, firmante, true, documento.TipoDocumentoId, tipoOrganizacion.TipoOrganizacionId);
                nuevodocumento.Firmado = true;
                nuevodocumento.FileName = "FIRMADO - " + documento.FileName;

                documento.Activo = false;

                context.SaveChanges();
            }
        }

        public byte[] CrearCertificadoPDF(ConfiguracionCertificado configuracioncertificado, Organizacion organizacion, Firmante firmante, int id, int TipoDocumentoId)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {

                EventoTitulos ev = new EventoTitulos();
                Font _fontTitulo = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.DARK_GRAY);
                Font _fontNumero = new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.DARK_GRAY);
                Font _fontFirmante = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.DARK_GRAY);
                Font _fontStandard = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.DARK_GRAY);
                Font _fontStandardBold = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.DARK_GRAY);

                MemoryStream memStream = new MemoryStream();
                Document doc = new Document(PageSize.LEGAL);
                PdfWriter write = PdfWriter.GetInstance(doc, memStream);
                write.PageEvent = ev;
                Chunk SaltoLinea = Chunk.NEWLINE;

                //Paragraph rae = new Paragraph(configuracioncertificado.Parrafo3, _fontStandard);
                string parrafo_dos = string.Format(configuracioncertificado.Parrafo2);



                string parrafo_uno = string.Format(configuracioncertificado.Parrafo1);
                    if (!string.IsNullOrEmpty(organizacion.NumeroRegistro))
                    {
                        parrafo_uno = parrafo_uno.Replace("[ROL]", organizacion.NumeroRegistro);
                    }

                    if (!string.IsNullOrEmpty(organizacion.RazonSocial))
                    {
                        parrafo_uno = parrafo_uno.Replace("[RAZONSOCIAL]", organizacion.RazonSocial);
                    }

                    if (organizacion.Region != null)
                    {
                        parrafo_uno = parrafo_uno.Replace("[REGION]", organizacion.Region.Nombre);
                    }

                    if (!string.IsNullOrEmpty(organizacion.Direccion))
                    {
                        parrafo_uno = parrafo_uno.Replace("[DOMICIOSOCIAL]", organizacion.Direccion);
                    }

                var aux = organizacion.Disolucions.FirstOrDefault();
                if (organizacion.Documentos.FirstOrDefault().TipoDocumentoId==106) //TODO Modificar el valor "103" por el correspondiente en fase de produccion
                {
                    if (aux != null) //TODO Modificar el valor "103" por el correspondiente en fase de produccion
                    {
                    
                    #region Parrafo 1 Test Rocha

                    parrafo_uno = parrafo_uno.Replace("[SIGLA]", organizacion.Sigla ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[NUMEROOFICIO]", aux.NumeroOficio.ToString() ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[FECHAPUBLICACIONDIARIOOFICIAL]", string.Format("{0:dd-MM-yyyy}", aux.FechaPubliccionDiarioOficial) ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[FECHAESCRITURAPUBLICA]", string.Format("{0:dd-MM-yyyy}", aux.FechaEscrituraPublica) ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[FECHAJUNTASOCIOS]", string.Format("{0:dd-MM-yyyy}", aux.FechaJuntaSocios) ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[FECHADISOLUCION]", string.Format("{0:dd-MM-yyyy}", aux.FechaDisolucion) ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[NUMERONORMA]", aux.NumeroNorma.ToString() ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[FECHANORMA]", string.Format("{0:dd-MM-yyyy}", aux.FechaNorma) ?? string.Empty);

                    if (!string.IsNullOrEmpty(aux.Autorizacion))
                    {
                        parrafo_uno = parrafo_uno.Replace("[AUTORIZACION]", "autorizado por: " + aux.Autorizacion);
                    }
                    else
                    {
                        parrafo_uno = parrafo_uno.Replace("[AUTORIZACION]", string.Empty);
                    }

                    parrafo_uno = parrafo_uno.Replace("[NUMEROFOJAS]", aux.NumeroFojas ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[AÑOINSCRIPCION]", aux.AñoInscripcion.ToString() ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[DATOSCBR]", aux.DatosCBR ?? string.Empty);

                    if (!string.IsNullOrEmpty(aux.MinistroDeFe))
                    {
                        parrafo_uno = parrafo_uno.Replace("[MINISTRODEFE]", "ante el " + aux.MinistroDeFe);
                    }
                    else
                    {
                        parrafo_uno = parrafo_uno.Replace("[MINISTRODEFE]", string.Empty);
                    }

                    parrafo_uno = parrafo_uno.Replace("[FECHAOFICIO]", string.Format("{0:dd-MM-yyyy}", aux.FechaOficio) ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[FECHAASAMBLEASOCIOS]", string.Format("{0:dd-MM-yyyy}", aux.FechaAsambleaSocios) ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[NOMBRENOTARIA]", aux.NombreNotaria ?? string.Empty);

                    parrafo_uno = parrafo_uno.Replace("[DATOSNOTARIO]", aux.DatosNotario ?? string.Empty);

                    #endregion
                    }
                    else
                    {
                        throw new Exception("Aviso: La Organización no cuenta con sus datos actualizados " +
                            "para una emisión de certificado inmediata. Por favor, para proceder con su requerimiento, seleccione la opción 'Certificado Disolución (Solicitar emisión)'");
                    }

                    


                    if (!string.IsNullOrWhiteSpace(parrafo_dos))
                    {
                        if (organizacion.FechaCelebracion.HasValue)
                        {
                            parrafo_dos = parrafo_dos.Replace("[FECHACELEBRACION]", string.Format("{0:dd-MM-yyyy}", organizacion.FechaCelebracion.Value));
                        }
                    }
                    if (aux.Comision)
                    {
                        foreach (var item in organizacion.ComisionLiquidadoras)
                        {
                            if (item.EsMiembro)
                            {
                                var last = organizacion.ComisionLiquidadoras.Last();
                                var comi = context.ComisionLiquidadora.FirstOrDefault(q => q.ComisionLiquidadoraId == item.ComisionLiquidadoraId);
                                parrafo_dos = parrafo_dos.Replace("[COMISION]", "La última Comisión Liquidadora, registrada por este Departamento, estaba integrada por las siguientes personas: ");
                                if (!item.Equals(last))
                                {
                                    parrafo_dos += item.NombreCompleto + ", ";
                                }
                                else
                                {
                                    parrafo_dos += item.NombreCompleto + ".";
                                }
                            }                            
                        }
                    }
                    else
                    {
                        parrafo_dos = parrafo_dos.Replace("[COMISION]", "No existe Comisión Liquidadora vigente a esta fecha, registrada por este Departamento.");
                    }
                }



                string parrafo_cuatro = string.Format(configuracioncertificado.Parrafo4 != null ? configuracioncertificado.Parrafo4 : " ");
                string parrafo_tres = string.Format(configuracioncertificado.Parrafo3 != null ? configuracioncertificado.Parrafo3 : " ");
                string parrafoone = string.Format(configuracioncertificado.Parrafo1);
                string parrafos = string.Format(configuracioncertificado.Parrafo1);


                doc.Open();
                doc.AddTitle(configuracioncertificado.Titulo);
                doc.AddAuthor(firmante.Nombre);


                var centrar = Element.ALIGN_CENTER;
                Paragraph paragraphTITULO = new Paragraph(configuracioncertificado.Titulo, _fontTitulo);
                paragraphTITULO.Alignment = centrar;

                Paragraph paragraphUNO = new Paragraph(parrafo_uno, _fontStandard);
                paragraphUNO.Alignment = Element.ALIGN_JUSTIFIED;

                Paragraph paragraphDOS = new Paragraph(parrafo_dos, _fontStandard);
                paragraphDOS.Alignment = Element.ALIGN_JUSTIFIED;
                


                var logo = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.URLImagenLogo);
                if (logo == null)
                {
                    throw new Exception("No se encontró la configuración de url de rúbrica.");
                }

                if (logo != null && logo.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La configuración de url de rúbrica es inválida.");
                }

                Image imagenLogo = Image.GetInstance(logo.Valor);
                imagenLogo.ScalePercent(20);

                PdfPTable tableHeader = new PdfPTable(3);
                tableHeader.WidthPercentage = 100f;
                tableHeader.DefaultCell.Border = Rectangle.NO_BORDER;
                tableHeader.DefaultCell.Border = 0;

                //logo
                PdfPCell cell = new PdfPCell(imagenLogo);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.BorderWidth = 0;
                cell.Border = Rectangle.NO_BORDER;
                tableHeader.AddCell(cell);

                //title
                cell = new PdfPCell(new Phrase(configuracioncertificado.Titulo, _fontTitulo));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BorderWidth = 0;
                cell.Border = Rectangle.NO_BORDER;
                tableHeader.AddCell(cell);

                //Id
                var paragrafId = new Paragraph(string.Format("N° {0}", id), _fontNumero);
                paragrafId.Alignment = Element.ALIGN_RIGHT;

                var paragrafDate = new Paragraph(string.Format("Emitido el {0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), _fontStandard);
                paragrafDate.Alignment = Element.ALIGN_RIGHT;

                cell = new PdfPCell();
                cell.AddElement(paragrafId);
                cell.AddElement(paragrafDate);

                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BorderWidth = 0;
                cell.Border = Rectangle.NO_BORDER;
                tableHeader.AddCell(cell);

                doc.Add(tableHeader);
                doc.Add(new Paragraph());

                if(organizacion.Documentos.FirstOrDefault().TipoDocumentoId==106)
                {
                    doc.Add(SaltoLinea);
                    doc.Add(paragraphUNO);
                    doc.Add(SaltoLinea);
                    doc.Add(paragraphDOS);
                }

                if (organizacion.Documentos.FirstOrDefault().TipoDocumentoId != 103 && organizacion.Documentos.FirstOrDefault().TipoDocumentoId != 106)
                {
                    doc.Add(SaltoLinea);
                    doc.Add(paragraphUNO);
                    doc.Add(SaltoLinea);

                }

                if (organizacion.Documentos.FirstOrDefault().TipoDocumentoId == 103)
                {
                    if (organizacion.ExistenciaLegals.Any() || organizacion.Reformas.Any() || organizacion.Saneamientos.Any()|| organizacion.ExistenciaAnteriors.Any() || organizacion.ExistenciaPosteriors.Any()|| organizacion.ReformaAGACs.Any()|| organizacion.ReformaAnteriors.Any()|| organizacion.ReformaPosteriors.Any())
                    {
                        

                        if (organizacion.TipoOrganizacionId == 1)
                        {
                                parrafo_uno = parrafo_uno.Replace("[SIGLA]", organizacion.Sigla ?? string.Empty);

                                parrafo_uno = parrafo_uno.Replace("[RAZONSOCIAL]", organizacion.RazonSocial ?? string.Empty);

                                parrafo_uno = parrafo_uno.Replace("[TIPOORGANIZACION]", organizacion.TipoOrganizacion.Nombre ?? string.Empty);

                                parrafo_uno = parrafo_uno.Replace("[ROL]", organizacion.NumeroRegistro ?? string.Empty);

                                parrafo_uno = parrafo_uno.Replace("[VIGENTE]", organizacion.Estado.Nombre ?? string.Empty);

                            Paragraph parrafouno = new Paragraph(parrafo_uno, _fontStandard);
                            paragraphDOS.Alignment = Element.ALIGN_JUSTIFIED;

                            doc.Add(SaltoLinea);
                            doc.Add(parrafouno);
                            doc.Add(SaltoLinea);


                            if (organizacion.ExistenciaPosteriors.Any() && organizacion.ExistenciaPosteriors.FirstOrDefault().FechaPublicacionn != null)
                            {

                                if (organizacion.ExistenciaLegals.Any(q => q.FechaConstitutivaSocios != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHACONSTITUTIVASOCIOS]", "el acta de la junta general constitutiva de la cooperativa, celebrada con fecha " + organizacion.ExistenciaLegals.FirstOrDefault().FechaConstitutivaSocios.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHACONSTITUTIVASOCIOS]", string.Empty);
                                }

                                if (organizacion.ExistenciaPosteriors.Any(q => q.FechaEscrituraPublica != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAESCRITURAPUBLICA]", "fue reducida a escritura pública con fecha " + organizacion.ExistenciaPosteriors.FirstOrDefault().FechaEscrituraPublica.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAESCRITURAPUBLICA]", string.Empty);
                                }


                                if (organizacion.ExistenciaPosteriors.Any(q => q.FechaPublicacionn != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAPUBLICACIONN]", "publicada en el Diario Oficial de fecha " + organizacion.ExistenciaPosteriors.FirstOrDefault().FechaPublicacionn.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAPUBLICACIONN]", string.Empty);
                                }
                                

                                if (organizacion.ExistenciaPosteriors.Any(q => q.DatosGeneralesNotario != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[DATOSGENERALNOTARIO]", "ante el" + organizacion.ExistenciaPosteriors.FirstOrDefault().DatosGeneralesNotario.ToString() + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[DATOSGENERALNOTARIO]", string.Empty);
                                }

                                if (organizacion.ExistenciaPosteriors.Any(q => q.Fojas != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FOJAS]", "Fojas N° " + organizacion.ExistenciaPosteriors.FirstOrDefault().Fojas.ToString() + ", ");
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FOJAS]", string.Empty);
                                }


                                if (organizacion.ExistenciaPosteriors.Any(q => q.AnoInscripcion != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAINSCRIPCION]", "Con fecha de inscripción " + organizacion.ExistenciaPosteriors.FirstOrDefault().AnoInscripcion + ", ");
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAINSCRIPCION]", string.Empty);
                                }

                                if (organizacion.ExistenciaPosteriors.Any(q => q.DatosCBR != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[DATOSCBR]", "Datos del conservador de bienes raíces " + organizacion.ExistenciaPosteriors.FirstOrDefault().DatosCBR.ToString() + ".");
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[DATOSCBR]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.TipoNormaId != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[TIPONORMA]", "El tipo de la norma es " + organizacion.ExistenciaAnteriors.FirstOrDefault().tipoNorma.Nombre.ToString() + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[TIPONORMA]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.NNorma != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[NUMERONORMA]", "N° " + organizacion.ExistenciaAnteriors.FirstOrDefault().NNorma.ToString() + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[NUMERONORMA]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.FNorma != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHANORMA]", "de fecha " + organizacion.ExistenciaAnteriors.FirstOrDefault().FNorma.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHANORMA]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.FechaPublicacion != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAPUBLICACIONN]", "publicada en el Diario Oficial de fecha " + organizacion.ExistenciaAnteriors.FirstOrDefault().FechaPublicacion.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAPUBLICACIONN]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.Autorizado != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[AUTORIZADOPOR]", "del " + organizacion.ExistenciaAnteriors.FirstOrDefault().Autorizado.ToString() + ". " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[AUTORIZADOPOR]", string.Empty);
                                }


                                Paragraph parrafoDos = new Paragraph(parrafo_dos, _fontStandard);
                                paragraphDOS.Alignment = Element.ALIGN_JUSTIFIED;



                                
                                doc.Add(parrafoDos);
                                doc.Add(SaltoLinea);
                            }

                            if (organizacion.ExistenciaAnteriors.Any() && organizacion.ExistenciaAnteriors.FirstOrDefault().FechaPublicacion != null)
                            {
                                if (organizacion.ExistenciaAnteriors.Any(q => q.TipoNormaId != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[TIPONORMA]", "El tipo de la norma es " + organizacion.ExistenciaAnteriors.FirstOrDefault().tipoNorma.Nombre.ToString() + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[TIPONORMA]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.NNorma != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[NUMERONORMA]", "N° " + organizacion.ExistenciaAnteriors.FirstOrDefault().NNorma.ToString() + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[NUMERONORMA]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.FNorma != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHANORMA]", "de fecha " + organizacion.ExistenciaAnteriors.FirstOrDefault().FNorma.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHANORMA]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.FechaPublicacion != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAPUBLICACIONN]", "publicada en el Diario Oficial de fecha " + organizacion.ExistenciaAnteriors.FirstOrDefault().FechaPublicacion.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAPUBLICACIONN]", string.Empty);
                                }

                                if (organizacion.ExistenciaAnteriors.Any(q => q.Autorizado != null))
                                {
                                    parrafo_dos = parrafo_dos.Replace("[AUTORIZADOPOR]", "del " + organizacion.ExistenciaAnteriors.FirstOrDefault().Autorizado.ToString() + ". " ?? string.Empty);
                                }
                                else
                                {
                                    parrafo_dos = parrafo_dos.Replace("[AUTORIZADOPOR]", string.Empty);
                                }

                                if (organizacion.TipoOrganizacionId==1)
                                {
                                    parrafo_dos = parrafo_dos.Replace("[FECHAESCRITURAPUBLICA]", string.Empty);
                                    parrafo_dos = parrafo_dos.Replace("[DATOSGENERALNOTARIO]", string.Empty);
                                    parrafo_dos = parrafo_dos.Replace("[FOJAS]", string.Empty);
                                    parrafo_dos = parrafo_dos.Replace("[FECHAINSCRIPCION]", string.Empty);
                                    parrafo_dos = parrafo_dos.Replace("[DATOSCBR]", string.Empty);
                                }

                                Paragraph parrafoDoss = new Paragraph(parrafo_dos, _fontStandard);
                                paragraphDOS.Alignment = Element.ALIGN_JUSTIFIED;
                                doc.Add(parrafoDoss);
                                doc.Add(SaltoLinea);

                            }

                            
                            //Saneamiento

                            if (organizacion.Saneamientos.Any())
                                {
                                    if (organizacion.Saneamientos.FirstOrDefault().FechaEscrituraPublicaa != null)
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FECHAESCRITURAPUBLICA]", "Fecha de escritura pública: " + organizacion.Saneamientos.FirstOrDefault().FechaEscrituraPublicaa.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FECHAESCRITURAPUBLICA]", string.Empty);
                                    }

                                    if (organizacion.Saneamientos.FirstOrDefault().FechaaPublicacionDiario != null)
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FECHAPUBLICACIONDIARIO]", "Fecha de publicación en diario oficial: " + organizacion.Saneamientos.FirstOrDefault().FechaaPublicacionDiario.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FECHAPUBLICACIONDIARIO]", string.Empty);
                                    }

                                    if (organizacion.Saneamientos.FirstOrDefault().DatoGeneralesNotario != null)
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[DATOSGENERALESNOTARIO]", "Los datos generales del notario: " + organizacion.Saneamientos.FirstOrDefault().DatoGeneralesNotario.ToString() + ", ");
                                    }
                                    else
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[DATOSGENERALESNOTARIO]", string.Empty);
                                    }

                                    if (organizacion.Saneamientos.FirstOrDefault().Fojass != null)
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FOJAS]", "Fojas N° " + organizacion.Saneamientos.FirstOrDefault().Fojass + ", ");
                                    }
                                    else
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FOJAS]", string.Empty);
                                    }

                                    if (organizacion.Saneamientos.FirstOrDefault().FechaaInscripcion != null)
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FECHAINSCRIPCION]", "Con año de inscripción: " + organizacion.Saneamientos.FirstOrDefault().FechaaInscripcion.ToString() + ", ");
                                    }
                                    else
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[FECHAINSCRIPCION]", string.Empty);
                                    }

                                    if (organizacion.Saneamientos.FirstOrDefault().DatossCBR != null)
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[DATOSCBR]", "Los datos del conservador de bienes raices: " + organizacion.Saneamientos.FirstOrDefault().DatossCBR.ToString() + ".");
                                    }
                                    else
                                    {
                                        parrafo_tres = parrafo_tres.Replace("[DATOSCBR]", string.Empty);
                                    }

                                    Paragraph parr3 = new Paragraph(parrafo_tres, _fontStandard);
                                    parr3.Alignment = Element.ALIGN_JUSTIFIED;
                                    doc.Add(parr3);
                                    doc.Add(SaltoLinea);
                                }



                            if (organizacion.ReformaAnteriors.Any() == true && organizacion.ReformaAnteriors.FirstOrDefault().FechaReforma != null)
                            {
                                foreach (var item in organizacion.ReformaAnteriors.ToList().OrderByDescending(q => q.FechaReforma).ToList())
                                {
                                    string parrafo = string.Format(configuracioncertificado.Parrafo4);

                                    if (item.FechaReforma != null)
                                    {
                                        parrafo = parrafo.Replace("[FECHAREFORMA]", "La fecha de la reforma  " + item.FechaReforma.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[FECHAREFORMA]", string.Empty);
                                    }


                                    if (organizacion.ReformaAnteriors.FirstOrDefault().NNorma != null)
                                    {
                                        parrafo = parrafo.Replace("[NUMERONORMARREF]", "El N° de reforma  " + item.NNorma.ToString() + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[NUMERONORMARREF]", string.Empty);
                                    }

                                    if (organizacion.ReformaAnteriors.FirstOrDefault().FechaNorma != null)
                                    {
                                        parrafo = parrafo.Replace("[FECHANORMAREF]", "La fecha de la norma  " + item.FechaNorma.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[FECHANORMAREF]", string.Empty);
                                    }


                                    if (organizacion.ReformaAnteriors.FirstOrDefault().FechaPublicDiario != null)
                                    {
                                        parrafo = parrafo.Replace("[FECHAPUBLICACIONDIARIOREF]", "La fecha de publicación  en diario oficial," + item.FechaPublicDiario.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[FECHAPUBLICACIONDIARIOREF]", string.Empty);
                                    }

                                    if (organizacion.ReformaAnteriors.FirstOrDefault().DatosNotario != null)
                                    {
                                        parrafo = parrafo.Replace("[DATOGENERALNOTARIOREF]", "Datos general del notario son: " + item.DatosNotario.ToString() + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[DATOGENERALNOTARIOREF]", string.Empty);
                                    }

                                    if (organizacion.ReformaAnteriors.FirstOrDefault().TipoNormaId != null)
                                    {
                                        parrafo = parrafo.Replace("[TIPONORMAREF]", "El tipo de norma  " + item.TipoNorma.Nombre.ToString() + ". ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[TIPONORMAREF]", string.Empty);
                                    }

                                    if (organizacion.TipoOrganizacionId == 1)
                                    {
                                        parrafo = parrafo.Replace("[FECHAJUNTAGENERAL]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAPUBLICACIONREF]", string.Empty);
                                        parrafo = parrafo.Replace("[anoinscripcion]", string.Empty);
                                        parrafo = parrafo.Replace("[DATOSCBRREF]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAESCRITURAPUBLICAREF]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAOFICIOREF]", string.Empty);
                                        parrafo = parrafo.Replace("[NUMEROOFICIOREF]", string.Empty);
                                        parrafo = parrafo.Replace("[APROBACION]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAREFORMAA]", string.Empty);

                                    }


                                    var fechaMayorr = organizacion.ReformaAnteriors.OrderByDescending(q => q.FechaReforma).FirstOrDefault();

                                    if (item.FechaReforma != null && fechaMayorr.FechaReforma == item.FechaReforma)
                                    {
                                        parrafo = parrafo.Replace("[ULTIMA]", "Esta es la última reforma anterior al 2003");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[ULTIMA]", string.Empty);
                                    }



                                    Paragraph parro = new Paragraph(parrafo, _fontStandard);
                                    parro.Alignment = Element.ALIGN_JUSTIFIED;

                                    doc.Add(parro);


                                }
                            }

                            if (organizacion.ReformaPosteriors.Any())
                            {
                                foreach (var item in organizacion.ReformaPosteriors.ToList().OrderByDescending(q => q.FReforma).ToList())
                                {
                                    string parrafo = string.Format(configuracioncertificado.Parrafo4);

                                    if (item.FReforma != null)
                                    {
                                        parrafo = parrafo.Replace("[FECHAREFORMA]", "La fecha de la reforma  " + item.FReforma.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[FECHAREFORMA]", string.Empty);
                                    }
                                    if (organizacion.ReformaPosteriors.FirstOrDefault().FechaJuntGeneralSocios != null)
                                    {
                                        parrafo = parrafo.Replace("[FECHAREFORMAA]", "Con fecha de junta general de socios "+item.FechaJuntGeneralSocios.Value.ToString("dd/MM/yyyy") +", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[FECHAREFORMAA]", string.Empty);
                                    }


                                    if (organizacion.ReformaPosteriors.FirstOrDefault().FechaEscrituraPublica != null)
                                    {
                                        parrafo = parrafo.Replace("[FECHANORMAREF]", "La fecha de la norma  " + item.FechaEscrituraPublica.Value.ToString("dd/MM/yyyy") + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[FECHANORMAREF]", string.Empty);
                                    }

                                    if (organizacion.TipoOrganizacionId == 1)
                                    {
                                        parrafo = parrafo.Replace("[FECHAPUBLICACIONDIARIOREF]", string.Empty);
                                        parrafo = parrafo.Replace("[DATOGENERALNOTARIOREF]", string.Empty);
                                        parrafo = parrafo.Replace("[TIPONORMAREF]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAJUNTAGENERAL]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAPUBLICACIONREF]", string.Empty);
                                        parrafo = parrafo.Replace("[DATOSCBRREF]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAESCRITURAPUBLICAREF]", string.Empty);
                                        parrafo = parrafo.Replace("[FECHAOFICIOREF]", string.Empty);
                                        parrafo = parrafo.Replace("[NUMEROOFICIOREF]", string.Empty);
                                        parrafo = parrafo.Replace("[APROBACION]", string.Empty);
                                        parrafo = parrafo.Replace("[NUMERONORMARREF]", string.Empty);

                                    }



                                    if (organizacion.ReformaPosteriors.FirstOrDefault().FojasNumero != null)
                                    {
                                        parrafo = parrafo.Replace("[anoinscripcion]", "Fojas N°: " + item.FojasNumero + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[anoinscripcion]", string.Empty);
                                    }

                                    if (organizacion.ReformaPosteriors.FirstOrDefault().AnoInscripcion != null)
                                    {
                                        parrafo = parrafo.Replace("[anoinscripcion]", "El año de inscripción es:  " + item.AnoInscripcion + ", ");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[anoinscripcion]", string.Empty);
                                    }


                                    var fechaMayorr = organizacion.ReformaPosteriors.OrderByDescending(q => q.FReforma).FirstOrDefault();

                                    if (item.FReforma != null && fechaMayorr.FReforma == item.FReforma)
                                    {
                                        parrafo = parrafo.Replace("[ULTIMA]", "Esta es la última reforma posterior al 2003");
                                    }
                                    else
                                    {
                                        parrafo = parrafo.Replace("[ULTIMA]", string.Empty);
                                    }



                                    Paragraph parraf = new Paragraph(parrafo, _fontStandard);
                                    parraf.Alignment = Element.ALIGN_JUSTIFIED;

                                    doc.Add(parraf);



                                }
                            }


                            }
                            if(organizacion.TipoOrganizacionId == 2 || organizacion.TipoOrganizacionId == 3)
                            {
                                parrafoone = parrafoone.Replace("[SIGLA]", organizacion.Sigla ?? string.Empty);

                                parrafoone = parrafoone.Replace("[RAZONSOCIAL]", organizacion.RazonSocial ?? string.Empty);

                                parrafoone = parrafoone.Replace("[TIPOORGANIZACION]", organizacion.TipoOrganizacion.Nombre ?? string.Empty);

                                parrafoone = parrafoone.Replace("[ROL]", organizacion.NumeroRegistro ?? string.Empty);

                                parrafoone = parrafoone.Replace("[VIGENTE]", organizacion.Estado.Nombre ?? string.Empty);


                                if (organizacion.ExistenciaLegals.Any())
                                {

                                    if (organizacion.ExistenciaLegals.FirstOrDefault().FechaConstitutivaSocios != null)
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[FECHACONSTITUTIVASOCIOS]", "La fecha constitutiva de socios " + organizacion.ExistenciaLegals.FirstOrDefault().FechaConstitutivaSocios.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                    }
                                    else
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[FECHACONSTITUTIVASOCIOS]", string.Empty);
                                    }

                                    if (organizacion.ExistenciaLegals.FirstOrDefault().NumeroOficio != null)
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[NUMEROOFICIO]", "el N° de oficio " + organizacion.ExistenciaLegals.FirstOrDefault().NumeroOficio + ", " ?? string.Empty);
                                    }
                                    else
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[NUMEROOFICIO]", string.Empty);
                                    }

                                    if (organizacion.ExistenciaLegals.FirstOrDefault().FechaOficio != null)
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[FECHAOFICIO]", "con fecha de oficio " + organizacion.ExistenciaLegals.FirstOrDefault().FechaOficio.Value.ToString("dd/MM/yyyy") + ", " ?? string.Empty);
                                    }
                                    else
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[FECHAOFICIO]", string.Empty);
                                    }

                                    if (organizacion.ExistenciaLegals.FirstOrDefault().Aprobacion != null && organizacion.ExistenciaLegals.FirstOrDefault().Aprobacion.Nombre != null)
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[APROBACION]", "con aprobación " + organizacion.ExistenciaLegals.FirstOrDefault().Aprobacion.Nombre ?? string.Empty);
                                    }
                                    else
                                    {
                                        parrafo_dos = parrafo_dos.Replace("[APROBACION]", string.Empty);
                                    }

                                    Paragraph parrafoDos = new Paragraph(parrafo_dos, _fontStandard);
                                    parrafoDos.Alignment = Element.ALIGN_JUSTIFIED;

                                    Paragraph parrafoonee = new Paragraph(parrafoone, _fontStandard);
                                    parrafoDos.Alignment = Element.ALIGN_JUSTIFIED;

                                    doc.Add(SaltoLinea);
                                    doc.Add(parrafoonee);
                                    doc.Add(SaltoLinea);
                                    doc.Add(parrafoDos);
                                    doc.Add(SaltoLinea);

                                }
                                if (organizacion.Reformas.Any())
                                {

                                    foreach (var item in organizacion.ReformaAGACs.ToList().OrderByDescending(q => q.FechaAsambleaDep).ToList())
                                    {
                                        string parrafo = string.Format(configuracioncertificado.Parrafo4);

                                        parrafo = parrafo.Replace("[ASAMBLEA]", "Asamblea/Depósito: " + item.AsambleaDeposito.Descripcion);

                                        parrafo = parrafo.Replace("[FECHAASAMBLEA]", "Fecha de asamblea, " + item.FechaAsambleaDep.Value.ToString("dd/MM/yyyy"));

                                        parrafo = parrafo.Replace("[NUMEROOFICIO]", "N° de oficio, " + item.NumeroOficio);

                                        parrafo = parrafo.Replace("[FECHAOFICIO]", "Fecha de oficio, " + item.FechaOficio.Value.ToString("dd/MM/yyyy"));

                                        parrafo = parrafo.Replace("[APROBACION]", "Con aprobación: " + item.Aprobacion.Nombre + ", ");

                                        var fechaMayor = organizacion.ReformaAGACs.OrderByDescending(q => q.FechaAsambleaDep).FirstOrDefault();

                                        if (fechaMayor.FechaAsambleaDep == item.FechaAsambleaDep)
                                        {
                                            parrafo = parrafo.Replace("[ULTIMA]", "Esta es la última reforma. ");

                                        }
                                        else
                                        {
                                            parrafo = parrafo.Replace("[ULTIMA]", string.Empty);
                                        }


                                        Paragraph parrafoss = new Paragraph(parrafo, _fontStandard);
                                        parrafoss.Alignment = Element.ALIGN_JUSTIFIED;

                                        doc.Add(parrafoss);
                                        doc.Add(SaltoLinea);

                                    }
                                }

                            }
                        

                    }

                    else
                    {
                        throw new Exception("Aviso: La Organización no cuenta con sus datos actualizados " +
                        "para una emisión de certificado inmediata. Por favor, para proceder con su requerimiento, seleccione la opción 'Certificado Estatutos- Estatuto vigente actual (Solicitar emisión)'");
                    }
                }


                if (configuracioncertificado.TieneDirectorio)
                {
                    var directorio = context.Directorio.Where(q => q.OrganizacionId == organizacion.OrganizacionId).OrderBy(q => q.Cargo.Secuencia);
                    if (directorio.Any())
                    {

                        #region directorio

                        doc.Add(paragraphDOS);

                        if (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa)
                        {
                            PdfPTable table = new PdfPTable(2);
                            table.WidthPercentage = 100.0f;
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(new PdfPCell(new Phrase("Cargo", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Nombre", _fontStandardBold)));

                            foreach (var item in directorio.ToList())
                            {
                                var cargo = context.Cargo.FirstOrDefault(q => q.CargoId == item.CargoId);
                                if (cargo != null)
                                    table.AddCell(new PdfPCell(new Phrase(cargo.Nombre, _fontStandard)) { HorizontalAlignment = Element.ALIGN_JUSTIFIED });

                                table.AddCell(new PdfPCell(new Phrase(item.NombreCompleto, _fontStandard)) { HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                            }

                            if (!organizacion.NotaDirectorio.IsNullOrWhiteSpace())
                            {
                                table.AddCell(new PdfPCell(new Phrase("*NOTA", _fontStandard)));
                                table.AddCell(new PdfPCell(new Phrase(organizacion.NotaDirectorio, _fontStandardBold)) { HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                            }

                            table.SpacingBefore = 15f;
                            doc.Add(table);
                        }
                        else if (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial)
                        {
                            PdfPTable table = new PdfPTable(4);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.WidthPercentage = 100f;
                            table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(new PdfPCell(new Phrase("Cargo", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Nombre", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Vigencia Desde", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Vigencia Hasta", _fontStandardBold)));
                            table.SetWidths(new float[] { 4f, 6f, 3f, 3f });

                            foreach (var item in directorio.ToList())
                            {
                                var cargo = context.Cargo.FirstOrDefault(q => q.CargoId == item.CargoId);
                                if (cargo != null)
                                    table.AddCell(new PdfPCell(new Phrase(cargo.Nombre, _fontStandard)));

                                table.AddCell(new PdfPCell(new Phrase(item.NombreCompleto, _fontStandard)));
                                if (item.FechaInicio.HasValue)
                                    table.AddCell(new PdfPCell(new Phrase(string.Format("{0:dd-MM-yyyy}", item.FechaInicio.Value), _fontStandard)));

                                if (item.FechaTermino.HasValue)
                                    table.AddCell(new PdfPCell(new Phrase(item.FechaTermino.HasValue ? string.Format("{0:dd-MM-yyyy}", item.FechaTermino.Value) : string.Empty, _fontStandard)));
                            }

                            if (!organizacion.NotaDirectorio.IsNullOrWhiteSpace())
                            {
                                table.AddCell(new PdfPCell(new Phrase("*NOTA", _fontStandard)));
                                table.AddCell(new PdfPCell(new Phrase(organizacion.NotaDirectorio, _fontStandardBold)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                            }

                            table.SpacingBefore = 15f;
                            doc.Add(table);
                        }
                        else if (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionConsumidores)
                        {
                            PdfPTable table = new PdfPTable(4);
                            table.WidthPercentage = 100.0f;
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(new PdfPCell(new Phrase("Cargo", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Nombre", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Vigencia Desde", _fontStandardBold)));
                            table.AddCell(new PdfPCell(new Phrase("Vigencia Hasta", _fontStandardBold)));
                            table.SetWidths(new float[] { 4f, 6f, 3f, 3f });

                            foreach (var item in directorio.ToList())
                            {
                                var cargo = context.Cargo.FirstOrDefault(q => q.CargoId == item.CargoId);
                                if (cargo != null)
                                    table.AddCell(new PdfPCell(new Phrase(cargo.Nombre, _fontStandard)));

                                table.AddCell(new PdfPCell(new Phrase(item.NombreCompleto, _fontStandard)));

                                if (item.FechaInicio.HasValue)
                                    table.AddCell(new PdfPCell(new Phrase(string.Format("{0:dd-MM-yyyy}", item.FechaInicio.Value), _fontStandard)));

                                if (item.FechaTermino.HasValue)
                                    table.AddCell(new PdfPCell(new Phrase(item.FechaTermino.HasValue ? string.Format("{0:dd-MM-yyyy}", item.FechaTermino.Value) : string.Empty, _fontStandard)));
                            }

                            if (!organizacion.NotaDirectorio.IsNullOrWhiteSpace())
                            {
                                table.AddCell(new PdfPCell(new Phrase("*NOTA", _fontStandard)));
                                table.AddCell(new PdfPCell(new Phrase(organizacion.NotaDirectorio, _fontStandardBold)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                            }

                            table.SpacingBefore = 15f;
                            doc.Add(table);
                        }

                        #endregion directorio
                    }
                    else
                    {
                        if ((organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial) || (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionConsumidores)) //AG y AC
                        {
                            Paragraph SinDirectorio = new Paragraph(" * A la fecha, no se registra directorio de la organización en esta División.", _fontStandard);
                            doc.Add(SaltoLinea);
                            doc.Add(SinDirectorio);
                        }
                        else if (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa)//Cooperativas cambia
                        {
                            Paragraph SinDirectorio = new Paragraph(" * A la fecha, no se registra consejo de administración de la organización en esta División.", _fontStandard);
                            doc.Add(SaltoLinea);
                            doc.Add(SinDirectorio);
                        }
                    }
                }

                if (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa) // Cooperativa
                {
                    //string orden = "Por orden del Subsecretario";
                    doc.Add(SaltoLinea);
                    if (organizacion.Documentos.FirstOrDefault().TipoDocumentoId == 103)
                    {
                        string orden = "Se hace presente que no se registra en nuestros archivos la cancelación de la personalidad jurídica de dicha Cooperativa." +
                            " Saluda atentamente a ustedes.";

                        Paragraph porOrden = new Paragraph(orden, _fontStandard);
                        porOrden.Alignment = Element.ALIGN_JUSTIFIED;
                        doc.Add(porOrden);
                    }
                    else
                    {

                    
                    string orden = "Saluda atentamente a ustedes.";
                    Paragraph porOrden = new Paragraph(orden, _fontStandard);
                    porOrden.Alignment = Element.ALIGN_JUSTIFIED;
                    doc.Add(porOrden);
                    }
                }
                doc.Add(SaltoLinea);

                //if (organizacion.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial)// Asoc. Gremial
                //{
                //    orden = "Por orden del Ministro";
                //}

                Paragraph rae = new Paragraph(configuracioncertificado.Parrafo3, _fontStandard);
                rae.Alignment = Element.ALIGN_JUSTIFIED;

                if ((int)DAES.Infrastructure.Enum.TipoDocumento.VigenciaEstatutos != 103)
                {

                    doc.Add(rae);
                }

                //imagen firma
                var configFirma = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.URLImagenRubrica);
                if (configFirma == null)
                {
                    throw new Exception("No se encontró la configuración de url de rúbrica.");
                }

                if (configFirma != null && configFirma.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La configuración de url de rúbrica es inválida.");
                }

                Image imagenFirma = Image.GetInstance(configFirma.Valor + firmante.IdFirma + ".png");
                imagenFirma.Alignment = Element.ALIGN_CENTER;
                imagenFirma.ScalePercent(65);
                doc.Add(imagenFirma);

                Paragraph responsable = new Paragraph(firmante.Nombre, _fontStandardBold);
                responsable.Alignment = centrar;
                doc.Add(responsable);

                Paragraph _cargo = new Paragraph(firmante.Cargo, _fontStandardBold);
                _cargo.Alignment = centrar;
                doc.Add(_cargo);

                Paragraph _unidad = new Paragraph(firmante.UnidadOrganizacional, _fontStandardBold);
                _unidad.Alignment = centrar;
                doc.Add(_unidad);

                doc.Add(SaltoLinea);

                var configmensajeVerificacion = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.MensajeVerificacionCertificado);
                if (configmensajeVerificacion == null)
                {
                    throw new Exception("No existe la configuración de mensaje de verificación de certificados");
                }

                if (configmensajeVerificacion != null && configmensajeVerificacion.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La configuración de mensaje de verificación de certificados es inválida");
                }

                if (!string.IsNullOrEmpty(configmensajeVerificacion.Valor))
                {
                    Paragraph url = new Paragraph(configmensajeVerificacion.Valor + " usando el código " + id, _fontStandard);
                    url.Alignment = Element.ALIGN_CENTER;
                    doc.Add(url);
                }

                //mensaje de pie de firma
                var configmensajePie = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.MensajePieFirmaCertificado);
                if (configmensajePie == null)
                {
                    throw new Exception("No existe la configuración de mensaje pié de firma de certificados");
                }

                if (configmensajePie != null && configmensajePie.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La configuración de mensaje de pié de firma de certificados es inválida");
                }

                Paragraph pie = new Paragraph(configmensajePie.Valor, _fontStandard);
                pie.Alignment = Element.ALIGN_CENTER;
                doc.Add(pie);

                doc.Close();
                return memStream.ToArray();
            }
        }

        public void ReformaCreate(Reforma reforma, Organizacion or)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                var refo = context.Reforma.Any(q => q.IdReforma == reforma.IdReforma);
                if (refo == false )
                {
                    context.Reforma.Add(new Reforma()
                    {
                        //OrganizacionId = or.OrganizacionId,
                        FechaReforma = reforma.FechaReforma,
                        FechaReformaa = reforma.FechaReformaa,
                        UltimaReforma = reforma.UltimaReforma,
                        TipoNormaId = reforma.TipoNormaId,
                        NumeroNormaa = reforma.NumeroNormaa,
                        FechaNormaa = reforma.FechaNormaa,
                        FechaPublicacionDiario = reforma.FechaPublicacionDiario,
                        DatosGeneralNotario = reforma.DatosGeneralNotario,
                        FechaJuntaGeneral = reforma.FechaJuntaGeneral,
                        FechaPublicacion = reforma.FechaPublicacion,
                        AnoInscripcion = reforma.AnoInscripcion,
                        DatosCBR = reforma.DatosCBR,
                        FechaEscrituraPublica = reforma.FechaEscrituraPublica,
                        Fojas = reforma.Fojas,
                        AsambleaDepId = reforma.AsambleaDepId,
                        FechaOficio = reforma.FechaOficio,
                        NumeroOficio = reforma.NumeroOficio,
                        AprobacionId = reforma.AprobacionId


                    });
                    context.SaveChanges();
                }


            }
        }

        public void ReformaAGCreate(ReformaAGAC reforma, Organizacion or)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                var refo = context.ReformaAGAC.Any(q => q.IdReformaAGAC == reforma.IdReformaAGAC);
                if (refo == false)
                {
                    context.Reforma.Add(new Reforma()
                    {
                        //OrganizacionId = or.OrganizacionId,
                        AsambleaDepId = reforma.AsambleaDepId,
                        FechaAsambleaDep = reforma.FechaAsambleaDep,
                        NumeroOficio = reforma.NumeroOficio,
                        FechaOficio = reforma.FechaOficio,
                        AprobacionId = reforma.AprobacionId

                    });
                    context.SaveChanges();
                }


            }
        }
        public void ReformaPosteriorCreate(ReformaPosterior reforma, Organizacion or)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                var refo = context.ReformaPosterior.Any(q => q.IdReformaPost == reforma.IdReformaPost);
                if (refo == false)
                {
                    context.ReformaPosterior.Add(new ReformaPosterior()
                    {
                        //OrganizacionId = or.OrganizacionId,
                        FReforma = reforma.FReforma,
                        
                        FechaJuntGeneralSocios = reforma.FechaJuntGeneralSocios,
                        FechaEscrituraPublica = reforma.FechaEscrituraPublica,
                        FojasNumero = reforma.FojasNumero,
                        AnoInscripcion = reforma.AnoInscripcion,
                        DatosCBR = reforma.DatosCBR


                    });
                    context.SaveChanges();
                }


            }
        }

        public void ReformaAnteriorCreate(ReformaAnterior reforma, Organizacion or)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                var refo = context.ReformaAnterior.Any(q => q.IdReformaAnterior == reforma.IdReformaAnterior);
                if (refo == false)
                {
                    context.ReformaAnterior.Add(new ReformaAnterior()
                    {
                        //OrganizacionId = or.OrganizacionId,
                        FechaReforma = reforma.FechaReforma,
                        TipoNormaId = reforma.TipoNormaId,
                        FechaNorma = reforma.FechaNorma,
                        NNorma = reforma.NNorma,
                        FechaPublicDiario = reforma.FechaPublicDiario,
                        DatosNotario = reforma.DatosNotario

                    });
                    context.SaveChanges();
                }


            }
        }

        public void SaneamientoCreate(Saneamiento saneamiento, Organizacion or)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                var san = context.Saneamiento.Any(q => q.IdSaneamiento == saneamiento.IdSaneamiento);
                if (san == false)
                {
                    context.Saneamiento.Add(new Saneamiento()
                    {
                        OrganizacionId = or.OrganizacionId,
                        FechaEscrituraPublicaa = saneamiento.FechaEscrituraPublicaa,
                        FechaaPublicacionDiario = saneamiento.FechaaPublicacionDiario,
                        DatoGeneralesNotario = saneamiento.DatoGeneralesNotario,
                        Fojass = saneamiento.Fojass,
                        FechaaInscripcion = saneamiento.FechaaInscripcion,
                        DatossCBR = saneamiento.DatossCBR,


                    });
                    context.SaveChanges();
                }


            }
        }

        public void ExistenciaCreate(Organizacion or, ExistenciaLegal existenciaLegals)
        {

            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {


                var returnValue = new List<string>();

                var existencia = context.ExistenciaLegal.Any(q => q.ExistenciaId == or.ExistenciaLegals.FirstOrDefault().ExistenciaId);

                if (existencia == false)
                {
                    context.ExistenciaLegal.Add(new ExistenciaLegal()
                    {
                        OrganizacionId = or.OrganizacionId,
                        TipoNormaId = or.TipoNormaId,
                        NumeroNorma = existenciaLegals.NumeroNorma,
                        FechaNorma = existenciaLegals.FechaNorma,
                        FechaPublicacionn = existenciaLegals.FechaPublic != null ? existenciaLegals.FechaPublic : existenciaLegals.FechaPubli,
                        AutorizadoPor = existenciaLegals.AutorizadoPor,
                        FechaConstitutivaSocios = existenciaLegals.FechaConstitutivaSocios,
                        NumeroOficio = existenciaLegals.NumeroOficio,
                        FechaOficio = existenciaLegals.FechaOficio,
                        FechaEscrituraPublica = existenciaLegals.FechaEscrituraPublica,
                        DatosGeneralesNotario = existenciaLegals.DatosGeneralesNotario,
                        Fojas = existenciaLegals.Fojas,
                        FechaInscripcion = existenciaLegals.FechaInscripcion,
                        DatosCBR = existenciaLegals.DatosCBR,
                        AprobacionId = existenciaLegals.AprobacionId


                    });
                    context.SaveChanges();

                }



            }
        }

        public void ExistenciaAnteriorCreate(Organizacion or, ExistenciaAnterior ExiAnt)
        {

            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {


                var returnValue = new List<string>();

                var existencia = context.ExistenciaLegalAnterior.Any(q => q.OrganizacionId == ExiAnt.OrganizacionId);

                if (existencia == false)
                {
                    context.ExistenciaLegalAnterior.Add(new ExistenciaAnterior()
                    {
                        OrganizacionId = or.OrganizacionId,
                        TipoNormaId = or.TipoNormaId,
                        NNorma = ExiAnt.NNorma,
                        FNorma = ExiAnt.FNorma,
                        FechaPublicacion = ExiAnt.FechaPublicacion,
                        Autorizado = ExiAnt.Autorizado


                    });
                    context.SaveChanges();

                }

            }
        }

        public void ExistenciaPostCreate(Organizacion or, ExistenciaPosterior ExiPost)
        {

            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {


                var returnValue = new List<string>();

                var existencia = context.ExistenciaPosterior.Any(q => q.OrganizacionId == ExiPost.OrganizacionId);

                if (existencia == false)
                {
                    context.ExistenciaPosterior.Add(new ExistenciaPosterior()
                    {
                        OrganizacionId = or.OrganizacionId,
                        FechaConstitutivaSocios = ExiPost.FechaConstitutivaSocios,
                        FechaEscrituraPublica = ExiPost.FechaEscrituraPublica,
                        FechaPublicacionn = ExiPost.FechaPublicacionn,
                        DatosGeneralesNotario = ExiPost.DatosGeneralesNotario,
                        Fojas = ExiPost.Fojas,
                        AnoInscripcion = ExiPost.AnoInscripcion,
                        DatosCBR = ExiPost.DatosCBR



                    });
                    context.SaveChanges();

                }

            }
        }

        public byte[] SignPDF(int id, string folio, byte[] content, string nombre, Firmante firmante, bool AgregarRubrica, int TipoDocumentoId, int TipoOrganizacionId)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                //validaciones
                var UserHSM = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.UserHSM);
                if (UserHSM == null)
                {
                    throw new Exception("No se encontró la configuración de usuario de HSM.");
                }

                if (UserHSM != null && UserHSM.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La configuración de usuario de HSM es inválida.");
                }

                var PasswordHSM = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.PasswordHSM);
                if (PasswordHSM == null)
                {
                    throw new Exception("No se encontró la configuración de usuario de HSM.");
                }

                if (PasswordHSM != null && PasswordHSM.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La configuración de password de HSM es inválida.");
                }

                //si se requieres agregar rubrica al documento
                if (AgregarRubrica)
                {
                    var URLImagenRubrica = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.URLImagenRubrica);
                    if (URLImagenRubrica == null)
                    {
                        throw new Exception("No se encontró la configuración de url de rúbrica de firma electrónica.");
                    }

                    if (URLImagenRubrica != null && URLImagenRubrica.Valor.IsNullOrWhiteSpace())
                    {
                        throw new Exception("La configuración de url de rúbrica de firma electrónica es inválida.");
                    }

                    var configmensajeVerificacion = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.MensajeVerificacionCertificado);
                    if (configmensajeVerificacion == null)
                    {
                        throw new Exception("No existe la configuración de mensaje de verificación de certificados");
                    }

                    if (configmensajeVerificacion != null && configmensajeVerificacion.Valor.IsNullOrWhiteSpace())
                    {
                        throw new Exception("La configuración de mensaje de verificación de certificados es inválida");
                    }

                    var configmensajePie = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.MensajePieFirmaCertificado);
                    if (configmensajePie == null)
                    {
                        throw new Exception("No existe la configuración de mensaje pié de firma de certificados");
                    }

                    if (configmensajePie != null && configmensajePie.Valor.IsNullOrWhiteSpace())
                    {
                        throw new Exception("La configuración de mensaje de pié de firma de certificados es inválida");
                    }

                    Font _fontStandard = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.DARK_GRAY);
                    Font _fontStandardBold = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.DARK_GRAY);
                    Font _fontBold = new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD, BaseColor.DARK_GRAY);

                    try
                    {
                        //cargar imagen rubrica
                        Image image = Image.GetInstance(URLImagenRubrica.Valor + firmante.IdFirma + ".png");

                        using (MemoryStream ms = new MemoryStream())
                        {
                            var reader = new PdfReader(content);

                            using (PdfStamper stamper = new PdfStamper(reader, ms, '\0', true))
                            {
                                var pdfContentFirstPage = stamper.GetOverContent(1);

                                if (!string.IsNullOrWhiteSpace(folio))
                                {
                                    Rectangle pagesize = reader.GetPageSize(1);
                                    if (pagesize != null)
                                        ColumnText.ShowTextAligned(pdfContentFirstPage, Element.ALIGN_RIGHT, new Phrase(string.Format("N° {0}", folio), _fontBold), 550, pagesize.Height - 95, 0);
                                    else
                                        ColumnText.ShowTextAligned(pdfContentFirstPage, Element.ALIGN_RIGHT, new Phrase(string.Format("N° {0}", folio), _fontBold), 550, 940, 0);
                                }

                                int delta = 150;

                                var pdfContentByte = stamper.GetOverContent(reader.NumberOfPages);
                                image.SetAbsolutePosition(180, delta);
                                image.ScalePercent(80);
                                pdfContentByte.AddImage(image);

                                ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(firmante.Nombre, _fontStandardBold), 300, delta + 50, 0);
                                ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(firmante.Cargo, _fontStandardBold), 300, delta + 40, 0);
                                ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(firmante.UnidadOrganizacional, _fontStandardBold), 300, delta + 30, 0);

                                if (id > 0)
                                {
                                    ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(configmensajePie.Valor, _fontStandard), 300, delta + 10, 0);
                                    ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(configmensajeVerificacion.Valor + " usando el código " + id, _fontStandard), 300, delta + 0, 0);
                                }

                                stamper.Close();
                            }
                            content = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al insertar rubrica en el documento:" + ex);
                    }
                }

                //firmar electrónicamente
                try
                {
                    var header = new Infrastructure.ServiceReferenceHSM.EncabezadoRequest()
                    {
                        User = UserHSM.Valor,
                        Password = PasswordHSM.Valor,
                        NombreConfiguracion = firmante.IdFirma,
                        TipoIntercambio = "PDF",
                        FormatoDocumento = "b64",
                        RespuestaEsperada = "1"
                    };

                    var documento = new Infrastructure.ServiceReferenceHSM.DocumentoParametro()
                    {
                        Documento = Convert.ToBase64String(content),
                        NombreDocumento = nombre
                    };

                    var hsmService = new Infrastructure.ServiceReferenceHSM.WSIntercambiaDocSoapClient();
                    var response = hsmService.IntercambiaDoc(header, documento);

                    if (response.Estado.Contains("FAIL"))
                    {
                        throw new Exception("Error de firma de documento.");
                    }

                    return Convert.FromBase64String(response.Documento);
                }
                catch (Exception ex)
                {
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "FirmarPDF", LogTimeUtc = DateTime.UtcNow, LogDetails = "Se detectó un problema al momento de firmar el documento", LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = ex.Message });
                    throw new Exception("Error de firma de documento.");
                }
            }
        }

        public Proceso ProcesoStart(Proceso obj)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                if (!context.DefinicionProceso.Any(q => q.DefinicionProcesoId == obj.DefinicionProcesoId))
                {
                    throw new Exception("No se encontró la definición del proceso");
                }

                if (obj.Solicitante == null)
                {
                    throw new Exception("No se encontró el solicitante");
                }

                if (obj.Solicitante != null && obj.Solicitante.Email.IsNullOrWhiteSpace())
                {
                    throw new Exception("Debe especificar el email del solicitante");
                }

                if (obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.ConstitucionWeb)
                {
                    if (obj.Organizacion == null)
                    {
                        throw new Exception("No se encontró la información de la organización");
                    }
                }

                if (
                obj.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.EstudioSocioEconomicos &&
                obj.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionWeb &&
                obj.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionOP &&
                obj.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.CooperativaViviendaAbierta
                )
                { 
                    if (!context.Organizacion.Any(q => q.OrganizacionId == obj.OrganizacionId))
                    {
                        throw new Exception("No se encontró la organización");
                    }
                }

                if (obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoManual || obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico || obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.Actualizacion)
                {
                    if (obj.Solicitante != null && obj.Solicitante.Rut.IsNullOrWhiteSpace())
                    {
                        throw new Exception("Debe especificar el RUT del solicitante");
                    }

                    if (obj.Solicitante != null && obj.Solicitante.Nombres.IsNullOrWhiteSpace())
                    {
                        throw new Exception("Debe especificar el nombre del solicitante");
                    }

                    if (obj.Solicitante != null && obj.Solicitante.Apellidos.IsNullOrWhiteSpace())
                    {
                        throw new Exception("Debe especificar el apellido del solicitante");
                    }
                }

                if (obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoManual || obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico)
                {
                    if (!obj.Documentos.Any())
                    {
                        throw new Exception("No se encontró datos del documento a emitir");
                    }

                    if (!context.Firmante.Any(q => q.EsActivo))
                    {
                        throw new Exception("No se encontró un firmante de certificadoa habilitado");
                    }

                    if (obj.Documentos.Any())
                    {
                        foreach (var certificado in obj.Documentos)
                        {
                            if (!context.TipoDocumento.Any(q => q.TipoDocumentoId == certificado.TipoDocumentoId))
                            {
                                throw new Exception("No se encontró el tipo de documento a emitir");
                            }
                        }
                    }
                }

                //iniciar nuevo proceso
                var proceso = new Proceso();
                proceso.DefinicionProceso = context.DefinicionProceso.FirstOrDefault(q => q.DefinicionProcesoId == obj.DefinicionProcesoId);
                proceso.FechaVencimiento = DateTime.Now.AddBusinessDays(proceso.DefinicionProceso.Duracion);
                proceso.Observacion = obj.Observacion;
                proceso.UserId = obj.UserId;
                proceso.FechaCreacion = DateTime.Now;

                proceso.Solicitante = new Solicitante()
                {
                    Nombres = obj.Solicitante.Nombres,
                    Apellidos = obj.Solicitante.Apellidos,
                    Email = obj.Solicitante.Email,
                    Rut = obj.Solicitante.Rut,
                    Fono = obj.Solicitante.Fono,
                    RegionId = obj.Solicitante.RegionId,
                    Cargo = obj.Solicitante.Cargo
                };

                //en el caso de que el proceso se origine a partir de un documento, asociar datos
                if (obj.DocumentoId > 0)
                {
                    var documento = gestionDocumentalContext.Documento.FirstOrDefault(q => q.Id == obj.DocumentoId);
                    if (documento != null)
                    {
                        proceso.DocumentoId = documento.Id;
                        proceso.Correlativo = documento.Doc_Correlativo;
                    }
                }

                //en el caso de un proceso de constitucion, asociar nueva organización
                if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.ConstitucionWeb)
                {
                    //si viene datos de una organizacion, usarlos para crea la nueva
                    if (obj.Organizacion != null)
                    {
                        proceso.Organizacion = obj.Organizacion;
                    }

                    //si no vienen datos, crear una nueva organizacion
                    if (obj.Organizacion == null)
                    {
                        proceso.Organizacion = new Organizacion()
                        {
                            FechaCreacion = DateTime.Now,
                            TipoOrganizacionId = (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida,
                            EstadoId = (int)Infrastructure.Enum.Estado.RolAsignado,
                            NumeroSocios = 0,
                            NumeroSociosHombres = 0,
                            NumeroSociosMujeres = 0,
                            EsGeneroFemenino = false,
                            EsImportanciaEconomica = false
                        };
                    }
                }

                //en el caso de un proceso de constitucion, asociar nueva organización
                else if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.ConstitucionOP)
                {
                    //si no vienen datos, crear una nueva organizacion
                    proceso.Organizacion = new Organizacion()
                    {
                        FechaCreacion = DateTime.Now,
                        TipoOrganizacionId = (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida,
                        EstadoId = (int)Infrastructure.Enum.Estado.RolAsignado,
                        NumeroSocios = 0,
                        NumeroSociosHombres = 0,
                        NumeroSociosMujeres = 0,
                        EsGeneroFemenino = false,
                        EsImportanciaEconomica = false
                    };
                }



                //en el caso de un proceso de estudio socioeconomico
                if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.EstudioSocioEconomicos)
                {
                    foreach (var item in obj.EstudioSocioEconomicos)
                    {
                        proceso.EstudioSocioEconomicos.Add(new EstudioSocioEconomico
                        {
                            FechaCreacion = item.FechaCreacion,
                            DocumentoAdjunto = item.DocumentoAdjunto,
                            Proceso = proceso
                        });

                    }
                    //si viene datos de una organizacion, usarlos para crea la nueva
                    if (obj.Organizacion != null)
                    {
                        proceso.Organizacion = obj.Organizacion;
                    }

                    //si no vienen datos, crear una nueva organizacion
                    if (obj.Organizacion == null)
                    {
                        proceso.Organizacion = new Organizacion()
                        {
                            FechaCreacion = DateTime.Now,
                            TipoOrganizacionId = (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida,
                            EstadoId = (int)Infrastructure.Enum.Estado.RolAsignado,
                            NumeroSocios = 0,
                            NumeroSociosHombres = 0,
                            NumeroSociosMujeres = 0,
                            EsGeneroFemenino = false,
                            EsImportanciaEconomica = false
                        };
                    }

                }
                ////en el caso de un proceso distinto de constitucion asignar organizacion seleccionada
                else
                {
                    proceso.Organizacion = context.Organizacion.FirstOrDefault(q => q.OrganizacionId == obj.OrganizacionId);
                }


                //cooperativa de vivienda abierta
                //
                if (obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.CooperativaViviendaAbierta)
                {
                    if (obj.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.CooperativaViviendaAbierta)
                    { foreach (var item in obj.CooperativaAbiertas)
                        {
                            proceso.CooperativaAbiertas.Add(new CooperativaAbierta
                            {
                                FechaCreacion = item.FechaCreacion,
                                DocumentoAdjunto = item.DocumentoAdjunto,
                                Proceso = proceso
                            });

                        }
                        //si viene datos de una organizacion, usarlos para crea la nueva
                        if (obj.Organizacion != null)
                        {
                            proceso.Organizacion = obj.Organizacion;
                        }

                        //si no vienen datos, crear una nueva organizacion
                        if (obj.Organizacion == null)
                        {
                            proceso.Organizacion = new Organizacion()
                            {
                                FechaCreacion = DateTime.Now,
                                TipoOrganizacionId = (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida,
                                EstadoId = (int)Infrastructure.Enum.Estado.RolAsignado,
                                NumeroSocios = 0,
                                NumeroSociosHombres = 0,
                                NumeroSociosMujeres = 0,
                                EsGeneroFemenino = false,
                                EsImportanciaEconomica = false
                            };
                        }
                    }
                    else
                    {
                        proceso.Organizacion = context.Organizacion.FirstOrDefault(q => q.OrganizacionId == obj.OrganizacionId);
                    }
                }

                //en el caso de que sea certificado automático, generar pdf firmado
                if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico)
                {
                    var doc = obj.Documentos.FirstOrDefault();
                    var tipodoc = context.TipoDocumento.FirstOrDefault(q => q.TipoDocumentoId == doc.TipoDocumentoId);

                    var documento = context.Documento.Add(new Documento()
                    {
                        Firmante = context.Firmante.FirstOrDefault(q => q.EsActivo),
                        TipoDocumentoId = doc.TipoDocumentoId,
                        Organizacion = proceso.Organizacion,
                        Proceso = proceso,
                        FechaValidoHasta = tipodoc.DiasVigencia.HasValue ? DateTime.Now.AddDays(tipodoc.DiasVigencia.Value) : (DateTime?)null,
                        TipoPrivacidadId = (int)Infrastructure.Enum.TipoPrivacidad.Publico
                    });

                    proceso.Documentos.Add(documento);
                }

                //si el proceso trae actualizaciones de datos agregarlas
                if (obj.ActualizacionOrganizacions.Any())
                {
                    proceso.ActualizacionOrganizacions.Add(obj.ActualizacionOrganizacions.FirstOrDefault());
                }

                //si el proceso trae articulo 91, asociarlo
                if (obj.Articulo91s.Any())
                {
                    proceso.Articulo91s.Add(obj.Articulo91s.FirstOrDefault());
                }

                //asignar tareas a ejecutar
                var definicionworkflow = context.DefinicionWorkflow.Where(q => q.Habilitado && q.DefinicionProcesoId == proceso.DefinicionProceso.DefinicionProcesoId).OrderBy(q => q.Secuencia).ThenBy(q => q.DefinicionWorkflowId).FirstOrDefault();
                if (definicionworkflow != null)
                {
                    //si se asignó a usuario especifico cambiar, de lo contrario asignar a usuario designado en el diseño del proceso
                    var workflow = new Workflow()
                    {
                        FechaCreacion = DateTime.Now,
                        TipoAprobacionId = (int)Infrastructure.Enum.TipoAprobacion.SinAprobacion,
                        Terminada = false,
                        DefinicionWorkflow = definicionworkflow,
                        Proceso = proceso,
                        PerfilId = definicionworkflow.PerfilId,
                        UserId = !string.IsNullOrWhiteSpace(proceso.UserId) ? proceso.UserId : definicionworkflow.UserId
                    };

                    //si trae documento desde gestion documental, asociarlo
                    if (proceso.DocumentoId > 0)
                    {
                        foreach (var adjunto in gestionDocumentalContext.Adjunto.Where(q => q.IdRegistro == proceso.DocumentoId))
                        {
                            workflow.Documentos.Add(new Documento()
                            {
                                Autor = adjunto.Adj_LoginUsuario,
                                Descripcion = adjunto.Adj_Descripcion,
                                FechaCreacion = adjunto.Adj_FechaCreacion,
                                FileName = adjunto.Adj_Nombre,
                                Url = adjunto.Adj_Url,
                                TipoPrivacidadId = (int)Infrastructure.Enum.TipoPrivacidad.Publico
                            });
                        }
                    }

                    //si es un trámite en linea y viene con documentos adjuntos, asociarlos al workflow
                    if (obj != null && obj.Documentos.Any())
                    {
                        foreach (var adjunto in obj.Documentos)
                        {
                            var tipodocumento = context.TipoDocumento.FirstOrDefault(q => q.TipoDocumentoId == adjunto.TipoDocumentoId);
                            if (tipodocumento != null)
                            {
                                workflow.Documentos.Add(new Documento()
                                {
                                    Organizacion = proceso.Organizacion,
                                    Proceso = proceso,
                                    Autor = adjunto.Autor,
                                    FechaCreacion = adjunto.FechaCreacion,
                                    FileName = adjunto.FileName,
                                    Content = adjunto.Content,
                                    TipoDocumentoId = adjunto.TipoDocumentoId,
                                    NumeroOficio = adjunto.NumeroOficio,
                                    FechaSalidaOficio = adjunto.FechaSalidaOficio,
                                    TipoPrivacidadId = adjunto.TipoPrivacidadId,
                                    Periodo = adjunto.Periodo
                                });
                            }
                        }
                    }

                    //en el caso de que sea certificado manual, agregar comentario al proceso y workflow
                    if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoManual || proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico)
                    {
                        var documento = obj.Documentos.FirstOrDefault();
                        if (documento != null)
                        {
                            var tipodocumento = context.TipoDocumento.Find(documento.TipoDocumentoId);
                            if (tipodocumento != null)
                            {
                                proceso.Observacion = tipodocumento.Nombre;
                                workflow.Observacion = tipodocumento.Nombre;
                            }
                        }
                    }

                    //agregar workflow al proceso
                    proceso.Workflows.Add(workflow);
                }

                if (proceso.DefinicionProceso.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.Fiscalizacion)
                {
                    //si no existen tareas a asignar, terminar proceso
                    if (definicionworkflow == null)
                    {
                        proceso.FechaTermino = DateTime.Now;
                        proceso.Terminada = true;
                    }
                }

                //guardar proceso
                context.Proceso.Add(proceso);
                context.SaveChanges();

                //en el caso de que sea certificado automático, generar pdf firmado
                if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico)
                {
                    var documento = proceso.Documentos.FirstOrDefault();
                    var configuracionCertificado = context.ConfiguracionCertificado.FirstOrDefault(q => q.TipoDocumentoId == documento.TipoDocumentoId && q.TipoOrganizacionId == proceso.Organizacion.TipoOrganizacionId);
                    documento.Content = CrearCertificadoPDF(configuracionCertificado, proceso.Organizacion, documento.Firmante, documento.DocumentoId, documento.TipoDocumentoId);
                    documento.Content = SignPDF(documento.DocumentoId, documento.NumeroFolio, documento.Content, documento.DocumentoId.ToString(), documento.Firmante, false, documento.TipoDocumentoId, proceso.OrganizacionId);
                    documento.FileName = string.Concat(documento.DocumentoId, ".pdf");
                    documento.Firmado = true;

                    context.SaveChanges();
                }

                //notificar al solicitante via correo el inicio del proceso
                NotificarProceso(proceso, proceso.Solicitante.Email);

                //en el caso de que sea certificado manual, notificar a certificadosaes@economia.cl
                var configcopia = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.NotificarSolicitudCertificadoManual);
                if (proceso.DefinicionProceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoManual)
                {
                    if (configcopia != null && !string.IsNullOrWhiteSpace(configcopia.Valor))
                    {
                        NotificarProceso(proceso, configcopia.Valor);
                    }
                }

                //notificar via correo la ejecución del primer workflow
                if (proceso.Workflows.Any())
                {
                    NotificarTarea(proceso.Workflows.FirstOrDefault().WorkflowId);
                }

                return proceso;
            }
        }

        public void ProcesoUpdate(Workflow w, int? DefinicionWorkflowId)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                if (w == null)
                {
                    throw new Exception("Debe especificar un workflow.");
                }

                var workflow = context.Workflow.FirstOrDefault(q => q.WorkflowId == w.WorkflowId);
                if (workflow == null)
                {
                    throw new Exception("No se encontró el workflow.");
                }

                var proceso = context.Proceso.FirstOrDefault(q => q.ProcesoId == workflow.ProcesoId);
                if (proceso == null)
                {
                    throw new Exception("No se encontró el proceso asociado al workflow.");
                }

                var definicionproceso = context.DefinicionProceso.FirstOrDefault(q => q.DefinicionProcesoId == proceso.DefinicionProcesoId);
                if (definicionproceso == null)
                {
                    throw new Exception("No se encontró la definición de proceso asociado al workflow.");
                }

                var definicionworkflowlist = context.DefinicionWorkflow.Where(q => q.Habilitado && q.DefinicionProcesoId == proceso.DefinicionProcesoId).OrderBy(q => q.Secuencia).ThenBy(q => q.DefinicionWorkflowId);
                if (!definicionworkflowlist.Any())
                {
                    throw new Exception("No se encontró la definición de tarea del proceso asociado al workflow.");
                }

                //terminar tarea actual
                workflow.TipoAprobacionId = w.TipoAprobacionId;
                workflow.FechaTermino = DateTime.Now;
                workflow.Observacion = w.Observacion;
                workflow.Terminada = true;

                //si la tarea es despacho de documentos, notificar a jefatura
                if (workflow.DefinicionWorkflow.TipoWorkflow.Nombre.ToUpper() == "ARCHIVAR")
                {
                    NotificarArchivo(workflow.WorkflowId);
                }

                //si hay tareas paralelas pendientes de aprobacion, guardar y salir
                var tareasparalelas = context.Workflow.Where(q => q.WorkflowId != workflow.WorkflowId && q.WorkflowGrupoId != null && q.WorkflowGrupoId == workflow.WorkflowGrupoId);
                if (tareasparalelas.Any() && tareasparalelas.Any(q => q.TipoAprobacionId == (int)Infrastructure.Enum.TipoAprobacion.SinAprobacion))
                {
                    context.SaveChanges();
                    return;
                }

                //determinar estado de tarea(s)
                var aprobado = false;

                if (tareasparalelas.Any())
                {
                    aprobado = tareasparalelas.All(q => q.TipoAprobacionId == (int)Infrastructure.Enum.TipoAprobacion.Aprobada) && w.TipoAprobacionId == (int)Infrastructure.Enum.TipoAprobacion.Aprobada;
                }
                else
                {
                    aprobado = w.TipoAprobacionId == (int)Infrastructure.Enum.TipoAprobacion.Aprobada;
                }

                //determinar siguiente tarea en base a estado y definicion de proceso
                DefinicionWorkflow siguientedefinicionworkflow = null;
                if (DefinicionWorkflowId != null)
                {
                    siguientedefinicionworkflow = context.DefinicionWorkflow.FirstOrDefault(q => q.DefinicionWorkflowId == DefinicionWorkflowId);
                }
                else if (workflow == null)
                {
                    siguientedefinicionworkflow = definicionworkflowlist.FirstOrDefault();
                }
                else if (workflow != null && aprobado)
                {
                    siguientedefinicionworkflow = definicionworkflowlist.FirstOrDefault(q => q.Secuencia > workflow.DefinicionWorkflow.Secuencia);
                }
                else if (workflow != null && !aprobado)
                {
                    siguientedefinicionworkflow =
                        definicionworkflowlist.FirstOrDefault(q => q.DefinicionWorkflowId == workflow.DefinicionWorkflow.DefinicionWorkflowRechazoId);
                }

                // si existe siguiente tarea, agregarla
                var nuevastareas = new List<Workflow>();
                if (siguientedefinicionworkflow != null)
                {
                    //si la tarea fue rechazada y la tarea anterior es asignable. buscar la persona asignada
                    if (string.IsNullOrWhiteSpace(siguientedefinicionworkflow.UserId) && !aprobado)
                    {
                        var wfa = proceso.Workflows.OrderByDescending(q => q.WorkflowId).FirstOrDefault(q => q.DefinicionWorkflowId == siguientedefinicionworkflow.DefinicionWorkflowId);
                        if (wfa != null)
                            siguientedefinicionworkflow.UserId = wfa.UserId;
                    }
                    //verificar que exista un usuario para asignar tarea
                    if (string.IsNullOrWhiteSpace(workflow.NextUserId) && string.IsNullOrWhiteSpace(siguientedefinicionworkflow.UserId))
                    {
                        throw new Exception(string.Format("La siguiente tarea '{0}' no tiene un usuario asignado. Favor revise la definición del proceso.", siguientedefinicionworkflow.TipoWorkflow.Nombre));
                    }

                    //determinar lista de usuarios de la siguiente tarea
                    var listausuarios = !string.IsNullOrWhiteSpace(workflow.NextUserId) ? workflow.NextUserId.Split(',') : siguientedefinicionworkflow.UserId.Split(',');

                    //asignar tareas a la lista de usuarios
                    foreach (var userid in listausuarios)
                    {
                        //validar usuario
                        var user = context.Users.FirstOrDefault(q => q.Id == userid);
                        if (user == null)
                        {
                            throw new Exception("No se encontró el usuario id: " + userid);
                        }

                        //crear nueva tarea
                        var nuevoworkflow = new Workflow();
                        nuevoworkflow.FechaCreacion = DateTime.Now;
                        nuevoworkflow.TipoAprobacionId = (int)Infrastructure.Enum.TipoAprobacion.SinAprobacion;
                        nuevoworkflow.Terminada = false;
                        nuevoworkflow.DefinicionWorkflow = siguientedefinicionworkflow;
                        nuevoworkflow.Proceso = proceso;
                        nuevoworkflow.PerfilId = siguientedefinicionworkflow.PerfilId;
                        nuevoworkflow.UserId = userid;

                        //si es mas de 1 usuario agrupar las tareas y hacerlas paralelas
                        if (listausuarios.Count() > 1)
                        {
                            nuevoworkflow.WorkflowGrupoId = workflow.WorkflowId;
                        }

                        //agregar tarea
                        proceso.Workflows.Add(nuevoworkflow);

                        //acumular tarea para notificar
                        nuevastareas.Add(nuevoworkflow);
                    }
                }

                // si no existe siguiente tarea, terminar proceso
                if (siguientedefinicionworkflow == null)
                {
                    proceso.Terminada = true;
                    proceso.FechaTermino = DateTime.Now;
                }

                //guardar cambios
                context.SaveChanges();

                //notificar de tarea via correo
                if (nuevastareas.Any())
                {
                    foreach (var item in nuevastareas)
                    {
                        NotificarTarea(item.WorkflowId);
                    }
                }
            }
        }

        public void ProcessDefinitionUpdate(int DefinicionProcesoId, string ids)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var idarray = ids.Split(',').Where(q => !string.IsNullOrWhiteSpace(q.ToString())).ToArray();
                for (int i = 0; i < idarray.Count(); i++)
                {
                    int id = int.Parse(idarray[i]);
                    var dpd = context.DefinicionWorkflow.FirstOrDefault(q => q.DefinicionWorkflowId == id);
                    dpd.Secuencia = i;
                }

                context.SaveChanges();
            }
        }

        public void WorkflowAssign(Workflow w, List<Model.DTO.DTOUser> users)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                //actualizar workflow actual
                var workflow = context.Workflow.FirstOrDefault(q => q.WorkflowId == w.WorkflowId);
                if (workflow == null)
                {
                    throw new Exception("Debe especificar un workflow.");
                }

                //asignar lista de usuarios
                workflow.NextUserId = string.Join(",", users.Where(q => q.Selected).Select(q => q.Id).ToArray());

                context.SaveChanges();
            }
        }

        public void ProcesoDelete(int id)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {

                var proceso = context.Proceso.FirstOrDefault(q => q.ProcesoId == id);
                if (proceso == null)
                    throw new Exception("No se encontró el proceso.");

                foreach (var documento in context.Documento.Where(q => q.ProcesoId == proceso.ProcesoId))
                    context.Documento.Remove(documento);

                foreach (var workflow in context.Workflow.Where(q => q.ProcesoId == id))
                    foreach (var documento in context.Documento.Where(q => q.WorkflowId == workflow.WorkflowId))
                        context.Documento.Remove(documento);

                foreach (var workflow in context.Workflow.Where(q => q.ProcesoId == id))
                    context.Workflow.Remove(workflow);

                foreach (var art91 in context.Articulo91.Where(q => q.ProcesoId == proceso.ProcesoId))
                    context.Articulo91.Remove(art91);

                foreach (var hallazgo in context.Hallazgo.Where(q => q.Fiscalizacion.ProcesoId == proceso.ProcesoId))
                    context.Hallazgo.Remove(hallazgo);

                foreach (var fiscalizacion in context.Fiscalizacion.Where(q => q.ProcesoId == proceso.ProcesoId))
                    context.Fiscalizacion.Remove(fiscalizacion);

                context.Proceso.Remove(proceso);
                context.SaveChanges();
            }
        }

        public void DefinicionProcesoDelete(int id)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var definicionproceso = context.DefinicionProceso.FirstOrDefault(q => q.DefinicionProcesoId == id);
                if (definicionproceso == null)
                {
                    throw new Exception("No se encontró la definición de proceso.");
                }

                context.Workflow.RemoveRange(context.Workflow.Where(q => q.Proceso.DefinicionProcesoId == definicionproceso.DefinicionProcesoId));
                context.Documento.RemoveRange(context.Documento.Where(q => q.Workflow.Proceso.DefinicionProcesoId == definicionproceso.DefinicionProcesoId));
                context.Proceso.RemoveRange(context.Proceso.Where(q => q.DefinicionProcesoId == definicionproceso.DefinicionProcesoId));
                context.DefinicionWorkflow.RemoveRange(context.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == definicionproceso.DefinicionProcesoId));
                context.DefinicionProceso.Remove(definicionproceso);

                context.SaveChanges();
            }
        }

        public List<string> DefinicionWorkflowDeleteValidate(int id)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();
                var definicioworkflow = context.DefinicionWorkflow.FirstOrDefault(q => q.DefinicionWorkflowId == id);
                if (definicioworkflow == null)
                {
                    returnValue.Add("No se encontró la definición de workflow.");
                }

                if (definicioworkflow.Workflows.Any())
                {
                    returnValue.Add("No se puede eliminar está definición ya que existen tareas que se generaron a partir de ella.");
                }

                if (context.DefinicionWorkflow.Any(q => q.DefinicionWorkflowRechazoId == definicioworkflow.DefinicionWorkflowId))
                {
                    returnValue.Add("No se puede eliminar está definición ya que es la instancia de rechazo de otras tareas. Revise la definición de su proceso.");
                }

                context.DefinicionWorkflow.Remove(definicioworkflow);

                return returnValue;
            }
        }

        public void DefinicionWorkflowDelete(int id)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var definicioworkflow = context.DefinicionWorkflow.FirstOrDefault(q => q.DefinicionWorkflowId == id);
                if (definicioworkflow == null)
                {
                    throw new Exception("No se encontró la definición de workflow.");
                }

                context.Workflow.RemoveRange(context.Workflow.Where(q => q.DefinicionWorkflowId == definicioworkflow.DefinicionWorkflowId));
                context.DefinicionWorkflow.Remove(definicioworkflow);
                context.SaveChanges();
            }
        }

        public void WorkflowMove(int workflowid, string userid)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var workflow = context.Workflow.FirstOrDefault(q => q.WorkflowId == workflowid);
                if (workflow == null)
                {
                    throw new Exception("No se encontró el workflow.");
                }

                workflow.UserId = userid;
                workflow.Observacion = "Tarea reasignada por el administrador de sistema";

                context.SaveChanges();

                //notificar de tarea via correo
                NotificarTarea(workflow.WorkflowId);
            }
        }

        public void WorkflowDelete(int workflowid)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var workflow = context.Workflow.FirstOrDefault(q => q.WorkflowId == workflowid);
                if (workflow == null)
                {
                    throw new Exception("No se encontró el workflow.");
                }

                context.Documento.RemoveRange(context.Documento.Where(q => q.Workflow.WorkflowId == workflow.WorkflowId));
                context.Workflow.Remove(workflow);

                context.SaveChanges();
            }
        }

        public List<string> AccountDeleteValidate(string userid)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();
                var user = context.Users.FirstOrDefault(q => q.Id == userid);
                if (user == null)
                {
                    returnValue.Add("Debe especificar un usuario");
                }

                if (user.Workflows.Any(q => !q.Terminada))
                {
                    returnValue.Add("El usuario especificado no se puede eliminar ya que tiene tareas asignadas pendientes de ejecución");
                }

                return returnValue;
            }
        }

        public List<string> UpdateArticulo91(Articulo91 articulo91)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();
                var obj = context.Articulo91.FirstOrDefault(q => q.Articulo91Id == articulo91.Articulo91Id);
                if (obj != null)
                {
                    obj.OK = articulo91.OK;
                    context.SaveChanges();
                }
                return returnValue;
            }
        }

        public List<string> UpdateActaFiscalizacion(ActaFiscalizacion actaFiscalizacion)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var returnValue = new List<string>();

                var actafiscalizacion = context.ActaFiscalizacion.FirstOrDefault(q => q.ActaFiscalizacionId == actaFiscalizacion.ActaFiscalizacionId);
                if (actafiscalizacion != null)
                {
                    actafiscalizacion.CambioDireccion = actaFiscalizacion.CambioDireccion;
                    actafiscalizacion.ComunaId = actaFiscalizacion.ComunaId;
                    actafiscalizacion.DireccionActual = actaFiscalizacion.DireccionActual;
                    actafiscalizacion.Fecha = actaFiscalizacion.Fecha;
                    actafiscalizacion.FechaFiscalizacionInSitu = actaFiscalizacion.FechaFiscalizacionInSitu;
                    actafiscalizacion.FechaSalidaOficioAcreditacionRequerimientos = actaFiscalizacion.FechaSalidaOficioAcreditacionRequerimientos;
                    actafiscalizacion.GeneroGerenteId = actaFiscalizacion.GeneroGerenteId;
                    actafiscalizacion.GeneroRepresentanteLegalId = actaFiscalizacion.GeneroRepresentanteLegalId;
                    actafiscalizacion.Gerente = actaFiscalizacion.Gerente;
                    actafiscalizacion.NActaReunionFiscalizacionInSitu = actaFiscalizacion.NActaReunionFiscalizacionInSitu;
                    actafiscalizacion.NOficioAcreditacioRequerimientos = actaFiscalizacion.NOficioAcreditacioRequerimientos;
                    actafiscalizacion.ObservacionContable = actaFiscalizacion.ObservacionContable;
                    actafiscalizacion.ObservacionLegal = actaFiscalizacion.ObservacionLegal;
                    actafiscalizacion.RegionId = actaFiscalizacion.RegionId;
                    actafiscalizacion.RepresentanteLegal = actaFiscalizacion.RepresentanteLegal;
                    actafiscalizacion.RUT = actaFiscalizacion.RUT;
                    actafiscalizacion.VigenciaRepresentanteLegal = actaFiscalizacion.VigenciaRepresentanteLegal;

                    foreach (var item in actaFiscalizacion.ActaFiscalizacionFiscalizadorContables)
                    {
                        var fiscalizador = context.ActaFiscalizacionFiscalizadorContable.FirstOrDefault(q => q.ActaFiscalizacionFiscalizadorContableId == item.ActaFiscalizacionFiscalizadorContableId);
                        if (fiscalizador != null)
                        {
                            fiscalizador.Seleccionado = item.Seleccionado;
                        }
                    }

                    foreach (var item in actaFiscalizacion.ActaFiscalizacionFiscalizadorLegals)
                    {
                        var fiscalizador = context.ActaFiscalizacionFiscalizadorLegal.FirstOrDefault(q => q.ActaFiscalizacionFiscalizadorLegalId == item.ActaFiscalizacionFiscalizadorLegalId);
                        if (fiscalizador != null)
                        {
                            fiscalizador.Seleccionado = item.Seleccionado;
                        }
                    }

                    foreach (var item in actaFiscalizacion.ActaFiscalizacionHechoContables)
                    {
                        var acta = context.ActaFiscalizacionHechoContable.FirstOrDefault(q => q.ActaFiscalizacionHechoContableId == item.ActaFiscalizacionHechoContableId);
                        if (acta != null)
                        {
                            acta.Observacion = item.Observacion;
                            acta.Periodo = item.Periodo;
                            acta.Requerido = item.Requerido;
                            acta.File = item.File;

                            if (item.File != null)
                            {
                                var file = item.File;
                                var target = new MemoryStream();
                                file.InputStream.CopyTo(target);
                                acta.Archivo = target.ToArray();
                            }
                        }
                    }

                    foreach (var item in actaFiscalizacion.ActaFiscalizacionHechoLegals)
                    {
                        var acta = context.ActaFiscalizacionHechoLegal.FirstOrDefault(q => q.ActaFiscalizacionHechoLegalId == item.ActaFiscalizacionHechoLegalId);
                        if (acta != null)
                        {
                            acta.Observacion = item.Observacion;
                            acta.Requerido = item.Requerido;
                            acta.File = item.File;

                            if (item.File != null)
                            {
                                var file = item.File;
                                var target = new MemoryStream();
                                file.InputStream.CopyTo(target);
                                acta.Archivo = target.ToArray();
                            }
                        }
                    }

                    context.SaveChanges();
                }
                return returnValue;
            }
        }

        public bool NotificarProceso(Proceso proceso, string to)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                if (proceso == null)
                {
                    throw new Exception("Proceso no encontrado.");
                }

                if (string.IsNullOrWhiteSpace(to))
                {
                    throw new Exception("No se encontró el correo electrónico de notificación.");
                }

                var configcorreo = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.PlantillaCorreoNotificacionProceso);
                if (configcorreo == null)
                {
                    throw new Exception("No existe la plantilla de correos manual");
                }

                if (configcorreo != null && configcorreo.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La plantilla de correos manuales es inválida");
                }

                var configDiasManuales = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.DiasCertificadosManuales);
                if (configDiasManuales == null)
                {
                    throw new Exception("No existe la configuración de dias de respuesta");
                }

                if (configDiasManuales != null && configDiasManuales.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("la configuración de dias de respuesta es inválida");
                }

                configcorreo.Valor = configcorreo.Valor.Replace("[Id]", proceso.ProcesoId.ToString());
                configcorreo.Valor = configcorreo.Valor.Replace("[Tramite]", proceso.DefinicionProceso.Nombre);
                configcorreo.Valor = configcorreo.Valor.Replace("[FechaCreacion]", string.Format("{0:dd-MM-yyyy HH:mm:ss}", proceso.FechaCreacion));
                configcorreo.Valor = configcorreo.Valor.Replace("[FechaVencimiento]", string.Format("{0:dd-MM-yyyy HH:mm:ss}", proceso.FechaVencimiento));
                configcorreo.Valor = configcorreo.Valor.Replace("[Organizacion]", proceso.Organizacion.RazonSocial);

                emailMsg.IsBodyHtml = true;
                emailMsg.Body = configcorreo.Valor;

                var configAsunto = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.AsuntoCorreoNotificacion);
                if (configAsunto != null && !configAsunto.Valor.IsNullOrWhiteSpace())
                {
                    emailMsg.Subject = configAsunto.Valor;
                }

                emailMsg.To.Add(to);

                //si el proceso es una solicitud de certificado automático, adjuntar pdf
                if (proceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico)
                {
                    if (proceso.Documentos.Any())
                    {
                        emailMsg.Attachments.Add(new Attachment(new MemoryStream(proceso.Documentos.FirstOrDefault().Content), proceso.Documentos.FirstOrDefault().FileName));
                    }
                }

                try
                {
                    Send();
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarProceso", LogTimeUtc = DateTime.UtcNow, LogDetails = "Correo de notificación de proceso enviado a " + emailMsg.To.ToString() + " correctamente.", LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = emailMsg.Body });
                }
                catch (Exception ex)
                {
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarProceso", LogTimeUtc = DateTime.UtcNow, LogDetails = "Se detectó un problema al momento de notificar proceso a " + emailMsg.To.ToString(), LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = ex.Message });
                }

                return true;
            }
        }

        public bool NotificarTarea(int workflowid)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var workflow = context.Workflow.FirstOrDefault(q => q.WorkflowId == workflowid);
                if (workflow == null)
                {
                    throw new Exception("No se encontró el workflow especificado");
                }

                var configplantillanotificaciontarea = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.PLantillaNotificacionTarea);
                if (configplantillanotificaciontarea == null)
                {
                    throw new Exception("No existe la plantilla de notificación de tareas");
                }

                if (configplantillanotificaciontarea != null && configplantillanotificaciontarea.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La plantilla de notificación de tareas está vacia");
                }

                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Id]", workflow.WorkflowId.ToString());
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[FechaCreacion]", workflow.FechaCreacion.ToString());
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Tarea]", workflow.DefinicionWorkflow.TipoWorkflow.Nombre);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Proceso]", workflow.Proceso.DefinicionProceso.Nombre);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Registro]", workflow.Proceso.Organizacion.NumeroRegistro);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Organizacion]", workflow.Proceso.Organizacion.RazonSocial);

                emailMsg.IsBodyHtml = true;
                emailMsg.Body = configplantillanotificaciontarea.Valor;
                emailMsg.To.Clear();

                var configAsunto = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.AsuntoCorreoNotificacion);
                if (configAsunto != null && !configAsunto.Valor.IsNullOrWhiteSpace())
                {
                    emailMsg.Subject = configAsunto.Valor;
                }

                if (workflow.User != null)
                {
                    foreach (var to in workflow.User.EmailNotificacionTarea.Split(';'))
                    {
                        if (!to.IsNullOrWhiteSpace())
                        {
                            emailMsg.To.Add(to);
                        }
                    }

                    try
                    {
                        Send();
                        LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarTarea", LogTimeUtc = DateTime.UtcNow, LogDetails = "Correo de notificación de tarea enviado a " + emailMsg.To.ToString() + " correctamente.", LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = emailMsg.Body });
                    }
                    catch (Exception ex)
                    {
                        LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarTarea", LogTimeUtc = DateTime.UtcNow, LogDetails = "Se detectó un problema al momento de notificar tarea a " + emailMsg.To.ToString(), LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = ex.Message });
                    }
                }

                return true;
            }
        }

        public bool NotificarArchivo(int workflowid)
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var workflow = context.Workflow.FirstOrDefault(q => q.WorkflowId == workflowid);
                if (workflow == null)
                {
                    throw new Exception("No se encontró el workflow especificado");
                }

                var configplantillanotificaciontarea = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.PLantillaNotificacionTareaArchivada);
                if (configplantillanotificaciontarea == null)
                {
                    throw new Exception("No existe la plantilla de notificación de tareas archivadas");
                }

                if (configplantillanotificaciontarea != null && configplantillanotificaciontarea.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La plantilla de notificación de tareas archivadas está vacia");
                }

                var jefaturas = context.Users.Where(q => q.Habilitado && (q.PerfilId == (int)Infrastructure.Enum.Perfil.Jefatura || q.PerfilId == (int)Infrastructure.Enum.Perfil.Admininstrador));
                if (!jefaturas.Any())
                {
                    throw new Exception("No hay cuentas de usuario de jefatura o administradores a quienes informar");
                }

                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Id]", workflow.WorkflowId.ToString());
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[FechaCreacion]", workflow.FechaCreacion.ToString());
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Tarea]", workflow.DefinicionWorkflow.TipoWorkflow.Nombre);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Proceso]", workflow.Proceso.DefinicionProceso.Nombre);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Registro]", workflow.Proceso.Organizacion.NumeroRegistro);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[Organizacion]", workflow.Proceso.Organizacion.RazonSocial);
                configplantillanotificaciontarea.Valor = configplantillanotificaciontarea.Valor.Replace("[idproceso]", workflow.Proceso.ProcesoId.ToString());

                emailMsg.IsBodyHtml = true;
                emailMsg.Body = configplantillanotificaciontarea.Valor;
                emailMsg.To.Clear();

                var configAsunto = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.AsuntoCorreoNotificacion);
                if (configAsunto != null && !configAsunto.Valor.IsNullOrWhiteSpace())
                {
                    emailMsg.Subject = configAsunto.Valor;
                }

                foreach (var to in jefaturas.Select(q => q.Email))
                {
                    if (!to.IsNullOrWhiteSpace())
                    {
                        emailMsg.To.Add(to);
                    }
                }

                try
                {
                    Send();
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarTareaArchivada", LogTimeUtc = DateTime.UtcNow, LogDetails = "Correo de notificación de tarea archivada enviado a " + emailMsg.To.ToString() + " correctamente.", LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = emailMsg.Body });
                }
                catch (Exception ex)
                {
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarTareaArchivada", LogTimeUtc = DateTime.UtcNow, LogDetails = "Se detectó un problema al momento de notificar tarea archivada a " + emailMsg.To.ToString(), LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = ex.Message });
                }

                return true;
            }
        }

        public string GetNotificarProcesosPendientes()
        {
            using (SistemaIntegradoContext context = new SistemaIntegradoContext())
            {
                var procesos = context.Proceso.Where(q => !q.Terminada).ToList().Where(q => DateTime.Now.Date > q.FechaVencimiento.Date).ToList();

                if (!procesos.Any())
                {
                    return null;
                }

                var plantillainforme = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.PlantillaInformeProcesosAtrasados);
                if (plantillainforme == null)
                {
                    throw new Exception("No existe la plantilla de informe de procesos atrasados");
                }

                if (plantillainforme != null && plantillainforme.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La plantilla de informe de procesos atrasados está vacia");
                }

                var plantillainformeDetalle = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.PlantillaInformeProcesosAtrasadosDetalle);
                if (plantillainformeDetalle == null)
                {
                    throw new Exception("No existe la plantilla de informe de procesos atrasados");
                }

                if (plantillainformeDetalle != null && plantillainformeDetalle.Valor.IsNullOrWhiteSpace())
                {
                    throw new Exception("La plantilla de informe de procesos atrasados está vacia");
                }

                var jefaturas = context.Users.Where(q => q.Habilitado && (q.PerfilId == (int)Infrastructure.Enum.Perfil.Jefatura || q.PerfilId == (int)Infrastructure.Enum.Perfil.Admininstrador));
                if (!jefaturas.Any())
                {
                    throw new Exception("No hay cuentas de usuario de jefatura o administradores a quienes informar");
                }

                string detalle = string.Empty;

                foreach (var item in procesos)
                {
                    var tarea = item.Workflows.Where(q => !q.Terminada).OrderBy(q => q.WorkflowId);
                    if (!tarea.ToList().Any())
                    {
                        continue;
                    }

                    string temp = plantillainformeDetalle.Valor;
                    temp = temp.Replace("[Id]", item.ProcesoId.ToString());
                    temp = temp.Replace("[Tipo]", item.DefinicionProceso.Nombre);
                    temp = temp.Replace("[FechaCreacion]", string.Format("{0:dd/MM/yyyy HH:mm:ss}", item.FechaCreacion));
                    temp = temp.Replace("[FechaVencimiento]", string.Format("{0:dd/MM/yyyy HH:mm:ss}", item.FechaVencimiento));
                    temp = temp.Replace("[Organizacion]", item.Organizacion.RazonSocial);
                    temp = temp.Replace("[Registro]", item.Organizacion.NumeroRegistro);
                    temp = temp.Replace("[Tarea]", tarea != null ? tarea.FirstOrDefault().DefinicionWorkflow.TipoWorkflow.Nombre : string.Empty);
                    temp = temp.Replace("[Funcionario]", tarea != null && tarea.FirstOrDefault().User != null ? tarea.FirstOrDefault().User.UserName : string.Empty);
                    detalle = string.Concat(detalle, temp);
                }

                plantillainforme.Valor = plantillainforme.Valor.Replace("[Procesos]", procesos.Count().ToString());
                plantillainforme.Valor = plantillainforme.Valor.Replace("[Detalle]", detalle);

                emailMsg.IsBodyHtml = true;
                emailMsg.Priority = MailPriority.High;
                emailMsg.Body = plantillainforme.Valor;
                emailMsg.To.Clear();

                var configAsunto = context.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.AsuntoCorreoNotificacion);
                if (configAsunto != null && !configAsunto.Valor.IsNullOrWhiteSpace())
                {
                    emailMsg.Subject = configAsunto.Valor;
                }

                foreach (var to in jefaturas)
                {
                    if (!to.Email.IsNullOrWhiteSpace())
                    {
                        emailMsg.To.Add(to.Email);
                    }
                }

                try
                {
                    Send();
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarProcesosPendientes", LogTimeUtc = DateTime.UtcNow, LogDetails = "Correo de reporte de procesos pendientes enviado a " + emailMsg.To.ToString() + " correctamente.", LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = emailMsg.Body });
                }
                catch (Exception ex)
                {
                    LogAdd(new Log() { LogId = Guid.NewGuid(), LogTimeLocal = DateTime.Now, LogAreaAccessed = "NotificarProcesosPendientes", LogTimeUtc = DateTime.UtcNow, LogDetails = "Se detectó un problema al momento de notificar procesos pendientes a " + emailMsg.To.ToString(), LogIpAddress = Dns.GetHostName(), LogUserName = Environment.UserName, LogContent = ex.Message });
                }

                return plantillainforme.Valor;
            }
        }
    }
}