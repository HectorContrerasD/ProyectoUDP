using AdivinaElBinarioServer.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace AdivinaElBinarioServer.Services
{
    public class RespuestaServer
    {
        public RespuestaServer()
        {
            var hilo = new Thread(new ThreadStart(Iniciar))
            {
                IsBackground = true
            };
            hilo.Start();
        }
        public List<UdpClient> clientes { get; set; }
        public string Servidor { get; set; } = "0.0.0.0";
        public void EnviarUsuario(UsuarioDTO dto)
        {
            using (UdpClient cliente = new UdpClient())
            {
                var ipendpoint = new IPEndPoint(IPAddress.Parse(Servidor), 5021);
              
                var json = JsonSerializer.Serialize(dto);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                cliente.Send(buffer, buffer.Length, ipendpoint);
                clientes.Add(cliente);
                if (!cliente.Client.Connected)
                {
                    clientes.Remove(cliente);
                }
            }
        }
        public event EventHandler<UsuarioDTO>? ValidarRespuesta;
        void Iniciar()
        {
            UdpClient server = new(5020);
            while (true)
            {
                IPEndPoint remoto = new(IPAddress.Any,5020 );
                byte[] buffer = server.Receive(ref remoto);
                UsuarioDTO? dto = JsonSerializer.Deserialize<UsuarioDTO>
                    (Encoding.UTF8.GetString(buffer));
                if (dto != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ValidarRespuesta?.Invoke(this, dto);
                    });
                }
            }
        }
        


    }
}
