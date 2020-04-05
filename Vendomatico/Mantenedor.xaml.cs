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
using System.Windows.Shapes;

// Espacio de nombres de la clase ObservableCollection.
using System.Collections.ObjectModel;

namespace Vendomatico
{
    /// <summary>
    /// Lógica de interacción para Mantenedor.xaml
    /// </summary>
    public partial class Mantenedor : Window
    {
        public Mantenedor()
        {
            InitializeComponent();
        }

        private ObservableCollection<Auto> listaAutosADesplegar; // Coleccion auxiliar.

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                listaAutosADesplegar = App.AccesoADatos.listaAutos(); // Recuperamos los contactos de la tabla mediante el método ObtenerContactos() y la asignamos a ListaDeContactosADesplegar.
                lstAutos.ItemsSource = listaAutosADesplegar; // El origen de items del list box de contactos es nuestra coleccion auxiliar.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Enumeracion auxiliar para dar valores descriptivos a la operación que esté realizando el usuario:   
        private enum ModoDeEdicion
        {
            Insertar,
            Modificar
        };
        // Variable de tipo "ModoDeEdicion" auxiliar cuyo valor se determina en base a si el usuario hizo click en el botón "Nuevo"
        // ó hizo click en el botón "Modificar"
        private ModoDeEdicion edicion;
          // Habilita algunos controles y deshabilita otros para forzar al usuario a que complete
        //una tarea de edicion (insertar o modificar 1 registro) de la tabla a la vez.
        private void CambiarVentanaAModoDeEdicion()
        {
            //lstAutos.IsEnabled = false;
            uGridNuevoModificarEliminar.IsEnabled = false;

            if (edicion == ModoDeEdicion.Insertar)
            {
                btnGuardar.IsEnabled = true;

            }
            else
            {
                if (edicion == ModoDeEdicion.Modificar)
                {
                    btnActualizar.IsEnabled = true;
                }
            }

            btnCancelar.IsEnabled = true;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(gridDetallesDeContacto); i++)
            {
                if (VisualTreeHelper.GetChild(gridDetallesDeContacto, i).GetType() == typeof(TextBox))
                {
                    // txtId lo mantenemos como de solo lectura por que el id de los contactos es de identidad y se autogenera en la tabla.
                    TextBox textBoxActual = (TextBox)VisualTreeHelper.GetChild(gridDetallesDeContacto, i);
                    if (textBoxActual.Name != "txtId")
                    {
                        textBoxActual.IsReadOnly = false;
                    }
                    // Convertimos explicitamente el elemento actual y extraemos la propiedad IsReadOnly.                    
                }

                // Más información sobre VisualTreeHelper en: http://msdn.microsoft.com/es-mx/library/system.windows.media.visualtreehelper.aspx
            }
        }


