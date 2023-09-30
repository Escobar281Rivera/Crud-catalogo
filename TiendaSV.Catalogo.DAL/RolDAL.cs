using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Referencias
using TiendaSV.Catalogo.EN;
using System.Data.SqlClient;

namespace TiendaSV.Catalogo.DAL
{
    public class RolDAL
    {
        #region Metodos Crear, Nodificar y Eliminar
        public static int Guardar(Rol pRol)
        {
            string consulta = @"INSERT INTO Roles(Nombre, Estado)
                               VALUES(@Nombre, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pRol.Nombre);
            comando.Parameters.AddWithValue("@Estado", pRol.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(Rol pRol)
        {
            string consulta = @"UPDATE Roles
                                   WHERE Id =@Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pRol.Nombre);
            comando.Parameters.AddWithValue("@Estado", pRol.Estado);
            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(Rol pRol)
        {
            //Eliminacion logica
            string consulta = @"UPDATE Roles SET Estado = 0 WHERE Id =@Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        public static Rol BuscarPorId(byte pId)
        {
            string consulta = @"SELECT  Id, Nombre, Estado
                                FROM Roles
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Rol objRol = new Rol();
            while (reader.Read())
            {
                objRol.Id = reader.GetByte(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Estado = reader.GetByte(2);
            }
            return objRol;
        }
        //Metodos de busqueda avanzada
        public static List<Rol> Buscar(Rol pRol)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, Nombre, Estado
                            FROM Roles";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();
            //Definir los filtros WHERE  de la consulta
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "Nombre LIKE @Nombre";
                comando.Parameters.AddWithValue("@Nombre", "%" + pRol.Nombre + "%");
            }

            if (pRol.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "Estado =@Estado";
                comando.Parameters.AddWithValue("@Estado", pRol.Estado);
            }

            //Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = "WHERE" + whereSQL;

            }

            //Definir consulta SQL Completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Rol> lista = new List<Rol>();
            while (reader.Read())
            {
                Rol objRol = new Rol();
                objRol.Id = reader.GetByte(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Estado = reader.GetByte(2);
                lista.Add(objRol);
            }
            return lista;
        }
        #endregion
    }
}


