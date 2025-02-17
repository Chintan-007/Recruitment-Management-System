
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Users;

public class RegisterUserDto
{
    
    [Required]
    [StringLength(100), MinLength(2)]
    public string firstname{get;set;} = string.Empty;

    [Required]
    [StringLength(100), MinLength(2)]
    public string lastname{get;set;} = string.Empty;

    [Required]
    [StringLength(100), MinLength(2)]
    public string username{get;set;} = string.Empty;

    [Required]
    [StringLength(100),MinLength(5),EmailAddress]
    public string email{get;set;} = string.Empty;

    [Required]
    [StringLength(15),MinLength(10),Phone]
    public string mobile{get;set;} = string.Empty;

    [Required]
    [Range(18, 80, ErrorMessage = "Age must be between 18 & 80")]
    public int age{get;set;}

    [Required]
    [StringLength(20),MinLength(8),PasswordPropertyText]
    public string password{get;set;} = string.Empty;

}