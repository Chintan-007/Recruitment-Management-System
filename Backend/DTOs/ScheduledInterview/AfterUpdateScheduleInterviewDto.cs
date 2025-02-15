using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class AfterUpdateScheduleInterviewDto
{
    public int jobOpeningId{get; set;}

    public string candidateId{get;set;} = string.Empty;

    public DateTime interviewDate{get; set;}

    public double rating{get; set;}

    public string feedback{get; set;} = string.Empty;

    public string interviewType{get; set;} = string.Empty;

    public List<string> roundHandlers{get; set;} = new List<string>();

}