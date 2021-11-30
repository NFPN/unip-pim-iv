using System;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Produto : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Marca { get; set; }

        [DataMember]
        public string Lote { get; set; }

        [DataMember]
        public string Fornecedor { get; set; }

        [DataMember]
        public decimal Valor { get; set; }

        [DataMember]
        public int QuantidadeDisponivel { get; set; }

        [DataMember]
        public DateTime DataValidade { get; set; }

        [DataMember]
        public DateTime DataAquisicao { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public int CategoriaId { get; set; }
    }
}
