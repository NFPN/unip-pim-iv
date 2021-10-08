using System.Runtime.Serialization;

namespace BlackRiver.EntityModels
{
    [DataContract()]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; set; }
    }
}
