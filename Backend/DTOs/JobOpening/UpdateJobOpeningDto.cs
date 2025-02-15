using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.DTOs.JobSkills;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.JobOpening;

public class UpdateJobOpeningDto
{
    public string jobName{get;set;} = string.Empty;

    public string jobDescription{get;set;} = string.Empty;
    
    public int experienceRequired{get;set;}
    
    public double minSalary{get;set;}
    
    public double maxSalary{get;set;}
    
    public int requiredCandidates{get;set;}
    
    public DateTime deadLine{get;set;}
    
    public string addtionalInfo{get;set;} = string.Empty;


    public int positionId{get;set;}
    

    public string organisationId{get;set;} = string.Empty;
    
    public int jobTypeId{get;set;}
    
    public int jobStatusId{get;set;}
    
    public List<CreateJobSkillDto> jobSkills{get; set;} = new List<CreateJobSkillDto>();

}
