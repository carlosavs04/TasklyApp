using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Taskly_App.Models.Requests
{
    public class CodeRequest
    {
        [JsonPropertyName("code")]
        [Required]
        public required string Code { get; set; }
    }
}
