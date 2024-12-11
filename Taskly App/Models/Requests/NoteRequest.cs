using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Taskly_App.Models.Requests
{
    public class NoteRequest
    {
        [JsonPropertyName("title")]
        [Required]
        public required string Title { get; set; }

        [JsonPropertyName("body")]
        [Required]
        public required string Body { get; set; }

        [JsonPropertyName("responsible_id")]
        [Required]
        public int ResponsibleId { get; set; }

        [JsonPropertyName("team_id")]
        [Required]
        public int TeamId { get; set; }
    }
}
