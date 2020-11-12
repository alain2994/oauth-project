using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace exercise_3.Repository.Models
{
    [Index(nameof(Username), Name = "UQ__Accounts__F3DBC572DAB2D3CE", IsUnique = true)]
    public partial class Account
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(30)]
        public string Email { get; set; }
        [Column("address")]
        [StringLength(30)]
        public string Address { get; set; }
        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
    }
}
