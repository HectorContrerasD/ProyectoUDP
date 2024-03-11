using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaElBinarioCliente.Models.Dtos
{
    public class RespuestasDTO
    {
        public string Nombre { get; set; } = "";
        public byte Respuesta { get; set; }
        public bool Acierto { get; set; } = false;
    }
}
