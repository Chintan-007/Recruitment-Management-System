using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Candidates;

public class RegisteredCandidateDto
{
    public string firstName{get; set;} = string.Empty;

    public string lastName{get;set;} = string.Empty;

    public string userName{get; set;} = string.Empty;
    
    public int age{get;set;}
    
    public string email{get;set;} = string.Empty;

    public string phoneNumber{get;set;} = string.Empty;

    public string  position{get; set;} = string.Empty;
    
    public int yearsOfExperience{get; set;}

    public string resumeLink{get; set;} = string.Empty;
    
    public string organisationName{get;set;} = string.Empty;

    public string token{get;set;} = string.Empty;
    public IEnumerable<string> candidateSkills;  
}