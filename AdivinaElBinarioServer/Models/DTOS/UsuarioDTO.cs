using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaElBinarioServer.Models.DTOS
{
    public class UsuarioDTO
    {
        public string Nombre { get; set; } = "";
        public byte Respuesta { get; set; }
        public bool Acierto { get; set; } = false;
      
    }
}
