using System;
using System.ComponentModel;

namespace BlackRiver.Desktop.Views
{
    [Serializable]
    public class DashboardReservaDataItem
    {
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Horário de chegada")]
        public DateTime Horário { get; set; }

        [DisplayName("Numero do Quarto")]
        public int NumeroQuarto { get; set; }

        [DisplayName("Reservado")]
        public bool Status { get; set; }
    }
}
