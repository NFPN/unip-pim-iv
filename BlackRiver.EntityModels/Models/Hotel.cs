using System.Collections.Generic;
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
        public Municipio MunicipioAtual { get; set; }

        [DataMember]
        public List<Quarto> Quartos { get; set; }

        [DataMember]
        public List<VagaEstacionamento> VagasEstacionamento { get; set; }

        [IgnoreDataMember]
        public int TotalVagasEstacionamento => VagasEstacionamento.Count;

        [IgnoreDataMember]
        public int TotalQuartos => Quartos.Count;
    }
}
