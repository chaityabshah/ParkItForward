using System.Runtime.Serialization;

namespace Planner.Models.DTO
{
    [DataContract]
    public class TaskDto
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "start")]
        public string Start { get; set; }

        [DataMember(Name = "end")]
        public string End { get; set; }

        [DataMember(Name = "url")]
        public string Id { get; set; }
    }
}