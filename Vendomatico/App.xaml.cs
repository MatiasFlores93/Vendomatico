using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Vendomatico
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static DataAcess accesoADatos = new DataAcess();

        // Propiedad estatica de solo lectura.
        public static DataAcess AccesoADatos
        {
            get { return App.accesoADatos; }
        }

        // Creamos una instancia de la clase DataAcess en el App por que asi
        // estará disponible para cualquier ventana que agreguemos a la aplicación.
    }
}
