using System;

namespace BlackRiver.Desktop.Views
{
    public partial class QuartoControl
    {
        [Serializable]
        public class QuartoDataRow
        {
            public int Numero { get; set; }
            public int Andar { get; set; }
            public string Status { get; set; }
            public string Tipo { get; set; }
            public bool Vip { get; set; }
            public decimal Diaria { get; set; }
        }
    }
}
