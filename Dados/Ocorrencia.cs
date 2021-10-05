using System;

namespace BlackRiver.Data.Models
{
    public class Ocorrencia
    {
        public bool Status { get; set; }
        public DateTime Data { get; set; }
        public string Departamento { get; set; }
        public Reserva Reserva { get; set; }
    }
}
