using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class ProdutoCategoria : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }
    }
}
