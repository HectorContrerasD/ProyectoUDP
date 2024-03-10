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
        public event EventHandler<UsuarioDTO>? ValidarRespuesta; 
        void Iniciar()
        {
            UdpClient server = new(5020);
            while (true)
            {
                IPEndPoint remoto = new(IPAddress.Any, 5020);
                byte[] buffer = server.Receive(ref remoto);
                UsuarioDTO? dto = JsonSerializer.Deserialize<UsuarioDTO>
                    (Encoding.UTF8.GetString(buffer));
                if (dto != null)
                {
                    Application.Current.Dispatcher.Invoke(()=>
                        {
                            ValidarRespuesta?.Invoke(this, dto);
                        });
                }
            }
        }
    }
    //public event EventHandler<UsuarioDTO>? ValidarRespuesta;
    
    
}
