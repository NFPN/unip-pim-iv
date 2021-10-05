﻿using System;

namespace BlackRiver.Data.Models
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string CTPS { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }

        public Hotel HotelAtual { get; set; }
        public Municipio MunicipioAtual { get; set; }
    }
}
