using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaSV.Catalogo.EN;
using TiendaSV.Catalogo.BL;

namespace TiendaSV.Catalogo.UI.AppWebbMVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        public bool ValidarDatos(Usuario pUsuario)
        {
            //Metodo para validar todos los campos que son obligatorioa para guardar o modificar
            //True => si los datos son validos
            //False => si los datos no son validos
            bool resultado = true;
            if (pUsuario.IdRol == 0)
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pUsuario.Nombre))
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pUsuario.Apellido))
            {
                resultado = false;
            }
            if (pUsuario.FechaNacimiento == null || pUsuario.FechaNacimiento == DateTime.MinValue)
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pUsuario.CorreoElectronico))
            {
                resultado = false;
            }
            if (pUsuario.Id == 0 && string.IsNullOrWhiteSpace(pUsuario.Clave))
            {
                resultado = false;
            }
            return resultado;
        }
        //Acciones CRUD
        public JsonResult Guardar(Usuario pUsuario)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pUsuario) == true)
                {
                    resultado = new UsuarioBL().Guardar(pUsuario);
                }
            }
            catch (Exception ex)
            {

                resultado = 0; ;
            }
            return Json(resultado);
        }
        public JsonResult Modificar(Usuario pUsuario)
        {
            int resultado = 0;
            try
            {
                if(pUsuario.Id > 0 && ValidarDatos(pUsuario) == true)
                {
                    resultado = new UsuarioBL().Modificar(pUsuario);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Usuario pUsuario)
        {
            int resultado = 0;
            try
            {
                if (pUsuario.Id > 0)
                {
                    resultado = new UsuarioBL().Elliminar(pUsuario);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult BuscarPorId(int pId)
        {
            Usuario objUsuario = new UsuarioBL().BuscarPorId(pId);
            return Json(objUsuario);
        }
        public JsonResult Buscar(Usuario pUsuario)
        {
            UsuarioBL usuarioBL = new UsuarioBL();
            List<Usuario> lista = usuarioBL.Buscar(pUsuario);
            //Ejecutar el metodo para llenar las propiedades virtuales de usuario y saber a que rol pertenecen los usuarios
            usuarioBL.CargarPropiedadVirtualRol(lista);
            return Json(lista);
        }
        public JsonResult ObtenerRoles()
        {
            List<Rol> lista = new RolBL().Buscar(new Rol { Estado = 1 });
            return Json(lista);
        }

    }
   
} 