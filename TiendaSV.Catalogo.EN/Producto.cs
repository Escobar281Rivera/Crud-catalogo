using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaSV.Catalogo.EN
{
    public class Producto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public string ImagenURL { get; set; }
        public byte Estatus { get; set; }
        public byte Estado { get; set; }

        //propiedades virtuales
        public virtual Usuario Usuario { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
