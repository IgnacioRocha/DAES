
namespace DAES.BLL.Interfaces
{
    public interface IFile
    {
        Model.DTO.DTOFileMetadata BynaryToText(byte[] content);
        byte[] CreateQr(string id);
        byte[] EstamparCodigoEnDocumento(byte[] documento, string text);
    }
}