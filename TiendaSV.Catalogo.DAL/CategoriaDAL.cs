using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias
using TiendaSV.Catalogo.EN;
using System.Data.SqlClient;

namespace TiendaSV.Catalogo.DAL
{
    public class CategoriaDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        public static int Guardar(Categoria pCategoria)
        {
            string consulta = @"INSERT INTO Categorias(Nombre, Estado) 
                                VALUES (@Nombre, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pCategoria.Nombre);
            comando.Parameters.AddWithValue("@Estado", pCategoria.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(Categoria pCategoria)
        {
            string consulta = @"UPDATE Categorias
                                SET Nombre = @Nombre, Estado = @Estado 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pCategoria.Nombre);
            comando.Parameters.AddWithValue("@Estado", pCategoria.Estado);
            comando.Parameters.AddWithValue("@Id", pCategoria.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(Categoria pCategoria)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Categorias SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pCategoria.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        public static Categoria BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, Nombre, Estado 
                                FROM Categorias
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Categoria objRol = new Categoria();
            while (reader.Read())
            {
                objRol.Id = reader.GetInt32(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Estado = reader.GetByte(2);
            }
            return objRol;
        }

        // Metodo de busqueda avanzada
        public static List<Categoria> Buscar(Categoria pCategoria)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, Nombre, Estado
                                FROM Categorias";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (!string.IsNullOrWhiteSpace(pCategoria.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Nombre LIKE @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pCategoria.Nombre + "%");
            }

            if (pCategoria.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pCategoria.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Concatenar consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Categoria> lista = new List<Categoria>();
            while (reader.Read())
            {
                Categoria objRol = new Categoria();
                objRol.Id = reader.GetInt32(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Estado = reader.GetByte(2);
                lista.Add(objRol);
            }
            return lista;
        }
        #endregion
    }
}
