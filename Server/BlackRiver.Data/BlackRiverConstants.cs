namespace BlackRiver
{
    public class BlackRiverConstants
    {
        public const string DatabaseName = "BlackRiverDB";

        public static string ConnectionString = @$"Data Source=localhost\SQLEXPRESS;Initial Catalog={DatabaseName};Integrated Security=True";
    }
}