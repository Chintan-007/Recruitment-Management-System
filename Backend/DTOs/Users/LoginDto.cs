
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.Users;

public class LoignDto
{
    
    [Required]
    [StringLength(100), MinLength(2)]
    public string name{get;set;} = string.Empty;

    [Required]
    [StringLength(20),MinLength(8),PasswordPropertyText]
    public string password{get;set;} = string.Empty;

}