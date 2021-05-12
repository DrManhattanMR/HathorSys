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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public void Delete(int id)
        {
        }
    }
}
