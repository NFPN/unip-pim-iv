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
        public Municipio MunicipioAtual { get; set; }

        [DataMember]
        public Hotel HotelAtual { get; set; }

        [DataMember]
        public Login Login { get; set; }
    }
}
