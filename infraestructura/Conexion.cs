using System.Data.SqlClient;
using System.Data;


namespace posiblePruebaAviatur.Infraestructura
{
    public class Conexion
    {
        public SqlConnection _connection;
        public Conexion() {
            try
            {
                _connection = new SqlConnection("Server=localhost; Initial Catalog=pruebaAviatur; Integrated Security=True");
            }
            catch (SqlException e) {
                Console.WriteLine("No se pudo conectar: " +e.Message);
            }
        }

        public SqlConnection AbrirConexion() {
           
            if(_connection.State==ConnectionState.Closed) {
                
               _connection.Open();
                
            }

            return _connection;
        }

        public void cerrarConexion() { 

           _connection.Close();
        }
    }
}
