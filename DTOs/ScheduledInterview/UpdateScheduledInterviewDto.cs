using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class UpdateScheduledInterviewDto
{
    // public int interviewTypeId{get; set;}
    public DateTime interviewDate{get; set;}

    public bool isCleared{get;set;}

    public double rating{get; set;}

    public string feedback{get; set;} = string.Empty;
    public List<string> roundHandlersIds{get; set;} = new List<string>();

}