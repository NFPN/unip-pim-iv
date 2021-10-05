using System;

namespace BlackRiver.Data.Models
{
    public class Produto
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Lote { get; set; }
        public string Fornecedor { get; set; }

        public decimal Valor { get; set; }
        public int QuantidadeDisponivel { get; set; }


        public DateTime DataValidade { get; set; }
        public DateTime DataAquisicao { get; set; }

    }
}
