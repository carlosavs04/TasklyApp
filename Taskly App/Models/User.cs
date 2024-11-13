﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly_App.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public string? PasswordConfirmation { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();

        public ICollection<Team> OwnedTeams { get; set; } = new List<Team>();

        public ICollection<Note> CreatedNotes { get; set; } = new List<Note>();

        public ICollection<Note> ResponsibleNotes { get; set; } = new List<Note>();
    }
}