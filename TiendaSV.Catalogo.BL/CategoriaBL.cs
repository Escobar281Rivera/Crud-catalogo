using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias
using TiendaSV.Catalogo.EN;
using TiendaSV.Catalogo.DAL;

namespace TiendaSV.Catalogo.BL
{
    public class CategoriaBL
    {
        public int Guardar(Categoria pCategoria)
        {
            pCategoria.Estado = 1;
            return CategoriaDAL.Guardar(pCategoria);
        }
        public int Modificar(Categoria pCategoria)
        {
            pCategoria.Estado = 1;
            return CategoriaDAL.Modificar(pCategoria);
        }
        public int Eliminar(Categoria pCategoria)
        {
            return CategoriaDAL.Eliminar(pCategoria);
        }
        public Categoria BuscarPorId(int pId)
        {
            return CategoriaDAL.BuscarPorId(pId);
        }
        public List<Categoria> Buscar(Categoria pCategoria)
        {
            pCategoria.Estado = 1;
            return CategoriaDAL.Buscar(pCategoria);
        }
    }
}
