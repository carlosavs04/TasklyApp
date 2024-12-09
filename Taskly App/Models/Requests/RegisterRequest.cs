using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Taskly_App.Models.Requests
{
    public class RegisterRequest
    {
        [JsonPropertyName("username")]
        [Required]
        public required string Username { get; set; }

        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public required string Password { get; set; }

        [JsonPropertyName("password_confirmation")]
        [Required]
        public required string PasswordConfirmation { get; set; }
    }
}
