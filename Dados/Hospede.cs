using System;
using System.Collections.Generic;

namespace BlackRiver.Data.Models
{
    public class Hospede
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }


        public List<Reserva> Reservas { get; set; }
    }
}