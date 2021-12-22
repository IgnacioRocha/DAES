using DAES.Infrastructure.GestionDocumental;

GestionDocumentalContext context = new GestionDocumentalContext();

var documentos = context.Documento.Where(q => q.Activo && q.Doc_Asunto.Contains("victor") && q.Doc_Asunto.Contains("silva")).Select(q=>q.Id);
foreach (var documento in documentos)
{
    var adjuntos = context.Adjunto.Where(q => q.IdRegistro == documento).ToList();
    if (adjuntos != null)
    {
        foreach (var adjunto in adjuntos)
        {
            Console.WriteLine(adjunto.Adj_Url);
        }
    }
}
