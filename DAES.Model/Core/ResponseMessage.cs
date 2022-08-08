using DAES.Model.GestionDocumental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.Core
{
    public class ResponseMessage
    {
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public bool IsValid => !Errors.Any();
        public int EntityId { get; set; }

        public byte[] Documento { get; set; }

        public ResponseMessage()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }
    }
}
