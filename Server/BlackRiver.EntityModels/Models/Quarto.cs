using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Quarto : BaseModel
    {
        [DataMember]
        public int TipoQuarto { get; set; }

        [DataMember]
        public int NumeroQuarto { get; set; }

        [DataMember]
        public int NumeroAndar { get; set; }

        [DataMember]
        public decimal ValorQuarto { get; set; }

        [DataMember]
        public int StatusQuarto { get; set; }

        [DataMember]
        public bool Vip { get; set; }
    }
}