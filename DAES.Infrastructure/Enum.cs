namespace DAES.Infrastructure
{
    public static class Enum
    {
        public enum Perfil
        {
            Admininstrador = 1,
            Consultor = 2,
            Jefatura = 3,
            AreaRegistro = 4,
            AreaLegal = 5,
            Secretaria = 6,
            AreaContable = 7,
            Transparencia = 8,
            SIAC = 9,
            RepresentanteOrganizacion = 10
        }

        public enum Estado
        {
            EnConstitucion = 1,
            Vigente = 2,
            Disuelta = 3,
            Inexistente = 4,
            Cancelada = 5,
            RolAsignado = 6
        }

        public enum Situacion
        {
            Activa = 1,
            Inactiva = 2
        }

        public enum TipoDocumento
        {
            VigenciaDirectorio = 1,
            Vigencia = 2,
            Articulo8voTransitorio = 3,
            Disolucion = 4,
            Estatutos = 5,
            SinClasificar = 6,
            Oficio = 21,
            Memo = 19,
            CertificadoArtículo8voTransitorio = 3,
            CertificadoDisolución = 4,
            CertificadoEstatutos = 5,
            CertificadoVigencia = 2,
            CertificadoVigenciaDirectorio = 1,
            Certificados = 11,
            CAC_AC01 = 46,
            CAC_AC02 = 47,
            CAC_AC03 = 48,
            CAC_AC04 = 49,
            CAC_AC05 = 50,
            CAC_AC12 = 51,
            CAC_ANUAL = 52,
            CAC_MENSUAL_1 = 53,
            CAC_MENSUAL_2 = 54,
            CDR_C01_Personas = 55,
            CDR_C02_OperacionesDeCrédito = 56,
            CDR_C03_GarantíasPrendarias = 57,
            CDR_C04_GarantíasHipotecarias = 58,
            CDR_C05_SaldosContables = 59,
            CDR_C06_GarantíasPersonales = 60,
            CAC_AUTOEVALUACION_TI = 61,
            CAC_AUTOEVALUACION_GESTION = 62,
            NOCAC_DEUDORES = 63,
            NOCAC_CARTERA_VENCIDA = 64,
            NOCAC_BALANCE = 65,
            NOCAC_AUTOEVALUACION_GESTION = 66,
            NOCAC_CACAPR = 67,
            CAC_AC06 = 68,
            CAC_AC07 = 69,
            CAC_AC08 = 70,
            CAC_AC09 = 71,
            CAC_AC10 = 72,
            CAC_AC11 = 73,
            PDF_BALANCE = 74,
            PDF_ESTADO_RESULTADOS = 75,
            PDF_AUDITORES_EXTERNOS = 76,
            NOCAC_DEUDORES_2 = 77,
            C901_BalanceGeneralClasificado = 82,
            C901_EstadoDeResultados = 83,
            C901_BalanceOchoColumnas = 84,
            C901_InformdeAuditoria = 85,
            C901_EstadoDeFlujoDeEfectivo = 86,
            C901_NotasExplicativasDeLosEstadosFinancieros = 87,
            C901_CertificadoInscripcionAuditoria = 88,
            C902_MemoriaCooperativa = 89,
            FirmadoFirmaElectrónica = 95,
            EstadosFinancierosIntermedios = 96,
            FichaDetalladaFuentesFinanciamiento = 97,
            ActaAsambleaSocios = 98,
            EstadosFinancierosDebidamenteAprobadosPorAsamblea = 99,
            FichaDetalladaFuentesFinancieros = 100,
            FichaDatos = 101,
            InformeComisionRevisodoraCuentas = 102
        }

        public enum TipoOrganizacion
        {
            Cooperativa = 1,
            AsociacionGremial = 2,
            AsociacionConsumidores = 3,
            AunNoDefinida = 4
        }

        public enum DefinicionProceso
        {
            SolicitudCertificadoManual = 67,
            SolicitudCertificadoAutomatico = 69,
            ConstitucionWeb = 51,
            Actualizacion = 70,
            AsambleaOrdinariaAsociacion = 73,
            ModificacionEstatutoAsociacion = 78,
            DisolucionAsociacion = 74,
            ActualizacionNumeroSociosAsociacion = 27,
            JuntaGeneralSociosObligatoriaCooperativa = 77,
            ReformaEstatutosCooperativa = 75,
            FusionCooperativa = 76,
            DivisionCooperativa = 77,
            TransformacionCooperativa = 77,
            DisolucionCooperativa = 79,
            RespuestaOficio = 81,
            ConstitucionOP = 82,
            ModeloSupervision = 83,
            Articulo90IncisoPrimero = 85,
            Articulo90IncisoSegundo = 87,
            Articulo91 = 89,
            AsambleaExtraOrdinariaAsociacion = 90,
            Fiscalizacion = 102,

            InformacionACSemestral = 104,
            InformacionACAnual = 105,
        }

        public enum TipoAprobacion
        {
            SinAprobacion = 1,
            Aprobada = 2,
            Rechazada = 3,
        }

        public enum Configuracion
        {
            SecuenciaCorrelativo = 1,
            PlantillaInformeProcesosAtrasados = 2,
            PlantillaInformeProcesosAtrasadosDetalle = 3,
            URLServicioFirmaElectronica = 4,
            URLImagenRubrica = 5,
            URLImagenSello = 6,
            URLImagenLogo = 7,
            MensajeVerificacionCertificado = 8,
            PlantillaCorreoNotificacionProceso = 10,
            NotificarSolicitudCertificadoManual = 11,
            AsuntoCorreoNotificacion = 13,
            DiasCertificadosManuales = 14,
            DireccionLinea1 = 15,
            DireccionLinea2 = 16,
            LayoutXMLFirmaElectronica = 17,
            PLantillaNotificacionTarea = 18,
            PLantillaNotificacionTareaArchivada = 19,
            MensajePieFirmaCertificado = 20,
            UserHSM = 21,
            PasswordHSM = 22
        }

        public enum Genero
        {
            Fememino = 1,
            Masculino = 2,
            SinGenero = 3
        }

        public enum TipoPrivacidad
        {
            Publico = 1,
            Privado = 2
        }

        public enum SubRubro
        {
            AhorroCredito = 15,
        }

        public enum Cargo
        {
            Defecto = 135,
        }
    }
}