using AdivinaElBinarioCliente.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaElBinarioCliente.Services
{
    public  class RespuestaCliente
    {
        private UDPClient cliente = new();

        public string Servidor { get; set; } = "0.0.0.0";

        public void EnviarRespuesta(RespuestasDTO dto)
        {
            var ipendpoint = new IPEndPoint(IPAddress.Parse(Servidor), 5001);
            var json = JsonSerializer.Serialize(dto);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            cliente.Send(buffer, buffer.Length, ipendpoint);
        }


    }
}
