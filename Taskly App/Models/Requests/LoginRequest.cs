using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Taskly_App.Models.Requests
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        [EmailAddress]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public required string Password { get; set; }
    }
}
