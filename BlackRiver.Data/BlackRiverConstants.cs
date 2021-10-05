namespace BlackRiver
{
    public class BlackRiverConstants
    {
        public const string DatabaseName = "BlackRiverDB";

        public static string ConnectionString = @$"Data Source=(localdb)\MSSQLocalDB;Initial Catalog={DatabaseName};Integrated Security=True";
    }
}