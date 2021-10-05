using System.Collections.Generic;

namespace BlackRiver.Data.Models
{
    public class Hotel
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public int TotalVagasEstacionamento => VagasEstacionamento.Count;
        public int TotalQuartos => Quartos.Count;

        public Municipio MunicipioAtual { get; set; }
        public List<Quarto> Quartos {get; set;}
        public List<VagaEstacionamento> VagasEstacionamento { get; set; }
    }
}
