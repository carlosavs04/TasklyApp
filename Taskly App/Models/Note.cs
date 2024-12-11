using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly_App.Models
{
    [Table("notes")]
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Body { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CompletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int ResponsibleId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public bool InitialIsCompleted { get; set; }

        [ForeignKey("ResponsibleId")]
        public User Responsible { get; set; } = null!;

        [ForeignKey("TeamId")]
        public Team Team { get; set; } = null!;

        [ForeignKey("CreatedBy")]
        public User Creator { get; set; } = null!;

    }
}
