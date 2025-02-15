using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Candidates;

public class CreateCandidateDto
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
    public int  positionId{get; set;}
    
    [Required]
    public int yearsOfExperience{get; set;}

    [Required]
    public string resumeLink{get; set;} = string.Empty;
    
    [Required]
    public string organisationName{get;set;} = string.Empty;

    [Required]
    public List<int> candidateSkillsIds{get;set;} = new List<int>();   
}