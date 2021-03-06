using System;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Ocorrencia : BaseModel
    {
        [DataMember]
        public decimal Score { get; set; }

        [DataMember]
        public bool Status { get; set; }

        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public string Departamento { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}
