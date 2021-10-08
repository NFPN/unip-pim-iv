using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Reserva : BaseModel
    {
        [DataMember]
        public List<Hospede> Hospedes { get; set; }

        [DataMember]
        public Quarto Quarto { get; set; }

        [DataMember]
        public decimal ValorDiaria { get; set; }

        [DataMember]
        public DateTime DataEntrada { get; set; }

        [DataMember]
        public DateTime DataSaida { get; set; }

        [DataMember]
        public DateTime DataCancelamento { get; set; }

        [DataMember]
        public string Status { get; set; }

        [IgnoreDataMember]
        public TimeSpan TempoEstadia => DataSaida.Subtract(DataEntrada);
    }
}