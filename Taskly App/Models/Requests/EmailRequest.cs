using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly_App.Models.Requests
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
