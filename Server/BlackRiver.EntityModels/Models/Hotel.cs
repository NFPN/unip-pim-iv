using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Hotel : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Endereco { get; set; }

        [DataMember]
        public int MunicipioId { get; set; }
    }
}
