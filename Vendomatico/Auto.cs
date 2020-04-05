using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel; // espacio de la interfaz  INotifyPropertyChanged

namespace Vendomatico
{
    public class Auto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { 
                    int _id = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Id")); 
                }
        }

        private string _imagen;
        public string Imagen
        {
            get { return _imagen; }
            set { 
                _imagen = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Imagen")); 
            }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { 
                _nombre = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nombre"));
            }

        }

        private int _precio;

        public int Precio
        {
            get { return _precio; }
            set { 
                _precio = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Precio"));
            }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { 
                _cantidad =value;
                OnPropertyChanged(new PropertyChangedEventArgs("Cantidad"));
            }
        }
        

        public Auto(int id, string nombre, int precio, string imagen)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Precio = precio;
            this.Imagen = imagen;
        }

        public Auto(int id, string nombre, int cantidad, int precio, string imagen)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Precio = precio;
            this.Imagen = imagen;
            this.Cantidad = cantidad;
        }

    }

}
