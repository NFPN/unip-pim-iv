using System;
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
        public Hospede HospedePagador { get; set; }
    }
}
