using posiblePruebaAviatur.Infraestructura.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace posiblePruebaAviatur.Infraestructura
{
    public class ClienteRepository
    {
        private Conexion _conexion; // clase que conecta a la base de datos
        private SqlCommand _command; // permite agregar parámetros y definir el tipo de ejecución sobre la base de datos
        private DataTable _tabla; // permite almacenar los datos. Es una estructura en memoria que se utiliza para almacenar datos en forma de filas y columnas
        public SqlDataReader _reader; // permite leer la base de datos

        public ClienteRepository()
        {
            _command = new SqlCommand();
            _conexion = new Conexion();
            _tabla = new DataTable();
        }

        public DataTable CrudCliente(int operacion, ClienteEntity cliente)
        {
            

            try
            {
                
                _command = new SqlCommand();
                _tabla= new DataTable();
                _command.Connection = _conexion.AbrirConexion();
                _command.CommandType = CommandType.StoredProcedure; // le indicamos que lo que necesitamos de la base de datos es un procedimiento almacenado
                _command.CommandText = "_spCliente"; // le indicamos el nombre de nuestro SP

                // agregamos los parámetros de nuestro SP
                _command.Parameters.AddWithValue("@param_idOperacion", operacion);
                _command.Parameters.AddWithValue("@param_id", cliente.id);
                _command.Parameters.AddWithValue("@param_nombre", cliente.nombre);
                _command.Parameters.AddWithValue("@param_apellido", cliente.apellido);
                _command.Parameters.AddWithValue("@param_direccion", cliente.direccion);
                _command.Parameters.AddWithValue("@param_fecha_nacimiento", cliente.fecha_nacimiento);
                _command.Parameters.AddWithValue("@param_genero", cliente.genero);

                _reader = _command.ExecuteReader();
                _tabla.Load(_reader);

                return _tabla;
            }
            catch (Exception e)
            {
                throw new Exception("Error en acceso a datos: " + e.Message);
            }
            finally
            {
                // Liberamos recursos y cerramos la conexión
                _command.Dispose();
                _conexion.cerrarConexion();
            }
        }
    }
}