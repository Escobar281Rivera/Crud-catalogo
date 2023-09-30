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
    public class UsuarioDAL
    {
        #region Metodos para Crear, Modificar y Eliminar
        public static int Guardar(Usuario pUsuario)
        {
            string consulta = @"INSERT INTO Usuarios(IdRol, Nombre, Apellido, FechaNacimiento, CorreoElectronico, Clave, Estado)
                                VALUES(@IdRol, @Nombre, @Apellido, @FechaNacimiento,@CorreoElectronico, @Clave, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdRol", pUsuario);
            comando.Parameters.AddWithValue("@Nombre", pUsuario);
            comando.Parameters.AddWithValue("@Apellido", pUsuario);
            comando.Parameters.AddWithValue("@FechaNacimiento", pUsuario);
            comando.Parameters.AddWithValue("@CorreoElectronico", pUsuario);
            comando.Parameters.AddWithValue("@Clave", pUsuario);
            comando.Parameters.AddWithValue("@Estado", pUsuario);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(Usuario pUsuario)
        {
            string consulta = @"UPDATE Usuarios
                                SET IdRol =@IdRol, Nombre=@Nombre, Apellido = @Apellido
                                            FechaNacimiento=@FechaNacimiento, CorreoElectronico=@CorreoElectronico, Estado =@Estado
                                             WHERE Id = @Id";

            SqlCommand comando =ComunDB.ObtenerComando();  
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
            comando.Parameters.AddWithValue("Nombre", pUsuario.Nombre);
            comando.Parameters.AddWithValue("@Apellido", pUsuario.Apellido);
            comando.Parameters.AddWithValue("@FechaNacimiento", pUsuario.FechaNacimiento);
            comando.Parameters.AddWithValue("@CorreoElectronico", pUsuario.CorreoElectronico);
            comando.Parameters.AddWithValue("@Estado", pUsuario.Estado);
            comando.Parameters.AddWithValue("@Id", pUsuario.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(Usuario pUsuario)
        {
            //Eliminacion logica
            string consulta = @"UPDATE Usuarios SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pUsuario.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion Metodos de busqueda
        public static Usuario BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdRol, Nombre, Apellido, FechaNacimiento, CorroElectronico, Estado
                                FROM Usuarios
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Usuario objRol = new Usuario();
            while(reader.Read())
            {
                objRol.Id = reader.GetInt32(0);
                objRol.IdRol = reader.GetByte(1);
                objRol.Nombre = reader.GetString(2);
                objRol.Apellido = reader.GetString(3);
                objRol.FechaNacimiento = reader.GetDateTime(4);
                objRol.CorreoElectronico = reader.GetString(5);
                objRol.Estado = reader.GetByte(0);
            }
            return objRol;
        }
        //Metodos de busqueda avanzada
        public static List<Usuario>Buscar(Usuario pUsuario)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, IdRol, Nombre, Apellido, FechaNacimiento, CorreoElectronico, Estado
                                FROM Usuarios";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            //Definir los filtros WHERE de la consulta
            if (pUsuario.IdRol > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "IdRol =@IdRol";
                comando.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
            }
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "Nombre LIKE @Nombre";
                comando.Parameters.AddWithValue("@Nombre", "%" + pUsuario.Nombre + "%");
            }
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "Apellido LIKE @Apellido";
                comando.Parameters.AddWithValue("@Apellido", "%" + pUsuario.Apellido + "%");
            }
            if (pUsuario.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "Estado =@Estado";
                comando.Parameters.AddWithValue("@Estado", pUsuario.Estado);
            }

            //Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = "WHERE" + whereSQL;
            }

            //Definir consulta SQL Completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Usuario> lista = new List<Usuario>();
            while (reader.Read())
            {
                Usuario objRol = new Usuario();
                objRol.Id = reader.GetInt32(0);
                objRol.IdRol = reader.GetByte(1);
                objRol.Nombre = reader.GetString(2);
                objRol.Apellido = reader.GetString(3);
                objRol.FechaNacimiento = reader.GetDateTime(4);
                objRol.CorreoElectronico = reader.GetString(5);
                objRol.Estado = reader.GetByte(6);
                lista.Add(objRol);
            }
            return lista;
        }
        public static Usuario IniciarSesion(Usuario pUsuario)
        {
            string consulta = @"SELECT Id, IdRol, Nombre, Apellido, FechaNacimineto,CorreoElectronico, Estado
                                FROM Usuarios
                                WHERE CorreoElectronico = @CorreoElectronico AND Clave = @Clave AND Estado = 1";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@CorreoElectronico", pUsuario.CorreoElectronico);
            comando.Parameters.AddWithValue("@Clave", pUsuario.Clave);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Usuario objRol = new Usuario();
            while (reader.Read())
            {
                objRol.Id = reader.GetInt32(0);
                objRol.IdRol = reader.GetByte(1);
                objRol.Nombre = reader.GetString(2);
                objRol.Apellido = reader.GetString(3);
                objRol.FechaNacimiento = reader.GetDateTime(4);
                objRol.CorreoElectronico = reader.GetString(5);
                objRol.Estado = reader.GetByte(6);
            }
            return objRol;
        }
    }

}
