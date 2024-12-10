using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Taskly_App.Models.Requests
{
    public class TeamRequest
    {
        [JsonPropertyName("name")]
        [Required]
        public required string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public required string Description { get; set; }
    }
}
