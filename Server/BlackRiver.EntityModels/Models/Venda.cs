using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Venda : BaseModel
    {
        [DataMember]
        public decimal ValorPago { get; set; }

        [DataMember]
        public DateTime DataVenda { get; set; }

        [DataMember]
        public int HospedeId { get; set; }

        [DataMember]
        public List<Produto> Produtos { get; set; }
    }
}
