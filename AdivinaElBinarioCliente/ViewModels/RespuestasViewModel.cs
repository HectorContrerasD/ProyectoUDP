using AdivinaElBinarioCliente.Models.Dtos;
using AdivinaElBinarioCliente.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdivinaElBinarioCliente.ViewModels
{
    public class RespuestasViewModel: INotifyPropertyChanged
    {
        public RespuestasDTO respuesta { get; set; } = new();
        public string Mensaje { get; set; }
        RespuestaCliente ClienteUDP = new();
        public bool Conectado { get; set; }

        public ICommand EnviarCommand { get; set; }
        public ICommand ConectarCommand { get; set; }

        public string IP { get; set; } = "0.0.0.0";

        public RespuestasViewModel()
        {
            EnviarCommand = new RelayCommand(Enviar);
            ConectarCommand = new RelayCommand(Conectar);
        }

        private void Conectar()
        {
            Conectado= true;
            Actualizar();
        }

        private void Enviar()
        {
            ClienteUDP.Servidor = IP;
            ClienteUDP.EnviarRespuesta(respuesta);
        }
        public void Actualizar()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
