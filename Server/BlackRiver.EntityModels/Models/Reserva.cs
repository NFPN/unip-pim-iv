using System;
using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class Reserva : BaseModel
    {
        [DataMember]
        public decimal ValorDiaria { get; set; }

        [DataMember]
        public DateTime DataEntrada { get; set; }

        [DataMember]
        public DateTime DataSaida { get; set; }

        [DataMember]
        public DateTime? DataCancelamento { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int QuartoId { get; set; }

        [DataMember]
        public int HospedeId { get; set; }

        [IgnoreDataMember]
        public TimeSpan TempoEstadia => DataSaida.Subtract(DataEntrada);
    }
}