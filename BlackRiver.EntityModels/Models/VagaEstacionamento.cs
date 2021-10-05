using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class VagaEstacionamento : BaseModel
    {
        [DataMember]
        public string NumeroVaga { get; set; }

        [DataMember]
        public string Placa { get; set; }
    }
}
