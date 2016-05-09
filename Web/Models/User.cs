using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("user",Schema = "public")] 
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }
        
        [Column("firstname")]
        public string FirstName { get; set; }
        
        [Column("lastname")]
        public string LastName { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [Column("lastUpdated")]
        public DateTime LastUpdated { get; set; }
        
        [Column("loggedAt")]
        public DateTime LoggedAt { get; set; }
        
        [Required]
        [Column("usertype")]
        public short UserType { get; set; }
    }
}