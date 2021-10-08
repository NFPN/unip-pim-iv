using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Categoria:BaseModel
    {
        [DataMember]
        public int Nome { get; set; }
    }
}
