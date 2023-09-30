using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaSV.Catalogo.EN
{
    public class Usuario
    {
        public int Id { get; set; }
        public byte IdRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public string Clave { get; set; }
        public byte Estado { get; set; }

        //propiedades virtiales
        public virtual Rol Rol {get; set;}

        //Enums
        public enum EnumRol
        {
            ADMINISTRADOR = 1,
            CLIENTE = 2,
        }
    }
}
