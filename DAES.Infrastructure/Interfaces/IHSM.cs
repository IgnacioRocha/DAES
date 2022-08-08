using System.Collections.Generic;

namespace DAES.Infrastructure.Interfaces
{
    public interface IHsm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="firmante"></param>
        /// <param name="documentoId"></param>
        /// <param name="folio"></param>
        /// <param name="urlVerificacion"></param>
        /// <param name="qr"></param>
        /// <returns></returns>
        byte[] SignWSDL(byte[] documento, List<string> firmante, int documentoId, string folio, string urlVerificacion, byte[] qr);

        /// <summary>
        /// Aplica firma electrónica a documentos usando el servicio REST
        /// </summary>
        /// <param name="documento">Array del contenido del documento a firmar</param>
        /// <param name="firmantes">Lista de ids de firmantes</param>
        /// <param name="documentoId">Id del documento a firmar</param>
        /// <param name="folio">Folio del documento</param>
        /// <param name="urlVerificacion">Url de verificación del documento</param>
        /// <param name="QR">Array de la imagen con código QR</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        byte[] SignREST(byte[] documento, List<string> firmante, int documentoId, string folio, string urlVerificacion, byte[] qr);
        byte[] Sign(byte[] documento, List<string> firmantes, int documentoId, string folio, string url, byte[] QR);
    }
}