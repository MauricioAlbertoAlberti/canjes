using System.Data;
using Canjes.Models;
using Microsoft.Data.SqlClient;

namespace Canjes.Datos
{
    public class CanjeDB
    {
        private static string cadena = "Server=.;Database=Canje;User=sa;Password=sqladmin;Trusted_Connection=True;Trust Server Certificate=True";
        private SqlConnection conexion;
        private SqlCommand comando;

        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public CanjeDB()
        {
            conexion = new SqlConnection(cadena);
            comando = new SqlCommand();
        }

        public void SetearQuery(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        //public void SetearProcedimiento(string sp)
        //{
        //    comando.CommandType = System.Data.CommandType.StoredProcedure;
        //    comando.CommandText = sp;
        //}

        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void Leer()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }

            catch (Exception ex) { throw ex; }
        }

        public int EjecutarNonQuery()
        {
            int filasAfectadas = -1;

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                filasAfectadas  = comando.ExecuteNonQuery();
                return filasAfectadas;
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
           
        }

        public void CerrarConexion()
        {
            comando.Parameters.Clear();
            if (comando.Connection.State == ConnectionState.Open)
            {
                comando.Connection.Close();
            }

            if (lector != null && !lector.IsClosed)
            {
                lector.Close();
            }

            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
            comando.Dispose();
        }

    }
}
