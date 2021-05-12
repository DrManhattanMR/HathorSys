using LogicaNegocios;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestHathor.Controllers
{
    public class SubCategoriaController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                ServiciosLogica Srv = new ServiciosLogica();
                ListaSubCategoria Lista = Srv.ObtenerSubCategoria();
                var response = Request.CreateResponse<IEnumerable<SubCategoria>>(System.Net.HttpStatusCode.Created, Lista);
                return response;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(ex.Message)),
                    ReasonPhrase = "Error al Obtener Lista de Sub Categoria: " + ex.Message.Replace("\n", "").Replace("\r", "")
                };
                throw new HttpResponseException(resp);
            }
        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public bool Post(SubCategoria entidad)
        {
            try
            {
                ServiciosLogica logica = new ServiciosLogica();
                return logica.InsertarSubCategoria(entidad);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                var respuesta = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(mensaje)),
                    ReasonPhrase = "Error al Obtener Lista de SubCategorias: " + ex.Message.Replace("\n", "").Replace("\r", "")


                };
                throw new HttpResponseException(respuesta);
            }
        }

        // PUT api/<controller>/5
        public bool Put(SubCategoria subcategoria)
        {
            try
            {
                ServiciosLogica logica = new ServiciosLogica();
                return logica.EditarSubCategoria(subcategoria);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                var respuesta = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(mensaje)),
                    ReasonPhrase = "Error al Editar SubCategoria: " + ex.Message.Replace("\n", "").Replace("\r", "")


                };
                throw new HttpResponseException(respuesta);
            }
        }

        // DELETE api/<controller>/5
        public bool Delete(string clave)
        {
            try
            {
                ServiciosLogica logica = new ServiciosLogica();
                return logica.EliminarSubCategoria(clave);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                var respuesta = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format(mensaje)),
                    ReasonPhrase = "Error al Elimnar  SubCategoria: " + ex.Message.Replace("\n", "").Replace("\r", "")


                };
                throw new HttpResponseException(respuesta);
            }
        }
    }
}