        // pone a mi ventana en modo de insertar deshabilitando parte de los controles.
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            lstAutos.SelectedItem = null;
            edicion = ModoDeEdicion.Insertar;
            CambiarVentanaAModoDeEdicion();
            FocusManager.SetFocusedElement(this, txtNombre);
        }

        // pone a mi ventana en modo de modificacion deshabilitando parte de los controles.
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (lstAutos.Items.Count > 0)
            {
                if (lstAutos.SelectedItem != null)
                {
                    edicion = ModoDeEdicion.Modificar;
                    CambiarVentanaAModoDeEdicion();
                    FocusManager.SetFocusedElement(this, txtNombre);
                }
                else
                {
                    MessageBox.Show("Tienes que seleccionar un contacto de la lista para poder modificarlo", 
                        "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Actualmente no hay ningun contacto registrado en el directorio", 
                    "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           }

        // Manda a llamar al método que inserta 1 registro en la tabla.
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Auto nuevoAuto;

            int id = -1;

            try
            {
                // Creamos una nueva instancia de la clase que representa nuestros contactos con la información
                // de los textboxes.
                nuevoAuto = new Auto (-1, txtNombre.Text,Convert.ToInt32(txtCantidad.Text),Convert.ToInt32(txtPrecio.Text),txtImagen.Text);
                // En el parámetro id mandamos -1 por que la Base de datos genera números enteros positivos
                // automaticamente para las columnas identidad. 

                id = App.AccesoADatos.GuardarNuevoVehiculo(nuevoAuto); // El valor de retorno (que es el id que generó
                // la base de datos para este registro insertado) lo asignamos a la variable id.

                if (id > -1) // si id es > 0 significa que si se guardó el registro en la tabla.
                {
                    MessageBox.Show("Contacto guardado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    /*nuevoContacto.Id = id;//asigno el id real, el que generó la base de datos.
                    ListaDeContactosADesplegar.Add(nuevoContacto); // Agrego este objeto nuevoContacto al listbox.
                    lstContactos.SelectedItem = nuevoContacto; // Lo establecemos como item seleccionado (sombreado).
                    lstContactos.ScrollIntoView(nuevoContacto);// Traemos el item al área visible del listbox.
                    FocusManager.SetFocusedElement(this, lstContactos); // Le pasamos el foco al listbox de contactos.*/                    
                    CambiarVentanaAModoNormal();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);              
            }

        }

        // Habilita algunos controles y deshabilita otros para forzar al usuario a que elija mediante los botones
        // a realizar solo una tarea de edición (insertar o modificar 1 registro) de la tabla a la vez.
        private void CambiarVentanaAModoNormal()
        {
            lstAutos.IsEnabled = true;
            uGridNuevoModificarEliminar.IsEnabled = true;

            if (edicion == ModoDeEdicion.Insertar)
            {
                btnGuardar.IsEnabled = false;
            }
            else
            {
                if (edicion == ModoDeEdicion.Modificar)
                {
                    btnActualizar.IsEnabled = false;
                }
            }

            btnCancelar.IsEnabled = false;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(gridDetallesDeContacto); i++)
            {
                if (VisualTreeHelper.GetChild(gridDetallesDeContacto, i).GetType() == typeof(TextBox))
                {
                    // txtId lo mantenemos como de solo lectura por que el id de los contactos es de identidad y se autogenera en la tabla.
                    TextBox textBoxActual = (TextBox)VisualTreeHelper.GetChild(gridDetallesDeContacto, i);
                    if (textBoxActual.Name != "txtId")
                    {
                        textBoxActual.IsReadOnly = true;
                    }
                    // Convertimos explicitamente el elemento actual y extraemos la propiedad IsReadOnly.                    
                }

                // Más información sobre VisualTreeHelper en: http://msdn.microsoft.com/es-mx/library/system.windows.media.visualtreehelper.aspx
            }
        }

        // Manda a llamar al método que actualiza 1 registro en la tabla.
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // En el objeto contactoActual asignamos una referencia al item actualmente seleccionado en el listbox.
                Auto autoActual;
                autoActual = lstAutos.SelectedItem as Auto;
                int id = ((Auto)lstAutos.SelectedItem).Id;
                // Al objeto contactoActual le asignamos la información de los textBoxes para que la valide antes
                // de actualizar la tabla.

                autoActual.Nombre = txtNombre.Text;
                autoActual.Cantidad = Convert.ToInt32(txtCantidad.Text);
                autoActual.Precio = Convert.ToInt32(txtPrecio.Text);
                autoActual.Imagen = txtImagen.Text;

                int resultado = 0;

                // Llamamos al método que actualiza el contacto en la base de datos.
                resultado = App.AccesoADatos.ActualizarContactoExistente(autoActual.Id, txtNombre.Text,
                    Convert.ToInt32(txtCantidad.Text), Convert.ToInt32(txtPrecio.Text), txtImagen.Text);
                // ▲ Retorna el numero de filas afectadas en la tabla, el cuál debe ser 1.   

                if (resultado > 0) // si se actualizó el registro en la tabla ...
                {
                    MessageBox.Show("Contacto modificado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    FocusManager.SetFocusedElement(this, lstAutos);
                    lstAutos.ScrollIntoView(autoActual);
                    CambiarVentanaAModoNormal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    
    }
}
