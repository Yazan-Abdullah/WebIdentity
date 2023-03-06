using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebIdentity.ViewModel
{
    public class RegesterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string PassWorld {get;set;} = "";
        [Required]
        [Compare("PassWorld")]
        [Display(Name ="Conferm PassWorld")]
        public string ConfermPassWorld {get;set;} = "";
    }
    public class RegesterListViewModel
    {
        public List<IdentityUser> Users { get; set; }
    }
    public class RegesterEditViewModel
    {
        public string Id { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
