namespace BlackRiver.EntityModels
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
}
