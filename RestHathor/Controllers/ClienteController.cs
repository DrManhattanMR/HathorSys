using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LogicaNegocios;
namespace RestHathor.Controllers
{
    public class ClienteController : ApiController
    {
        // GET: api/Cliente
        public HttpResponseMessage Get(string idCliente)
        {
            try
            {
                ServiciosLogica Srv = new ServiciosLogica();
                ListaCliente Lista = Srv.ObtenerClientes(idCliente);
                var response = Request.CreateResponse<IEnumerable<Cliente>>(HttpStatusCode.Created, Lista);
                return response;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(ex.Message)),
                    ReasonPhrase = "Error al Obtener Lista de Clientes: " + ex.Message.Replace("\n", "").Replace("\r", "")
                };
                throw new HttpResponseException(resp);
            }
        }

        // GET: api/Cliente/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cliente
        public bool Post(Cliente entidad)
        {
            try
            {
                ServiciosLogica srv = new ServiciosLogica();
                return srv.InsertarCliente(entidad);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(mensaje)),
                    ReasonPhrase = "Error al Insertar Cliente : " + mensaje.Replace("\n", "").Replace("\r", "")
                };
                throw new HttpResponseException(resp);
            }
        }

        // PUT: api/Cliente/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cliente/5
        public bool Delete(string idCliente)
        {
            try
            {
                ServiciosLogica logica = new ServiciosLogica();
                return logica.EliminarCliente(idCliente);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                var respuesta = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(mensaje)),
                    ReasonPhrase = "Error al Elimnar  Cliente: " + ex.Message.Replace("\n", "").Replace("\r", "")


                };
                throw new HttpResponseException(respuesta);
            }
        }
    }
}
