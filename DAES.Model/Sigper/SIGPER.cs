namespace DAES.Model.Sigper
{
    public class SIGPER
    {
        public PEDATPER Funcionario { get; set; }
        public PLUNILAB Unidad { get; set; }
        public PEDATPER Jefatura { get; set; }
        public PEDATPER Secretaria { get; set; }
        public PeDatLab DatosLaborales { get; set; }
        public ReContra Contrato { get; set; }
        public string SubSecretaria { get; set; }
    }
}