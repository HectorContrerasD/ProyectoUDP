using AdivinaElBinarioCliente.Models.Dtos;
using AdivinaElBinarioCliente.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdivinaElBinarioCliente.ViewModels
{
    public class RespuestasViewModel: INotifyPropertyChanged
    {
        public RespuestasDTO respuesta { get; set; } = new();

        RespuestaCliente ClienteUDP = new();

        public ICommand EnviarCommand { get; set; }

        public string IP { get; set; } = "0.0.0.0";

        public RespuestasViewModel()
        {
            EnviarCommand = new RelayCommand(Enviar);
        }

        private void Enviar()
        {
            ClienteUDP.Servidor = IP;
            ClienteUDP.EnviarRespuesta(respuesta);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
