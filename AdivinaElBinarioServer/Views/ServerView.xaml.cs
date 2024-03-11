using AdivinaElBinarioServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdivinaElBinarioServer.Views
{
    /// <summary>
    /// Interaction logic for ServerView.xaml
    /// </summary>
    public partial class ServerView : Window
    {
        public ServerView()
        {
            InitializeComponent();
            var viewmodel = DataContext as ServerViewModel;
            viewmodel.OcultarBinario += Viemodel_OcultarBinario;
            viewmodel.MostrarBinario += Viewmodel_MostrarBinario;
        }

        private void Viewmodel_MostrarBinario(object? sender, EventArgs e)
        {
            binario.Visibility = Visibility.Visible;
        }

        private void Viemodel_OcultarBinario(object? sender, EventArgs e)
        {
           binario.Visibility = Visibility.Collapsed;
        }
    }
}
