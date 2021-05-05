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
        public bool InsertarCliente(Cliente entidad)
        {
            ServicioCliente srv = new ServicioCliente();
            return srv.AgregarCliente(entidad);
        }
    }
}
