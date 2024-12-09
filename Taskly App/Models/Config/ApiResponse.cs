using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly_App.Models.Config
{
    public class ApiResponse<T>
    {
        public required string Status { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }
        public string? Token { get; set; }
        public object? Errors { get; set; }
    }
}
