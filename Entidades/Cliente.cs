using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class Cliente
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string IdCliente { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string ApellidoPaterno { get; set; }
        [DataMember]
        public string ApellidoMaterno { get; set; }
        [DataMember]
        public DateTime FechaNacimiento { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Correo { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember] 
        public DateTime FechaAlta { get; set; }
        [DataMember]
        public DateTime FechaModificacion { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember] 
        public byte[] Foto { get; set; }
        [DataMember]
        public string Sexo { get; set; }
        public Cliente()
        {
            Id = 0;
            IdCliente = string.Empty;
            Nombre = string.Empty;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            FechaNacimiento = DateTime.MinValue;
            Direccion = string.Empty;
            Correo = string.Empty;
            Telefono = string.Empty;
            FechaAlta = DateTime.MinValue;
            FechaModificacion = DateTime.MinValue;
            Password = string.Empty;
            Foto = null;
            Sexo = string.Empty;
        }
    }
    [CollectionDataContract]
    public class ListaCliente : List<Cliente>
    {

    }
}
