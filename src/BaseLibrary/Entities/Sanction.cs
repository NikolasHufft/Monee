﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Sanction
    {
        public int Id { get; set; }
        //public string CiviId { get; set; }
        //public string FileNumber { get; set; }
        public string Other { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Punishment { get; set; } = string.Empty;
        //[Required]
        //public DateTime PunishmentDate { get; set; }

        // Many-to-one relationship
        public virtual SanctionType? SanctionType { get; set; }
        public int SanctionTypeId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int EmployeeId { get; set; }
        // Relationships : one-to-many
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
