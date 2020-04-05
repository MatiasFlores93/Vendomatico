using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlServerCe;

using System.Collections.ObjectModel;

namespace Vendomatico
{
    public class DataAcess
    {
        // Creamos una variable de referencia a la cadena de conexión almacenada en la configuración del proyecto.
        private string cadenaConexion = Properties.Settings.Default.conexionDB;

        // Variables para recuperar información de la Base de datos
        private SqlCeConnection CN;
        private SqlCeCommand CMD;
        private SqlCeDataReader RDR;

        // El objeto Transaction es especialmente útil cuando se necesita
        // realizar una operacion que requiera insertar filas en varias tablas (por ejemplo una venta)
        // ya que si algo falla en alguna de las tablas, los cambios
        // hechos a las demás se deshacen, evitando así, una pérdida de integridad en los datos.

        private SqlCeTransaction TR;

        public Auto obtenerVehiculo(int idABuscar)
        {
            CN = new SqlCeConnection(cadenaConexion);
            CMD = new SqlCeCommand("SELECT * FROM autos WHERE id = @p1", CN);
            CMD.Parameters.AddWithValue("@p1", idABuscar);
            CMD.CommandType = CommandType.Text;

            try // Intentamos
            {
                CN.Open(); // Abrir conexión a la Base de datos,.
                RDR = CMD.ExecuteReader(CommandBehavior.SingleRow);// Ejecutar instrucción SQL.

                if (RDR.Read()) // Si se lee el registro (por que si existe) entonces:
                {
                    // Creamos un objeto de tipo ContactoEmpresaZeta que "envuelve"
                    // el registro actual de la tabla.

                    Auto auto =
                        new Auto((int)RDR["id"], (string)RDR["nombre"], (int)RDR["precio"],
                            (string)RDR["imagen"]);

                    return auto; // Regresamos el objeto.
                }
                else // Si no (por que no existe)
                {
                    return null; // Regresamos un valor nulo.
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw ex; // Lanzamos una excepción en caso de haberla ( por ejemplo que no se puede conectar a la Base de Datos)
            }
            finally // Finalmente si se produce o no una excepción, cerramos la conexión.
            {
                CN.Close();
            }
        }

        // Método que obtiene todos los contactos de la tabla de contactos.
        public ObservableCollection<Auto> listaAutos()
        {
            // Instanciamos la variable CN pasandole a su constructor la variable "cadenaConexion".
            CN = new SqlCeConnection(cadenaConexion);
            // Instanciamos la variable CMD pasandole a su constructor la instrucción Sql que debe ejecutar
            // así como  la variable CN que le indica en que base de datos debe ejecutar dicha instrucción.
            CMD = new SqlCeCommand("SELECT * FROM autos", CN);
            // Tipo de comando.
            CMD.CommandType = CommandType.Text;

            // Creamos una colección de tipo ContactoEmpresaZeta que "envuelve"
            // a los registros de la tabla que se van a recuperar.
            ObservableCollection<Auto> ListaDeContactos =
                new ObservableCollection<Auto>();

            try // Intentamos...
            {
                CN.Open(); // Abrir la conexión.
                RDR = CMD.ExecuteReader(); // Ejecutar la instrucción SQL SELECT.

                while (RDR.Read()) // recorrer todos los registros recuerados.
                {
                    //string direccionTmp = RDR["Direccion"] as string ?? "[No especificado]";
                    // Con la sentencia as string intentamos convertir
                    // el valor que traiga la columna RDR["Dirección"] a un tipo string
                    // si no es posible la conversión (por que RDR["Dirección"] es DBNull u otra cosa que no sea string)
                    // entonces el operador as devolvera null, a lo que el operador ?? responde
                    // asignado la cadena [No especificado] a la variable direcciónTmp.

                    // Más información sobre el operador as: http://msdn.microsoft.com/es-mx/library/cscsdfbt%28v=vs.80%29.aspx
                    // Más información sobre el operador ?? C#: http://msdn.microsoft.com/es-es/library/ms173224%28v=vs.80%29.aspx // es mejor buscar en otra página por que esta información no es muy clara.

                    // Crear un objecto que "envuelve" el registro actual.
                    Auto autoActual =
                        new Auto((int)RDR["id"], (string)RDR["nombre"], (int)RDR["cantidad"], (int)RDR["precio"],
                            (string)RDR["imagen"]); // Notar que en el constructor mande a la variable direcciónTmp.

                    // Agregar el objeto a la coleccion.
                    ListaDeContactos.Add(autoActual);
                }
            }
            catch (Exception ex)
            {
                throw ex; // Lanzamos excepción.
            }
            finally
            {
                CN.Close(); // Cerramos la conexión.
            }

            return ListaDeContactos; // Regresamos la lista.
        }

        public List<Auto> obtenerAutos()
        {
            // Instanciamos la variable CN pasandole a su constructor la variable "cadenaConexion".
            CN = new SqlCeConnection(cadenaConexion);
            // Instanciamos la variable CMD pasandole a su constructor la instrucción Sql que debe ejecutar
            // así como  la variable CN que le indica en que base de datos debe ejecutar dicha instrucción.
            CMD = new SqlCeCommand("SELECT * FROM autos", CN);
            // Tipo de comando.
            CMD.CommandType = CommandType.Text;

            // Creamos una colección de tipo ContactoEmpresaZeta que "envuelve"
            // a los registros de la tabla que se van a recuperar.
            List<Auto> listaAutos = new List<Auto>();

            try // Intentamos...
            {
                CN.Open(); // Abrir la conexión.
                RDR = CMD.ExecuteReader(); // Ejecutar la instrucción SQL SELECT.

                while (RDR.Read()) // recorrer todos los registros recuerados.
                {
                    //string direccionTmp = RDR["Direccion"] as string ?? "[No especificado]";
                    // Con la sentencia as string intentamos convertir
                    // el valor que traiga la columna RDR["Dirección"] a un tipo string
                    // si no es posible la conversión (por que RDR["Dirección"] es DBNull u otra cosa que no sea string)
                    // entonces el operador as devolvera null, a lo que el operador ?? responde
                    // asignado la cadena [No especificado] a la variable direcciónTmp.

                    // Más información sobre el operador as: http://msdn.microsoft.com/es-mx/library/cscsdfbt%28v=vs.80%29.aspx
                    // Más información sobre el operador ?? C#: http://msdn.microsoft.com/es-es/library/ms173224%28v=vs.80%29.aspx // es mejor buscar en otra página por que esta información no es muy clara.

                    // Crear un objecto que "envuelve" el registro actual.
                    Auto auto =
                        new Auto((int)RDR["id"], (string)RDR["nombre"], (int)RDR["cantidad"], (int)RDR["precio"],
                            (string)RDR["imagen"]); // Notar que en el constructor mande a la variable direcciónTmp.

                    // Agregar el objeto a la coleccion.
                    listaAutos.Add(auto);
                }
            }
            catch (Exception ex)
            {
                throw ex; // Lanzamos excepción.
            }
            finally
            {
                CN.Close(); // Cerramos la conexión.
            }

            return listaAutos; // Regresamos la lista.
        }

        // Método que inserta un nuevo contacto en la tabla.
        public int GuardarNuevoVehiculo(Auto autoNuevo)
        {
            CN = new SqlCeConnection(cadenaConexion);
            CMD = new SqlCeCommand();
            CMD.Connection = CN;
            CMD.CommandType = CommandType.Text;

            // CMD consta de 2 instrucciones SQL, la primer que inserta el registro en la tabla de contactos y
            // la segunda que devuelve el id que se autogeneró en la base de datos.
            // en mi caso el id es autogenerado pues lo especifiqué como columna "identidad".

            CMD.CommandText = "INSERT autos (nombre, cantidad, precio, imagen) " +
                "VALUES (@p1,@p2,@p3,@p4);";

            // establecemos los valores que tomarán los parámetros de la instrucción SQL.
            CMD.Parameters.AddWithValue("@p1", autoNuevo.Nombre);
            CMD.Parameters.AddWithValue("@p2", autoNuevo.Cantidad);
            CMD.Parameters.AddWithValue("@p3", autoNuevo.Precio);
            CMD.Parameters.AddWithValue("@p4", autoNuevo.Imagen);

            String com = CMD.CommandText;

            int id = 0; // Variable auxiliar.

            try
            {
                CN.Open();
                TR = CN.BeginTransaction();
                CMD.Transaction = TR;
                id = CMD.ExecuteNonQuery();
                // CMD.ExecuteScalar devuelve object no pude pasar de object a int directamente
                TR.Commit();
            }
            catch (Exception ex)
            {
                if (TR != null)
                {
                    TR.Rollback(); // Si hay una excepción llamamos al método Rollback() para que deshaga
                    // cualquier cambio hecho a la base de datos.
                }
                throw ex;
            }
            finally
            {
                CN.Close();
            }

            return id;
        }

        // Método que actualiza un contacto existente de la tabla de contactos.
        public int ActualizarContactoExistente(int id, string nombre, int cantidad, int precio,
            string imagen)
        {
            CN = new SqlCeConnection(cadenaConexion);
            CMD = new SqlCeCommand
            ("UPDATE autos " +
            "SET nombre = @p1, cantidad = @p2, precio = @p3, imagen = @p4 " +
            "WHERE id = @p0", CN);

            CMD.CommandType = CommandType.Text;

            CMD.Parameters.AddWithValue("@p0", id);
            CMD.Parameters.AddWithValue("@p1", nombre);
            CMD.Parameters.AddWithValue("@p2", cantidad);
            CMD.Parameters.AddWithValue("@p3", precio);
            CMD.Parameters.AddWithValue("@p4", imagen);

            try
            {
                CN.Open();
                return CMD.ExecuteNonQuery();// Devuelve el número de filas afectadas en este caso debe ser 1.
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CN.Close();
            }
        }

    }
}
