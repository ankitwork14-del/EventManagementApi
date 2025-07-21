using System.ComponentModel.DataAnnotations;

namespace EventManagementApi.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public string? Organizer { get; set; }

        public bool IsPublic { get; set; } = true;
    }
}
