using AdivinaElBinarioCliente.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdivinaElBinarioCliente.Services
{
    public class RespuestaCliente
    {
        public event EventHandler<RespuestasDTO> RespuestaRecibida;
        public string Servidor { get; set; } = "0.0.0.0";
        public List<UdpClient> ListaClientes = new();
        public void EnviarRespuesta(RespuestasDTO dto)
        {
            using (UdpClient cliente = new UdpClient())
            {
                var ipendpoint = new IPEndPoint(IPAddress.Parse(Servidor), 5020);
                dto.Nombre = Dns.GetHostName();
                var json = JsonSerializer.Serialize(dto);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                cliente.Send(buffer, buffer.Length, ipendpoint);
                ListaClientes.Add(cliente);
                if (!cliente.Client.Connected)
                {
                    ListaClientes.Remove(cliente);
                }
                
            }

        }

        public RespuestaCliente()
        {
            var hilo = new Thread(new ThreadStart(EscucharRespuesta))
            {
                IsBackground = true
            };
            hilo.Start();
        }

        private void EscucharRespuesta()
        {
            using (UdpClient cliente = new UdpClient(5021))
            {
                while (true)
                {
                    IPEndPoint remoto = new IPEndPoint(IPAddress.Any, 5021);
                    byte[] buffer = cliente.Receive(ref remoto);
                    RespuestasDTO dto = JsonSerializer.Deserialize<RespuestasDTO>(Encoding.UTF8.GetString(buffer));
                    if (dto != null)
                    {
                        OnRespuestaRecibida(dto);
                    }
                }
            }
        }

        protected virtual void OnRespuestaRecibida(RespuestasDTO respuesta)
        {
            RespuestaRecibida?.Invoke(this, respuesta);
        }
    }



}

