using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class AddedJobCandidateDto
{

    public string jobName{get;set;} = string.Empty;

    public string jobDescription{get;set;} = string.Empty;
    
    public string position{get;set;} = string.Empty;
    
    public string organisation{get;set;} = string.Empty;
    
    public string jobType{get;set;} = string.Empty;
    
    public string jobStatus{get;set;} = string.Empty;

    // public List<string?> candidates{get;set;} = new List<string?>();
    public List<CandidateInfoDto> candidates{get;set;} = new List<CandidateInfoDto>();
    

}