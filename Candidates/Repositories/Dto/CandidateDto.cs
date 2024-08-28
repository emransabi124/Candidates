using System.ComponentModel.DataAnnotations;

namespace Candidates.Repositories.Dto
{
    public class CandidateDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string CallTime { get; set; }

        [Url]
        public string LinkedInProfileUrl { get; set; }

        [Url]
        public string GitHubProfileUrl { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
