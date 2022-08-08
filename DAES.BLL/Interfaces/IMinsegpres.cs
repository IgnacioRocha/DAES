namespace DAES.BLL.Interfaces
{
    public interface IMinsegpres
    {
        byte[] SignConOtp(byte[] documento, string OTP, int id, string Run, string Nombre, bool TipoDocumento, int DocumentoId);
        
        byte[] SignSinOtp(byte[] documento, int id, string Run, string Nombre, bool TipoDocumento, int DocumentoId);
    }
}
