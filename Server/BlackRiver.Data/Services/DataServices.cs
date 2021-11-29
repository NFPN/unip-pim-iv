using BlackRiver.EntityModels;

namespace BlackRiver.Data.Services
{
    public static class DataServices
    {
        public static readonly BlackRiverDBContextFactory Factory = new();

        public static GenericDataService<Hospede> HospedeService { get; set; }
        public static GenericDataService<Funcionario> FuncionarioService { get; set; }
        public static GenericDataService<Venda> VendaService { get; set; }
        public static GenericDataService<Produto> ProdutoService { get; set; }
        public static GenericDataService<ProdutoCategoria> ProdutoCategoriaService { get; set; }
        public static GenericDataService<Reserva> ReservaService { get; set; }
        public static GenericDataService<UserLogin> UserLoginService { get; set; }
        public static GenericDataService<Hotel> HotelService { get; set; }
        public static GenericDataService<Quarto> QuartoService { get; set; }
        public static GenericDataService<VagaEstacionamento> VagaEstacionamentoService { get; set; }
        public static GenericDataService<Municipio> MunicipioService { get; set; }
        public static GenericDataService<Ocorrencia> OcorrenciaService { get; set; }

        static DataServices()
        {
            HospedeService = new(Factory);
            FuncionarioService = new(Factory);
            VendaService = new(Factory);
            ProdutoService = new(Factory);
            ProdutoCategoriaService = new(Factory);
            ReservaService = new(Factory);
            UserLoginService = new(Factory);
            HotelService = new(Factory);
            QuartoService = new(Factory);
            VagaEstacionamentoService = new(Factory);
            MunicipioService = new(Factory);
            OcorrenciaService = new(Factory);
        }
    }
}
