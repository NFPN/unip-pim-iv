using System;

namespace BlackRiver.Desktop.Views
{
    internal class ReservaDataRow
    {
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public bool Cancelado { get; set; }
    }
}