using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Taskly_App.Models.Config
{
    public class ApiException : Exception
    {
        public string? ApiMessage { get; }
        public Dictionary<string, List<string>>? Errors { get; }

        public ApiException(string? apiMessage, object? errors) : base(apiMessage)
        {
            ApiMessage = apiMessage;
            Errors = errors as Dictionary<string, List<string>> ?? JsonSerializer.Deserialize<Dictionary<string, List<string>>>(JsonSerializer.Serialize(errors));
        }
    }
}
