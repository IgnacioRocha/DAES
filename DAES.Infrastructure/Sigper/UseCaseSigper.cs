using System;
using System.Collections.Generic;
using System.Linq;
using DAES.Model.Sigper;
using DAES.Infrastructure.Interfaces;

namespace DAES.Infrastructure.Sigper
{
    public class UseCaseSigper : ISIGPER
    {
        private string criterioExclusionUnidad = "COMISIONADO";

        public PLUNILAB GetUnidad(int codigo)
        {
            using (AppContextEconomia context = new AppContextEconomia())
            {
                if (context.PLUNILAB.AsNoTracking().Any(q => q.Pl_UndCod == codigo))
                    return context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigo);
            }
            using (AppContextTurismo context = new AppContextTurismo())
            {
                if (context.PLUNILAB.AsNoTracking().Any(q => q.Pl_UndCod == codigo))
                    return context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigo);
            }

            return null;
        }
        public SIGPER GetUserByEmail(string email)
        {
            var sigper = new SIGPER();

            using (var context = new AppContextEconomia())
            {
                var funcionario = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.Rh_Mail == email && q.RH_EstLab == "A");
                if (funcionario != null)
                {
                    sigper.Funcionario = funcionario;

                    //jefatura del funcionario
                    var jefatura = (from f in context.PEFERJEFAF
                                    join j in context.PEFERJEFAJ on f.PeFerJerCod equals j.PeFerJerCod
                                    join p in context.PEDATPER on j.FyPFunARut equals p.RH_NumInte
                                    where f.FyPFunRut == funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where j.PeFerJerAutEst == 1
                                    select p).FirstOrDefault();

                    if (jefatura != null)
                        sigper.Jefatura = jefatura;

                    //datos laborales del funcionario
                    var datosLaborales = context.PeDatLab.AsNoTracking().Where(q => q.RH_NumInte == funcionario.RH_NumInte).OrderByDescending(q => q.RhConIni).FirstOrDefault();
                    if (datosLaborales != null)
                        sigper.DatosLaborales = datosLaborales;

                    var codigoUnidad = (from u in context.ReContra
                                        join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                        where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                        where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                        where u.Re_ConIni == (from ud in context.ReContra
                                                              where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                              select ud.Re_ConIni).Max()
                                        select u).FirstOrDefault();

                    if (codigoUnidad != null)
                    {
                        /*unidad del funcionario*/
                        var unidad = context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigoUnidad.Re_ConUni);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase) && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }
                    }

                    /*se obtienen datos laborales desde tabla ReContra*/
                    var contrato = (from rc in context.ReContra
                                    join p in context.PEDATPER on rc.RH_NumInte equals p.RH_NumInte
                                    join pl in context.PLUNILAB on rc.Re_ConUni equals pl.Pl_UndCod
                                    join e in context.DGESCALAFONES on rc.Re_ConEsc equals e.Pl_CodEsc
                                    join co in context.DGCONTRATOS on rc.RH_ContCod equals co.RH_ContCod
                                    join pc in context.PECARGOS on rc.Re_ConCar equals pc.Pl_CodCar
                                    join es in context.DGESTAMENTOS on rc.ReContraEst equals es.DgEstCod
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where rc.Re_ConIni == (from ud in context.ReContra
                                                           where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                           select ud.Re_ConIni).Max()
                                    select rc).FirstOrDefault();

                    if (contrato != null)
                        sigper.Contrato = contrato;

                    sigper.SubSecretaria = "ECONOMIA";

                    if (sigper.Unidad != null && !string.IsNullOrWhiteSpace(sigper.Unidad.Pl_UndDes) && !sigper.Unidad.Pl_UndDes.ToUpper().Contains(criterioExclusionUnidad))
                        return sigper;
                }
            }

            using (var context = new AppContextTurismo())
            {
                var funcionario = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.Rh_Mail == email && q.RH_EstLab == "A");
                if (funcionario != null)
                {
                    sigper.Funcionario = funcionario;

                    //jefatura del funcionario
                    var jefatura = (from f in context.PEFERJEFAF
                                    join j in context.PEFERJEFAJ on f.PeFerJerCod equals j.PeFerJerCod
                                    join p in context.PEDATPER on j.FyPFunARut equals p.RH_NumInte
                                    where f.FyPFunRut == funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A")
                                    where j.PeFerJerAutEst == 1
                                    where p.Rh_MailPer == null
                                    select p).FirstOrDefault();

                    if (jefatura != null)
                        sigper.Jefatura = jefatura;

                    //datos laborales del funcionario
                    var datosLaborales = context.PeDatLab.AsNoTracking().Where(q => q.RH_NumInte == funcionario.RH_NumInte).OrderByDescending(q => q.RhConIni).FirstOrDefault();
                    if (datosLaborales != null)
                        sigper.DatosLaborales = datosLaborales;

                    var codigoUnidad = (from u in context.ReContra
                                        join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                        where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                        where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                        where u.Re_ConIni == (from ud in context.ReContra
                                                              where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                              select ud.Re_ConIni).Max()
                                        select u).FirstOrDefault();

                    if (codigoUnidad != null)
                    {
                        //unidad del funcionario
                        var unidad = context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigoUnidad.Re_ConUni);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_EstLab.Equals("A") && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }
                    }

                    /*se obtienen datos laborales desde tabla ReContra*/
                    var contrato = (from rc in context.ReContra
                                    join p in context.PEDATPER on rc.RH_NumInte equals p.RH_NumInte
                                    join pl in context.PLUNILAB on rc.Re_ConUni equals pl.Pl_UndCod
                                    join e in context.DGESCALAFONES on rc.Re_ConEsc equals e.Pl_CodEsc
                                    join co in context.DGCONTRATOS on rc.RH_ContCod equals co.RH_ContCod
                                    join pc in context.PECARGOS on rc.Re_ConCar equals pc.Pl_CodCar
                                    join es in context.DGESTAMENTOS on rc.ReContraEst equals es.DgEstCod
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where rc.Re_ConIni == (from ud in context.ReContra
                                                           where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                           select ud.Re_ConIni).Max()
                                    select rc).FirstOrDefault();

                    if (contrato != null)
                        sigper.Contrato = contrato;


                    sigper.SubSecretaria = "TURISMO";

                    return sigper;
                }
            }

            return sigper;
        }
        public List<PLUNILAB> GetUnidades()
        {
            var returnValue = new List<PLUNILAB>();

            using (var context = new AppContextEconomia())
            {
                var unidades = context.PLUNILAB.AsNoTracking().Where(q => !q.Pl_UndDes.Contains(criterioExclusionUnidad)).ToList().Select(q => new PLUNILAB { Pl_UndCod = q.Pl_UndCod, Pl_UndDes = q.Pl_UndDes.Trim() + " (ECONOMIA)" });
                foreach (var item in unidades)
                {
                    /*excluir las unidades sin funcionarios*/
                    var funcionarios = from PE in context.PEDATPER
                                       join r in context.PeDatLab on PE.RH_NumInte equals r.RH_NumInte
                                       where PE.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                       where r.RhConUniCod == item.Pl_UndCod
                                       where r.PeDatLabAdDocCor == (from ud in context.PeDatLab
                                                                    where ud.RH_NumInte == PE.RH_NumInte
                                                                    select ud.PeDatLabAdDocCor).Max()
                                       select PE;
                    if (funcionarios.Any())
                        returnValue.Add(item);
                }
            }
            using (var context = new AppContextTurismo())
            {
                //excluir las unidades sin funcionarios
                //excluir unidad 1 de turismo
                var unidades = context.PLUNILAB.AsNoTracking().Where(q => !q.Pl_UndDes.Contains(criterioExclusionUnidad) && q.Pl_UndCod != 1).ToList().Select(q => new PLUNILAB { Pl_UndCod = q.Pl_UndCod, Pl_UndDes = q.Pl_UndDes.Trim() + " (TURISMO)" });
                foreach (var item in unidades)
                {
                    //excluir las unidades sin funcionarios
                    var funcionarios = from PE in context.PEDATPER
                                       join r in context.PeDatLab on PE.RH_NumInte equals r.RH_NumInte
                                       where PE.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                       where r.RhConUniCod == item.Pl_UndCod
                                       where r.PeDatLabAdDocCor == (from ud in context.PeDatLab
                                                                    where ud.RH_NumInte == PE.RH_NumInte
                                                                    select ud.PeDatLabAdDocCor).Max()
                                       select PE;

                    if (funcionarios.Any())
                        returnValue.Add(item);
                }
            }

            return returnValue.OrderBy(q => q.Pl_UndDes).ToList();
        }
        public DAES.Model.Sigper.SIGPER GetUserByRut(int rut)
        {
            var sigper = new DAES.Model.Sigper.SIGPER();

            using (var context = new AppContextEconomia())
            {
                var funcionario = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_NumInte == rut && q.RH_EstLab == "A");
                if (funcionario != null)
                {
                    sigper.Funcionario = funcionario;

                    //jefatura del funcionario
                    var jefatura = (from f in context.PEFERJEFAF
                                    join j in context.PEFERJEFAJ on f.PeFerJerCod equals j.PeFerJerCod
                                    join p in context.PEDATPER on j.FyPFunARut equals p.RH_NumInte
                                    where f.FyPFunRut == funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where j.PeFerJerAutEst == 1
                                    select p).FirstOrDefault();

                    if (jefatura != null)
                        sigper.Jefatura = jefatura;

                    //datos laborales del funcionario
                    var datosLaborales = context.PeDatLab.AsNoTracking().Where(q => q.RH_NumInte == funcionario.RH_NumInte).OrderByDescending(q => q.RhConIni).FirstOrDefault();
                    if (datosLaborales != null)
                        sigper.DatosLaborales = datosLaborales;

                    var codigoUnidad = (from u in context.ReContra
                                        join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                        where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                        where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                        where u.Re_ConIni == (from ud in context.ReContra
                                                              where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                              select ud.Re_ConIni).Max()
                                        select u).FirstOrDefault();

                    if (codigoUnidad != null)
                    {
                        //unidad del funcionario
                        var unidad = context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigoUnidad.Re_ConUni);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase) && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }
                    }

                    /*se obtienen datos laborales desde tabla ReContra*/
                    var contrato = (from rc in context.ReContra
                                    join p in context.PEDATPER on rc.RH_NumInte equals p.RH_NumInte
                                    join pl in context.PLUNILAB on rc.Re_ConUni equals pl.Pl_UndCod
                                    join e in context.DGESCALAFONES on rc.Re_ConEsc equals e.Pl_CodEsc
                                    join co in context.DGCONTRATOS on rc.RH_ContCod equals co.RH_ContCod
                                    join pc in context.PECARGOS on rc.Re_ConCar equals pc.Pl_CodCar
                                    join es in context.DGESTAMENTOS on rc.ReContraEst equals es.DgEstCod
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where rc.Re_ConIni == (from ud in context.ReContra
                                                           where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                           select ud.Re_ConIni).Max()
                                    select rc).FirstOrDefault();

                    if (contrato != null)
                        sigper.Contrato = contrato;

                    sigper.SubSecretaria = "ECONOMIA";

                    if (sigper.Unidad != null && !string.IsNullOrWhiteSpace(sigper.Unidad.Pl_UndDes) && !sigper.Unidad.Pl_UndDes.ToUpper().Contains(criterioExclusionUnidad))
                        return sigper;
                }
            }

            using (var context = new AppContextTurismo())
            {
                var funcionario = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_NumInte == rut && q.RH_EstLab == "A");
                if (funcionario != null)
                {
                    sigper.Funcionario = funcionario;

                    //jefatura del funcionario
                    var jefatura = (from f in context.PEFERJEFAF
                                    join j in context.PEFERJEFAJ on f.PeFerJerCod equals j.PeFerJerCod
                                    join p in context.PEDATPER on j.FyPFunARut equals p.RH_NumInte
                                    where f.FyPFunRut == funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where j.PeFerJerAutEst == 1
                                    select p).FirstOrDefault();

                    if (jefatura != null)
                        sigper.Jefatura = jefatura;

                    //datos laborales del funcionario
                    var datosLaborales = context.PeDatLab.AsNoTracking().Where(q => q.RH_NumInte == funcionario.RH_NumInte).AsEnumerable().LastOrDefault();
                    if (datosLaborales != null)
                        sigper.DatosLaborales = datosLaborales;

                    var CodUnidad = (from u in context.ReContra
                                     join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                     where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                     where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                     where u.Re_ConIni == (from ud in context.ReContra
                                                           where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                           select ud.Re_ConIni).Max()
                                     select u).FirstOrDefault();

                    if (CodUnidad != null)
                    {
                        //unidad del funcionario
                        var unidad = context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == CodUnidad.Re_ConUni);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase) && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }
                    }

                    /*se obtienen datos laborales desde tabla ReContra*/
                    var contrato = (from rc in context.ReContra
                                    join p in context.PEDATPER on rc.RH_NumInte equals p.RH_NumInte
                                    join pl in context.PLUNILAB on rc.Re_ConUni equals pl.Pl_UndCod
                                    join e in context.DGESCALAFONES on rc.Re_ConEsc equals e.Pl_CodEsc
                                    join co in context.DGCONTRATOS on rc.RH_ContCod equals co.RH_ContCod
                                    join pc in context.PECARGOS on rc.Re_ConCar equals pc.Pl_CodCar
                                    join es in context.DGESTAMENTOS on rc.ReContraEst equals es.DgEstCod
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where rc.Re_ConIni == (from ud in context.ReContra
                                                           where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                           select ud.Re_ConIni).Max()
                                    select rc).FirstOrDefault();

                    if (contrato != null)
                        sigper.Contrato = contrato;

                    sigper.SubSecretaria = "TURISMO";

                    return sigper;
                }
            }

            return sigper;
        }
        public List<PEDATPER> GetUserByUnidad(int codigoUnidad)
        {
            var returnValue = new List<PEDATPER>();

            using (var context = new AppContextEconomia())
            {
                if (context.PLUNILAB.AsNoTracking().Any(q => q.Pl_UndCod == codigoUnidad))
                {
                    var data = (from ReContra in context.ReContra
                                join PEDATPER in context.PEDATPER on ReContra.RH_NumInte equals PEDATPER.RH_NumInte
                                where PEDATPER.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                where ReContra.Re_ConUni == codigoUnidad
                                where ReContra.Re_ConIni == (from ud in context.ReContra where ud.RH_NumInte == PEDATPER.RH_NumInte select ud.Re_ConIni).Max()
                                select PEDATPER);

                    returnValue.AddRange(data.ToList());
                }
            }
            using (var context = new AppContextTurismo())
            {
                if (context.PLUNILAB.AsNoTracking().Any(q => q.Pl_UndCod == codigoUnidad))
                {
                    var data = (from ReContra in context.ReContra
                                join PEDATPER in context.PEDATPER on ReContra.RH_NumInte equals PEDATPER.RH_NumInte
                                where PEDATPER.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                where ReContra.Re_ConUni == codigoUnidad
                                where ReContra.Re_ConIni == (from ud in context.ReContra where ud.RH_NumInte == PEDATPER.RH_NumInte select ud.Re_ConIni).Max()
                                select PEDATPER);

                    returnValue.AddRange(data.ToList());
                }
            }

            return returnValue;

        }
        public List<PEDATPER> GetUserByUnidadWithoutHonorarios(int codigoUnidad)
        {
            var returnValue = new List<PEDATPER>();

            using (var context = new AppContextEconomia())
            {
                if (context.PLUNILAB.AsNoTracking().Any(q => q.Pl_UndCod == codigoUnidad))
                {
                    var data = (from ReContra in context.ReContra
                                join PEDATPER in context.PEDATPER on ReContra.RH_NumInte equals PEDATPER.RH_NumInte
                                where PEDATPER.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                where ReContra.Re_ConUni == codigoUnidad
                                where ReContra.RH_ContCod != 10 /*distinto de honorarios*/
                                where ReContra.Re_ConIni == (from ud in context.ReContra where ud.RH_NumInte == PEDATPER.RH_NumInte select ud.Re_ConIni).Max()
                                select PEDATPER);

                    returnValue.AddRange(data.ToList());
                }
            }
            using (var context = new AppContextTurismo())
            {
                if (context.PLUNILAB.AsNoTracking().Any(q => q.Pl_UndCod == codigoUnidad))
                {
                    var data = (from ReContra in context.ReContra
                                join PEDATPER in context.PEDATPER on ReContra.RH_NumInte equals PEDATPER.RH_NumInte
                                where PEDATPER.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                where ReContra.Re_ConUni == codigoUnidad
                                where ReContra.RH_ContCod != 10
                                where ReContra.Re_ConIni == (from ud in context.ReContra where ud.RH_NumInte == PEDATPER.RH_NumInte select ud.Re_ConIni).Max()
                                select PEDATPER);

                    returnValue.AddRange(data.ToList());
                }
            }

            return returnValue;

        }
        public List<PEDATPER> GetAllUsers()
        {
            var returnValue = new List<PEDATPER>();

            using (var dbE = new AppContextEconomia())
            {
                //var rE = from PE in dbE.PEDATPER
                var rE = from PE in dbE.PEDATPER
                         join r in dbE.ReContra on PE.RH_NumInte equals r.RH_NumInte
                         //from r in dbE.ReContra
                         where r.Re_ConPyt != 0
                         where r.Re_ConIni.Year >= DateTime.Now.Year || r.RH_ContCod == 1
                         //where r.ReContraSed != 0
                         where r.Re_ConCar != 21
                         //where r.Re_ConTipHon != 1
                         from PL in dbE.PLUNILAB
                         where (PL.Pl_UndCod == PE.RhSegUnd01.Value || PL.Pl_UndCod == PE.RhSegUnd02.Value || PL.Pl_UndCod == PE.RhSegUnd03.Value)
                         //where PL.Pl_UndCod == codigoUnidad
                         where PE.RH_EstLab.Equals("A")
                         select PE;
                returnValue.AddRange(rE.ToList());

            }

            using (var dbT = new AppContextTurismo())
            {
                //var rT = from PE in dbT.PEDATPER
                var rT = from PE in dbT.PEDATPER
                         join r in dbT.ReContra on PE.RH_NumInte equals r.RH_NumInte
                         where r.Re_ConPyt != 0
                         where r.Re_ConIni.Year >= DateTime.Now.Year || r.RH_ContCod == 1
                         //where r.ReContraSed != 0
                         where r.Re_ConCar != 21
                         //where r.Re_ConTipHon != 1
                         from PL in dbT.PLUNILAB
                         where (PL.Pl_UndCod == PE.RhSegUnd01.Value || PL.Pl_UndCod == PE.RhSegUnd02.Value || PL.Pl_UndCod == PE.RhSegUnd03.Value)
                         //where PL.Pl_UndCod == codigoUnidad
                         where PE.RH_EstLab.Equals("A")
                         select PE;
                returnValue.AddRange(rT.ToList());
            }

            return returnValue.OrderBy(q => q.PeDatPerChq).ToList();
        }
        public List<PEDATPER> GetUserByTerm(string term)
        {
            var returnValue = new List<PEDATPER>();

            using (var context = new AppContextEconomia())
            {
                returnValue.AddRange(context.PEDATPER.AsNoTracking().Where(q => q.RH_EstLab.Equals("A") && (q.PeDatPerChq.ToLower().Contains(term.ToLower())) || (q.Rh_Mail.ToLower().Contains(term.ToLower()))));
            }
            using (var context = new AppContextTurismo())
            {
                returnValue.AddRange(context.PEDATPER.AsNoTracking().Where(q => q.RH_EstLab.Equals("A") && (q.PeDatPerChq.ToLower().Contains(term.ToLower())) || (q.Rh_Mail.ToLower().Contains(term.ToLower()))));
            }

            return returnValue.OrderBy(q => q.PeDatPerChq).ToList();
        }
        public DAES.Model.Sigper.SIGPER GetSecretariaByUnidad(int codigo)
        {
            using (var context = new AppContextEconomia())
            {
                //unidad del funcionario
                var unidad = context.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigo);
                if (unidad != null && !string.IsNullOrEmpty(unidad.Pl_UndNomSec))
                {
                    //secretaria del funcionario
                    var secretaria = context.PEDATPER.AsNoTracking().FirstOrDefault(q => q.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase) && q.PeDatPerChq.ToUpper().Trim() == unidad.Pl_UndNomSec.ToUpper().Trim());
                    if (secretaria != null)
                        return GetUserByRut(secretaria.RH_NumInte);
                }
            }

            return null;
        }
        public List<DGREGIONES> GetRegion()
        {
            using (var context = new AppContextEconomia())
            {
                return context.DGREGIONES.ToList();
            }
        }
        public List<DGCOMUNAS> GetDGCOMUNAs()
        {
            using (var context = new AppContextEconomia())
            {
                return context.DGCOMUNAS.ToList();
            }
        }
        public List<DGESCALAFONES> GetGESCALAFONEs()
        {
            using (var context = new AppContextEconomia())
            {
                return context.DGESCALAFONES.ToList();
            }
        }
        public List<DGCONTRATOS> GetDGCONTRATOS()
        {
            using (var context = new AppContextEconomia())
            {
                return context.DGCONTRATOS.ToList();
            }
        }
        public List<PECARGOS> GetPECARGOs()
        {
            using (var context = new AppContextEconomia())
            {
                return context.PECARGOS.ToList();
            }
        }
        public List<DGESTAMENTOS> GetDGESTAMENTOs()
        {
            using (var context = new AppContextEconomia())
            {
                return context.DGESTAMENTOS.ToList();
            }
        }
        public List<ReContra> GetReContra()
        {
            using (var context = new AppContextEconomia())
            {
                return context.ReContra.Where(c => c.ReContraSed != 0 /*&& c.Re_ConIni >= Convert.ToDateTime("2019-01-01")*/).ToList();
            }
        }
        public List<REPYT> GetREPYTs()
        {
            using (var context = new AppContextEconomia())
            {
                return context.REPYT.Where(c => c.RePytEst == "S").ToList();
            }
        }
        public string GetRegionbyComuna(string codComuna)
        {
            using (var dbEconomia = new AppContextEconomia())
            {
                var region = dbEconomia.DGCOMUNAS.Where(c => c.Pl_CodCom == codComuna).FirstOrDefault().Pl_CodReg;
                var regionnombre = dbEconomia.DGREGIONES.Where(r => r.Pl_CodReg == region).FirstOrDefault().Pl_DesReg;
                return regionnombre;
            }
        }
        public List<DGCOMUNAS> GetComunasbyRegion(string IdRegion)
        {
            using (var context = new AppContextEconomia())
            {
                return context.DGCOMUNAS.Where(c => c.Pl_CodReg == IdRegion).ToList();
            }
        }
        public List<PEDATPER> GetAllUsersForCometido()
        {
            var returnValue = new List<PEDATPER>();

            using (var dbE = new AppContextEconomia())
            {
                var rE = from PE in dbE.PEDATPER
                             //join r in dbE.ReContra on PE.RH_NumInte equals r.RH_NumInte
                             //where r.Re_ConPyt != 0
                             //where r.Re_ConIni.Year >= DateTime.Now.Year || r.RH_ContCod == 1
                             //where r.Re_ConCar != 21
                             //from PL in dbE.PLUNILAB
                             //where (PL.Pl_UndCod == PE.RhSegUnd01.Value || PL.Pl_UndCod == PE.RhSegUnd02.Value || PL.Pl_UndCod == PE.RhSegUnd03.Value)
                         where PE.RH_EstLab.Equals("A")
                         select PE;
                returnValue.AddRange(rE.ToList());

            }

            /*solo se dejan los funcionarios de economia, ya que algunos estan duplicados en turismo  22092020*/
            //using (var dbT = new AppContextTurismo())
            //{
            //    var rT = from PE in dbT.PEDATPER
            //             //join r in dbT.ReContra on PE.RH_NumInte equals r.RH_NumInte
            //             //where r.Re_ConPyt != 0
            //             //where r.Re_ConIni.Year >= DateTime.Now.Year || r.RH_ContCod == 1
            //             //where r.Re_ConCar != 21
            //             //from PL in dbT.PLUNILAB
            //             //where (PL.Pl_UndCod == PE.RhSegUnd01.Value || PL.Pl_UndCod == PE.RhSegUnd02.Value || PL.Pl_UndCod == PE.RhSegUnd03.Value)
            //             where PE.RH_EstLab.Equals("A")
            //             select PE;
            //    returnValue.AddRange(rT.ToList());
            //}

            return returnValue.OrderBy(q => q.PeDatPerChq).ToList();
        }
        public List<PLUNILAB> GetUnidadesFirmantes(List<string> listEmailFirmantes)
        {
            var returnValue = new List<PLUNILAB>();

            foreach (var email in listEmailFirmantes)
            {
                var user = GetUserByEmail(email);
                if (user != null && user.Unidad != null && !returnValue.Any(q => q.Pl_UndCod == user.Unidad.Pl_UndCod))
                    //returnValue.Add(GetUserByEmail(email).Unidad);
                    returnValue.Add(user.Unidad);
            }

            return returnValue.OrderBy(q => q.Pl_UndDes).ToList();
        }
        public List<PEDATPER> GetUserFirmanteByUnidad(int codigoUnidad, List<string> listEmailFirmantes)
        {
            var returnValue = new List<PEDATPER>();

            var funcionarios = GetUserByUnidad(codigoUnidad);
            foreach (var funcionario in funcionarios.Where(q => listEmailFirmantes.Contains(q.Rh_Mail.Trim())))
                returnValue.Add(funcionario);

            return returnValue;
        }
        public DAES.Model.Sigper.SIGPER NewGetUserByEmail(string email)
        {
            var sigper = new DAES.Model.Sigper.SIGPER();

            using (var context = new AppContextEconomia())
            {
                var funcionario = context.PEDATPER.FirstOrDefault(q => q.Rh_Mail == email && q.RH_EstLab == "A");
                if (funcionario != null)
                {
                    sigper.Funcionario = funcionario;

                    //jefatura del funcionario
                    var jefatura = (from f in context.PEFERJEFAF
                                    join j in context.PEFERJEFAJ on f.PeFerJerCod equals j.PeFerJerCod
                                    join p in context.PEDATPER on j.FyPFunARut equals p.RH_NumInte
                                    where f.FyPFunRut == funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                    where j.PeFerJerAutEst == 1
                                    //where p.Rh_MailPer == null
                                    select p).FirstOrDefault();

                    if (jefatura != null)
                        sigper.Jefatura = jefatura;

                    //datos laborales del funcionario
                    var PeDatLab = context.PeDatLab.Where(q => q.RH_NumInte == funcionario.RH_NumInte && q.RH_ContCod != 13 && (q.RhConIni.Value.Year == DateTime.Now.Year || q.RH_ContCod == 1)).OrderByDescending(q => q.RH_Correla).FirstOrDefault();
                    if (PeDatLab != null)
                    {
                        var CodUnidad = (from u in context.PeDatLab
                                         join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                         where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                         where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                         where u.PeDatLabAdDocCor == (from ud in context.PeDatLab
                                                                      where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                                      select ud.PeDatLabAdDocCor).Max()
                                         select u.RhConUniCod).FirstOrDefault();

                        /*unidad del funcionario*/
                        var unidad = context.PLUNILAB.FirstOrDefault(q => q.Pl_UndCod == CodUnidad);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.FirstOrDefault(q => q.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase) && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }

                        /*datos laborales funcionario*/
                        var FunDatosLaborales = context.PeDatLab.Where(q => q.RH_NumInte == funcionario.RH_NumInte && q.RH_ContCod != 13 && (q.RhConIni.Value.Year == DateTime.Now.Year || q.RH_ContCod == 1)).OrderByDescending(q => q.RH_Correla).FirstOrDefault();

                        if (FunDatosLaborales != null)
                        {
                            sigper.DatosLaborales = FunDatosLaborales;
                        }

                        /*se obtienen datos laborales desde tabla ReContra*/
                        var datosLaborales = (from rc in context.ReContra
                                              join p in context.PEDATPER on rc.RH_NumInte equals p.RH_NumInte
                                              join pl in context.PLUNILAB on rc.Re_ConUni equals pl.Pl_UndCod
                                              join e in context.DGESCALAFONES on rc.Re_ConEsc equals e.Pl_CodEsc
                                              join co in context.DGCONTRATOS on rc.RH_ContCod equals co.RH_ContCod
                                              join pc in context.PECARGOS on rc.Re_ConCar equals pc.Pl_CodCar
                                              join es in context.DGESTAMENTOS on rc.ReContraEst equals es.DgEstCod
                                              where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                              where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                              where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                              where rc.Re_ConIni == (from ud in context.ReContra
                                                                     where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                                     select ud.Re_ConIni).Max()
                                              select rc).FirstOrDefault();

                        if (datosLaborales != null)
                        {
                            sigper.Contrato = datosLaborales;
                        }

                    }
                    else
                    {
                        PeDatLab = context.PeDatLab.Where(q => q.RH_NumInte == funcionario.RH_NumInte && q.RH_ContCod != 13 && (q.RhConIni.Value.Year == DateTime.Now.Year || q.RH_ContCod == 1)).OrderByDescending(q => q.RH_Correla).FirstOrDefault();

                        var CodUnidad = (from u in context.PeDatLab
                                         join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                         where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                         where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                         where u.PeDatLabAdDocCor == (from ud in context.PeDatLab
                                                                      where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                                      select ud.PeDatLabAdDocCor).Max()
                                         select u.RhConUniCod).FirstOrDefault();


                        //unidad del funcionario
                        var unidad = context.PLUNILAB.FirstOrDefault(q => q.Pl_UndCod == CodUnidad);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.FirstOrDefault(q => q.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase) && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }

                        /*datos laborales funcionario*/
                        var FunDatosLaborales = context.PeDatLab.Where(q => q.RH_NumInte == funcionario.RH_NumInte && q.RH_ContCod != 13 && (q.RhConIni.Value.Year == DateTime.Now.Year || q.RH_ContCod == 1)).OrderByDescending(q => q.RH_Correla).FirstOrDefault();
                        if (FunDatosLaborales != null)
                        {
                            sigper.DatosLaborales = FunDatosLaborales;
                        }
                    }

                    sigper.SubSecretaria = "ECONOMIA";

                    return sigper;
                }
            }

            using (var context = new AppContextTurismo())
            {
                var funcionario = context.PEDATPER.FirstOrDefault(q => q.Rh_Mail == email);
                if (funcionario != null)
                {
                    sigper.Funcionario = funcionario;

                    //jefatura del funcionario
                    var jefatura = (from f in context.PEFERJEFAF
                                    join j in context.PEFERJEFAJ on f.PeFerJerCod equals j.PeFerJerCod
                                    join p in context.PEDATPER on j.FyPFunARut equals p.RH_NumInte
                                    where f.FyPFunRut == funcionario.RH_NumInte
                                    where p.RH_EstLab.Equals("A")
                                    where j.PeFerJerAutEst == 1
                                    where p.Rh_MailPer == null
                                    select p).FirstOrDefault();

                    if (jefatura != null)
                        sigper.Jefatura = jefatura;

                    //datos laborales del funcionario
                    var PeDatLab = context.PeDatLab.Where(q => q.RH_NumInte == funcionario.RH_NumInte && q.RH_ContCod != 13 && (q.RhConIni.Value.Year == DateTime.Now.Year || q.RH_ContCod == 1)).OrderByDescending(q => q.RH_Correla).FirstOrDefault();
                    if (PeDatLab != null)
                    {
                        var CodUnidad = (from u in context.PeDatLab
                                         join p in context.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                         where p.RH_EstLab.Equals("A", StringComparison.InvariantCultureIgnoreCase)
                                         where p.RH_NumInte == sigper.Funcionario.RH_NumInte
                                         where u.PeDatLabAdDocCor == (from ud in context.PeDatLab
                                                                      where ud.RH_NumInte == sigper.Funcionario.RH_NumInte
                                                                      select ud.PeDatLabAdDocCor).Max()
                                         select u.RhConUniCod).FirstOrDefault();

                        //unidad del funcionario
                        var unidad = context.PLUNILAB.FirstOrDefault(q => q.Pl_UndCod == CodUnidad);
                        if (unidad != null)
                        {
                            sigper.Unidad = unidad;

                            //secretaria del funcionario
                            var secretaria = context.PEDATPER.FirstOrDefault(q => q.RH_EstLab.Equals("A") && q.PeDatPerChq == unidad.Pl_UndNomSec);
                            if (secretaria != null)
                                sigper.Secretaria = secretaria;
                        }

                        /*datos laborales funcionario*/
                        var FunDatosLaborales = context.PeDatLab.Where(q => q.RH_NumInte == funcionario.RH_NumInte && q.RH_ContCod != 13 && (q.RhConIni.Value.Year == DateTime.Now.Year || q.RH_ContCod == 1)).OrderByDescending(q => q.RH_Correla).FirstOrDefault();
                        if (FunDatosLaborales != null)
                        {
                            sigper.DatosLaborales = FunDatosLaborales;
                        }
                    }

                    sigper.SubSecretaria = "TURISMO";

                    return sigper;
                }
            }

            return sigper;
        }
        public int GetBaseCalculoHorasExtras(int rut, int mes, int anno, int calidad)
        {
            using (var context = new AppContextEconomia())
            {
                Decimal? monto = 0;
                if (calidad == 10)
                {
                    /*honorarios*/
                    monto = (from R in context.RePagHisDet
                             where R.RH_NumInte == rut
                             where R.Re_Hismm == mes
                             where R.Re_Hisyy == anno
                             where R.RehDetObjTip == "H"
                             //where results.Contains(R.RehDetObj)
                             select R.RehDetObjMon).Sum().Value;
                }
                else
                {
                    /*planta y contrata*/
                    var results = (from l in context.LREMREP1Level1
                                   where l.lrem_codrep == 36
                                   where l.lrem_tipo == 1
                                   select l.lrem_reforcod).ToList();

                    var valor = from R in context.RePagHisDet
                                where R.RH_NumInte == rut
                                where R.Re_Hismm == mes
                                where R.Re_Hisyy == anno
                                where results.Contains(R.RehDetObj)
                                select R.RehDetObjMon.Value;

                    //var lala = context.RePagHisDet.Where(q => q.RH_NumInte == rut && q.Re_Hismm == mes && q.Re_Hisyy == anno);
                    //var juju = context.RePagHisDet.FirstOrDefault().RehDetObjMon;

                    if (valor.Any())
                    {
                        monto = (from R in context.RePagHisDet
                                 where R.RH_NumInte == rut
                                 where R.Re_Hismm == mes
                                 where R.Re_Hisyy == anno
                                 where results.Contains(R.RehDetObj)
                                 select R.RehDetObjMon).Sum().Value;
                    }
                }

                return Convert.ToInt32(monto.Value);
            }
        }
        public int GetHorasTrabajadas(string rut, int mes, int anno)
        {
            int HorasDiurnas = 0;
            int TotalHorasDiurnas = 0;
            //int HorasNocturnas = 0;
            DateTime? entrada = null;

            using (var context = new AppContextEconomia())
            {
                //var marcacionesMes = (from m in context.MARCACIONES
                //                      where m.IDENTIFICADOR == rut
                //                      where m.FECHA.Year == anno
                //                      where m.FECHA.Month == mes
                //                      select m).ToList();

                var marcacionesMes = context.MARCACIONES.Where(c => c.IDENTIFICADOR == rut && c.FECHA.Year == anno && c.FECHA.Month == mes).ToList();

                //DateTime oPrimerDiaDelMes = new DateTime(anno, mes, 1);

                foreach (var dia in marcacionesMes)
                {
                    if (dia.IN_OUT.Trim() == "IN" || dia.HORA.Hour < 12)
                        entrada = dia.HORA;
                    else if (dia.IN_OUT.Trim() == "OUT")
                    {
                        DateTime? salida = dia.HORA;

                        if ((salida.Value - entrada.Value).Hours >= 9)
                        {
                            HorasDiurnas = (salida.Value - entrada.Value).Minutes;
                        }
                        else if ((salida.Value - entrada.Value).Hours >= 8)
                        {
                            if (dia.FECHA.DayOfWeek.ToString() == "Friday")
                            {
                                if ((salida.Value - entrada.Value).Hours >= 8)
                                {
                                    HorasDiurnas = (salida.Value - entrada.Value).Minutes;
                                }
                                else
                                    HorasDiurnas = 0;// (salida.Value - entrada.Value).Minutes;
                            }
                            else
                                HorasDiurnas = 0;
                        }
                        else
                            HorasDiurnas = 0;
                    }
                    TotalHorasDiurnas += HorasDiurnas;
                    HorasDiurnas = 0;
                }
            }

            return TotalHorasDiurnas;
        }


        private PLUNILAB GetUnidadByFuncionarioEconomia(int RH_NumInte)
        {
            using (var _economiaContext = new AppContextEconomia())
            {
                var codigoUnidad = (from u in _economiaContext.ReContra
                                    join p in _economiaContext.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                    where p.RH_EstLab.Equals("A")
                                    where p.RH_NumInte == RH_NumInte
                                    where u.Re_ConIni == (from ud in _economiaContext.ReContra
                                                          where ud.RH_NumInte == RH_NumInte
                                                          select ud.Re_ConIni).Max()
                                    select u).FirstOrDefault();

                if (codigoUnidad != null)
                {
                    var unidad = _economiaContext.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigoUnidad.Re_ConUni);
                    if (unidad != null)
                        return unidad;
                }
            }

            return null;
        }

        private PLUNILAB GetUnidadByFuncionarioTurismo(int RH_NumInte)
        {
            using (var _turismoContext = new AppContextTurismo())
            {
                var codigoUnidad = (from u in _turismoContext.ReContra
                                    join p in _turismoContext.PEDATPER on u.RH_NumInte equals p.RH_NumInte
                                    where p.RH_EstLab.Equals("A")
                                    where p.RH_NumInte == RH_NumInte
                                    where u.Re_ConIni == (from ud in _turismoContext.ReContra
                                                          where ud.RH_NumInte == RH_NumInte
                                                          select ud.Re_ConIni).Max()
                                    select u).FirstOrDefault();

                if (codigoUnidad != null)
                {
                    var unidad = _turismoContext.PLUNILAB.AsNoTracking().FirstOrDefault(q => q.Pl_UndCod == codigoUnidad.Re_ConUni);
                    if (unidad != null)
                        return unidad;
                }
            }

            return null;
        }

        public List<PEDATPER> GetUserByTermUnidad(string term)
        {
            var returnValue = new List<PEDATPER>();

            using (var _economiaContext = new AppContextEconomia())
            {
                _economiaContext.PEDATPER
                .Where(p => p.RH_EstLab.Equals("A") && (p.PeDatPerChq.ToLower().Contains(term.ToLower())) || (p.Rh_Mail.ToLower().Contains(term.ToLower())))
                .ToList()
                .ForEach(q =>
                {
                    returnValue.Add(new PEDATPER
                    {
                        PeDatPerChq = q.PeDatPerChq,
                        Rh_Mail = q.Rh_Mail,
                        PLUNILAB = GetUnidadByFuncionarioEconomia(q.RH_NumInte)
                    });
                });
            }

            using (var _turismoContext = new AppContextTurismo())
            {
                _turismoContext.PEDATPER
                .AsNoTracking()
                .Where(q => q.RH_EstLab.Equals("A") && (q.PeDatPerChq.ToLower().Contains(term.ToLower())) || (q.Rh_Mail.ToLower().Contains(term.ToLower())))
                .ToList().ForEach(q =>
                {
                    returnValue.Add(new PEDATPER()
                    {
                        PeDatPerChq = q.PeDatPerChq,
                        Rh_Mail = q.Rh_Mail,
                        PLUNILAB = GetUnidadByFuncionarioTurismo(q.RH_NumInte)
                    }); ;
                });
            }

            return returnValue.OrderBy(q => q.PeDatPerChq).ToList();
        }

        SIGPER ISIGPER.GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        SIGPER ISIGPER.GetUserByRut(int rut)
        {
            throw new NotImplementedException();
        }

        SIGPER ISIGPER.GetSecretariaByUnidad(int codigo)
        {
            throw new NotImplementedException();
        }

        //DAES.Model.Sigper.SIGPER ISIGPER.NewGetUserByEmail(string email)
        //{
        //    throw new NotImplementedException();
        //}
    }
}