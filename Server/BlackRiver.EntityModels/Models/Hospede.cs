using System;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Hospede : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public DateTime DataNascimento { get; set; }

        [DataMember]
        public string CPF { get; set; }

        [DataMember]
        public string CNPJ { get; set; }

        [DataMember]
        public string RG { get; set; }

        [DataMember]
        public string Telefone { get; set; }

        [DataMember]
        public string Endereco { get; set; }

        [DataMember]
        public string CEP { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int LoginId { get; set; }

        [DataMember]
        public int VagaEstacionamentoId { get; set; }
    }
}