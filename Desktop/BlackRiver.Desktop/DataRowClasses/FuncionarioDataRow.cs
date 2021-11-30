using System;

namespace BlackRiver.Desktop.Views
{
    public partial class FuncionariosControl
    {
        [Serializable]
        public class FuncionarioDataRow
        {
            public string Nome { get; set; }
            public string Cargo { get; set; }
            public string Departamento { get; set; }
            public string Email { get; set; }
        }
    }
}
