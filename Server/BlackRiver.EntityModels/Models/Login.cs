using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    public class Login : BaseModel
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Type { get; set; }
    }
}
