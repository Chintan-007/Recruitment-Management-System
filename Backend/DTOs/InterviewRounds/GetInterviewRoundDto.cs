using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs;

public class GetInterviewRoundDto
{   
    
    public string candidate{get;set;} = string.Empty;
    public string interviewType{get;set;} = string.Empty;

    public string interviewer{get;set;} = string.Empty;

    public string feedback{get;set;} = string.Empty;

    public int rating{get;set;}
}