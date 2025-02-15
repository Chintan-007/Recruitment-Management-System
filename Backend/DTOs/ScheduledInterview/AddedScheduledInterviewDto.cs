using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class AddedScheduledInterviewDto
{
    public int jobOpeningId{get; set;}

    public string candidateId{get;set;} = string.Empty;

    public DateTime interviewDate{get; set;}

    public bool isCleared{get; set;} = false;

    public string interviewType{get; set;} = string.Empty;

    public List<string> roundHandlers{get; set;} = new List<string>();

}