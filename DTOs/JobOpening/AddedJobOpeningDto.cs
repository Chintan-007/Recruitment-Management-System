using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.JobOpening;

public class AddedJobOpeningDto
{
    public string jobName{get;set;} = string.Empty;

    public string jobDescription{get;set;} = string.Empty;
    
    public int experienceRequired{get;set;}
    
    public double minSalary{get;set;}
    
    public double maxSalary{get;set;}
    
    public int requiredCandidates{get;set;}
    
    public DateTime deadLine{get;set;}
    
    public string addtionalInfo{get;set;} = string.Empty;

    public string position{get;set;} = string.Empty;
    
    public string organisation{get;set;} = string.Empty;
    
    public string jobType{get;set;} = string.Empty;
    
    public string jobStatus{get;set;} = string.Empty;
 
    public List<string>? jobSkills{get; set;} = [];

}