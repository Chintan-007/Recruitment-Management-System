using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.DTOs.JobSkills;

namespace RecruitmentManagement.DTOs.JobOpening;

public class CreateJobOpeningDto
{
    [Required]
    [StringLength(100),MinLength(2)]
    public string jobName{get;set;} = string.Empty;

    [Required]
    public string jobDescription{get;set;} = string.Empty;
    
    [Required]
    public int experienceRequired{get;set;}
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Salary can't be negative")]
    public double minSalary{get;set;}
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Salary can't be negative")]
    public double maxSalary{get;set;}
    
    public int requiredCandidates{get;set;}
    
    [Required]
    public DateTime deadLine{get;set;}
    
    public string addtionalInfo{get;set;} = string.Empty;


    [Required]
    public int positionId{get;set;}
    
    
    [Required]
    public int jobTypeId{get;set;}
    
    [Required]
    public int jobStatusId{get;set;}
    
    [Required]
    public List<CreateJobSkillDto> jobSkills{get; set;} = new List<CreateJobSkillDto>();

}
