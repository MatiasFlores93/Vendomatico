using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vendomatico
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<TextBlock> listaNombresVehiculos; // esta lista almacena todos los casilleros donde se puede poner los nombres de los vehiculos
        List<Image> listaImagenesVehiculos;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaNombresVehiculos = new List<TextBlock>();
            listaImagenesVehiculos = new List<Image>();

            // Se pasan cada uno de los capos de textos a la lista, asi se pueden llenar de forma dinamica
            listaNombresVehiculos.Add(op1Nombre);
            listaNombresVehiculos.Add(op2Nombre);
            listaNombresVehiculos.Add(op3Nombre);
            listaNombresVehiculos.Add(op4Nombre);
            listaNombresVehiculos.Add(op5Nombre);
            listaNombresVehiculos.Add(op6Nombre);
            listaNombresVehiculos.Add(op7Nombre);
            listaNombresVehiculos.Add(op8Nombre);
            listaNombresVehiculos.Add(op9Nombre);
            listaNombresVehiculos.Add(op10Nombre);
            listaNombresVehiculos.Add(op11Nombre);
            listaNombresVehiculos.Add(op12Nombre);

            // Se agregan los espacios de las imagenes a su lista
            listaImagenesVehiculos.Add(op1Imagen);
            listaImagenesVehiculos.Add(op2Imagen);
            listaImagenesVehiculos.Add(op3Imagen);
            listaImagenesVehiculos.Add(op4Imagen);


            //Auto auto1 = App.AccesoADatos.obtenerVehiculo(1);
            //op1Nombre.Text = auto1.Nombre;

            // Solicitamos la lista de Autos a la base de datos

            List<Auto> listaAutos = App.AccesoADatos.obtenerAutos();

            for (int i = 0; i < listaAutos.Count; i++) 
            {
                listaNombresVehiculos[i].Text = listaAutos[i].Nombre;

                Uri uri = new Uri(listaAutos[i].Imagen + "", UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri); 
                listaImagenesVehiculos[i].Source = imgSource;
            }
        }

        private void btnMantenedor_Click(object sender, RoutedEventArgs e)
        {
            Mantenedor man = new Mantenedor();
            man.Owner = this;
            man.ShowDialog();
        }
    }
}
