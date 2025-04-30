using System.ComponentModel.DataAnnotations;

namespace AshenCrown.Domain.Entities
{
    public class Mission
    {
        public int Id { get; set; }
        
        public string? Content { get; set; }

        public bool? IsComplete { get; set; }
    }
}
