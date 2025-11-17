using NIBAUTH.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;

namespace NIBAUTH.Domain.Entities
{
    public class Camera:DefaultTable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public BrancheBlock BrancheBlock { get; set; }
    }
}
