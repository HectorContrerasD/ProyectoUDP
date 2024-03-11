using AdivinaElBinarioServer.Models.DTOS;
using AdivinaElBinarioServer.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdivinaElBinarioServer.ViewModels
{
    public class ServerViewModel : INotifyPropertyChanged
    {
        Random rnd = new Random();
        public string IP { get; set; }
        RespuestaServer server = new();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer AdivinarTimer = new System.Windows.Threading.DispatcherTimer();
        public int NumeroR { get; set; }
        public ObservableCollection<UsuarioDTO> UsuariosLista { get; set; } = new();
        public ObservableCollection<string> UsuariosAcertados { get; set; } = new();
        public string Binario { get; set; } 
        public UsuarioDTO usuario { get; set; } 
        public ICommand IniciarCommand { get; set; }
       //public ICommand EnviarCommand { get; set; }
        public bool bandera { get; set; }
        public event EventHandler OcultarBinario;
        public event EventHandler MostrarBinario; 
        public event PropertyChangedEventHandler? PropertyChanged;
        public ServerViewModel()
        {
            var ips = Dns.GetHostAddresses(Dns.GetHostName());
            IP = ips
                .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Select(x => x.ToString())
                .FirstOrDefault() ?? "0.0.0.0";
            server.ValidarRespuesta += Server_ValidarRespuesta;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            IniciarCommand = new RelayCommand(Iniciar);
            //EnviarCommand = new RelayCommand(Enviar);
            Actualizar();
            //dispatcherTimer.Start();
        }

        public void Enviar()
        {
            server.Servidor = IP;
            //if ()
            //{

            //}
            server.EnviarUsuario(usuario);
            Actualizar();
        }

        private void Server_ValidarRespuesta(object? sender, Models.DTOS.UsuarioDTO e)
        {
            if (bandera)
            {
                var usuario = UsuariosLista.FirstOrDefault(x=> x.Nombre == e.Nombre);
                if (usuario!=null)
                {
                    if (e.Respuesta == NumeroR)
                    { 
                        if (UsuariosAcertados != null)
                        {
                            if (!UsuariosAcertados.Contains(e.Nombre))
                            {
                                usuario.Acierto = true;
                                UsuariosAcertados.Add(e.Nombre);
                            }
                        }
                    }
                }
                else
                {
                    UsuariosLista.Add(e);
                    if (e.Respuesta == NumeroR)
                    {
                        e.Acierto = true;
                        UsuariosAcertados.Add(e.Nombre);
                       
                    }
                }

            }
        }

        private void Iniciar()
        {
            GenerarNumero();
            dispatcherTimer.Start();
            UsuariosAcertados.Clear();
           
            Actualizar();
        }

        public void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            OcultarBinario.Invoke(sender, e);
            AdivinarTimer.Tick += new EventHandler(TiempoAdivinar);
            dispatcherTimer.Stop();
            
            AdivinarTimer.Interval = new TimeSpan(0, 0, 10);
            AdivinarTimer.Start();
            bandera = true;

        }

        private void TiempoAdivinar(object? sender, EventArgs e)
        {

            Actualizar();
            bandera = false;
            MostrarBinario.Invoke(sender, e);
            AdivinarTimer.Start();
            Enviar();
            Iniciar();  
            
        }

        public void GenerarNumero()
        {
            NumeroR = rnd.Next(0, 256);
            Binario = Convert.ToString(NumeroR, 2);
           
        }
        public void Actualizar()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

    }
}
