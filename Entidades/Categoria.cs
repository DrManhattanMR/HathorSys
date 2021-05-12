using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class Categoria
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string IdCategoria { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        public Categoria()
        {
            Id = 0;
            IdCategoria = string.Empty;
            Descripcion = string.Empty;
        }
    }
    public class ListaCategoria : List<Categoria>
    {

    }
}
