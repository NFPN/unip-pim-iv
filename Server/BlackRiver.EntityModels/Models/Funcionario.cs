using System;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Funcionario : BaseModel
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public DateTime DataNascimento { get; set; }

        [DataMember]
        public string CPF { get; set; }

        [DataMember]
        public string RG { get; set; }

        [DataMember]
        public string CTPS { get; set; }

        [DataMember]
        public string Telefone { get; set; }

        [DataMember]
        public string Endereco { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Departamento { get; set; }

        [DataMember]
        public string Cargo { get; set; }

        [DataMember]
        public bool Ativo { get; set; }

        [DataMember]
        public int MunicipioId { get; set; }

        [DataMember]
        public int HotelId { get; set; }

        [DataMember]
        public int LoginId { get; set; }
    }
}
