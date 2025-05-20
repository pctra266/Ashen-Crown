using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AshenCrown.Domain.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }

        public string Position {  get; set; }
        public int ExperienceRequirement { get; set; }
    }

}
