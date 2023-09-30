using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Referencias
using TiendaSV.Catalogo.EN;
using TiendaSV.Catalogo.DAL;


namespace TiendaSV.Catalogo.BL
{
    public class UsuarioBL
    {
        public int Guardar(Usuario pUsuario)
        {
            pUsuario.Estado = 1;
            return UsuarioDAL.Guardar(pUsuario);
        }
        public int Modificar(Usuario pUsuario)
        {
            pUsuario.Estado = 1;
            return UsuarioDAL.Modificar(pUsuario);
        }
        public int Elliminar(Usuario pUsuario)
        {
            return UsuarioDAL.Eliminar(pUsuario);
        }
        public Usuario BuscarPorId( int pId)
        {
            return UsuarioDAL.BuscarPorId(pId);
        }
        public List<Usuario>Buscar(Usuario pUsuario)
        {
            pUsuario.Estado = 1;
            return UsuarioDAL.Buscar(pUsuario); 
        }
        public Usuario IniciarSesion(Usuario pUsuario)
        {
            return UsuarioDAL.IniciarSesion(pUsuario);
        }
        public void CargarPropiedadVirtualRol(List<Usuario>pLista,Action<List<Rol>> pAccion = null)
        {
            //Metodos para cargar los datos de la propiedad virtual de Rol
            if (pLista.Count > 0)
            {
                //Dictionary de Roles
                //byte = Tipo de datos de roles
                //Rol = Clase para guardar los datos de roles
                Dictionary<byte, Rol > diccionarioRoles = new Dictionary<byte, Rol>();
                //Buscar los roles y cargarlos a los usuarios en su propiedad virtual
                foreach (var obj in pLista)
                {
                    //verificar si exite el rol en el diccionario
                    if (diccionarioRoles.ContainsKey(obj.IdRol) == true)
                    {
                        //Cargar propiedad virtual desde el diccionario de roles
                        obj.Rol = diccionarioRoles[obj.IdRol];
                    }
                    else
                    {
                        //Si el rol no existe en el diccionario, se busca en la bas de datos y se agrega al diccionario
                        diccionarioRoles.Add(obj.IdRol, RolDAL.BuscarPorId(obj.IdRol));
                        //Cargar propiedad vistual desde el diccionario de roles
                        obj.Rol = diccionarioRoles[obj.IdRol];
                    }
                }
                //Accion auxiliar para sobrecargar otro propiedad virtual dentro de la clase ROL
                if (pAccion != null && diccionarioRoles.Count > 0)
                {
                    pAccion(diccionarioRoles.Select(x => x.Value).ToList());
                }
            }
            
        }
    }
}

