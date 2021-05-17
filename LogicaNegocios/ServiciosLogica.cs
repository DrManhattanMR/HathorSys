using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoDatos;
namespace LogicaNegocios
{
    public class ServiciosLogica
    {
        #region Cliente
        public bool InsertarCliente(Cliente entidad)
        {
            ServicioCliente srv = new ServicioCliente();
            return srv.AgregarCliente(entidad);
        }
        public ListaCliente ObtenerClientes(string idCliente)
        {
            ServicioCliente srv = new ServicioCliente();
            return srv.ObtenerClientes(idCliente);
        }
        public bool EliminarCliente(string idCliente)
        {
            ServicioCliente srv = new ServicioCliente();
            return srv.EliminarCliente(idCliente);
        }
        public bool EditarCliente(Cliente entidad)
        {
            ServicioCliente srv = new ServicioCliente();
            return srv.EditarCliente(entidad);
        }
        #endregion Region


        #region Categoria
        public ListaCategoria ObtenerCategoria()
        {
            ServicioCategoria srv = new ServicioCategoria();
            return srv.ObteberCategoria();
        }
        public bool InsertarCategoria(Categoria entidad)
        {
            ServicioCategoria srv = new ServicioCategoria();
            return srv.InsertarCategoria(entidad);
        }
        public bool EditarCategoria(Categoria categoria)
        {
            ServicioCategoria srv = new ServicioCategoria();
            return srv.EditarCategoria(categoria);
        }
        public bool EliminarCategoria(string clave)
        {
            ServicioCategoria srv = new ServicioCategoria();
            return srv.EliminarCategoria(clave);
        }
        #endregion Categoria


        #region SubCategoria
        public ListaSubCategoria ObtenerSubCategoria()
        {
            ServicioSubCategoria srv = new ServicioSubCategoria();
            return srv.ObtenerSubCategoria();
        }
        public bool InsertarSubCategoria(SubCategoria entidad)
        {
            ServicioSubCategoria srv = new ServicioSubCategoria();
            return srv.InsertarSubCategoria(entidad);
        }
        public bool EditarSubCategoria(SubCategoria subcategoria)
        {
            ServicioSubCategoria srv = new ServicioSubCategoria();
            return srv.EditarSubCategoria(subcategoria);
        }
        public bool EliminarSubCategoria(string clave)
        {
            ServicioSubCategoria srv = new ServicioSubCategoria();
            return srv.EliminarSubCategoria(clave);
        }
        #endregion SubCategoria
    }
}
