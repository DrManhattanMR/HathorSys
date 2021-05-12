using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class SubCategoria
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string IdCategoria { get; set; }
        [DataMember]
        public string IdSubCategoria { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        public SubCategoria()
        {
            Id = 0;
            IdCategoria = string.Empty;
            IdSubCategoria = string.Empty;
            Descripcion = string.Empty;
        }
    }
    public class ListaSubCategoria : List<SubCategoria>
    {

    }
}
