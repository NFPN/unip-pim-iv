using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Municipio : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public char[] UF { get; set; }
    }
}