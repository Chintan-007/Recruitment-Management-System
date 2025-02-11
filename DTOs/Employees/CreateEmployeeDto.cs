using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.Employees;

public class CreateEmployeeDto
{
    
    [Required]
    [StringLength(100),MinLength(2)]
    public string firstName{get; set;} = string.Empty;

    [Required]
    [StringLength(100),MinLength(2)]
    public string lastName{get;set;} = string.Empty;

    [Required]
    [StringLength(100),MinLength(2)]
    public string userName{get; set;} = string.Empty;
    
    [Required]
    [Range(18, 80, ErrorMessage = "Age must be between 18 & 80")]
    public int age{get;set;}
    
    [Required]
    [StringLength(70),MinLength(5)]
    public string email{get;set;} = string.Empty;

    [Required]
    [StringLength(15),MinLength(10)]
    public string phoneNumber{get;set;} = string.Empty;

    [Required]
    public string password{get; set;} = string.Empty;

    [Required]
   public string organisationId{get;set;} = string.Empty;

    [Required]
    public int  positionId{get; set;}
    
    [Required]
    public int yearsOfExperience{get; set;}
}