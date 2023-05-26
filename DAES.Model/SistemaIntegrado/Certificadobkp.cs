using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Certificadobkp")]
    public class Certificadobkp
    {
        public Certificadobkp()
        {
        }

        [Key]
        public int CertificadoId { get; set; }
        public int TipoCertificadoId { get; set; }
        public int OrganizacionId { get; set; }
        public int FirmanteId { get; set; }
        public int RegionId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }
        public string Fono { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public byte[] PDF { get; set; }
        public int? ProcesoId { get; set; }
    }
}