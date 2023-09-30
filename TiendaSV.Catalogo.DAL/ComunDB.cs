using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias
using System.Data;
using System.Data.SqlClient;

namespace TiendaSV.Catalogo.DAL
{
    public class ComunDB
    {
        const string StrConexion = @"Data Source=.;Initial Catalog=TiendSVCatalogo;Integrated Security=True";
        private static SqlConnection ObtenerConexion()
        {
            SqlConnection connection = new SqlConnection(StrConexion);
            connection.Open();
            return connection;
        }
        public static SqlCommand ObtenerComando()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ObtenerConexion();
            return command;
        }
        public static int EjecutarComando(SqlCommand pComando)
        {
            int resultado = pComando.ExecuteNonQuery();
            pComando.Connection.Close();
            return resultado;
        }
        public static SqlDataReader EjecutarComandoReader(SqlCommand pComando)
        {
            SqlDataReader reader = pComando.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

    }
}

