using System.ComponentModel.DataAnnotations;

namespace webapi_playground.Models.DTOs.Auth;

public class UpdateRolesInfo
{
    [Required]
    public List<Roles> Roles { get; set; }
}
