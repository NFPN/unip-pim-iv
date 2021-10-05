using System;
using System.Collections.Generic;

namespace BlackRiver.Data.Models
{
    public class Reserva
    {
        public List<Hospede> Hospedes { get; set; }
        public Quarto Quarto { get; set; }

        public decimal ValorDiaria { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime DataCancelamento { get; set; }
        public string Status { get; set; }

        public TimeSpan TempoEstadia => DataSaida.Subtract(DataEntrada);
    }
}