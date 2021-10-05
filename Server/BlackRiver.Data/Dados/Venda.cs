using System;

namespace BlackRiver.Data.Models
{
    public class Venda
    {
        public decimal ValorPago { get; set; }
        public DateTime DataVenda { get; set; }
        public Hospede HospedePagador { get; set; }
    }
}
