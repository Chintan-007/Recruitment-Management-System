using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class NewScheduledInterviewDto
{
    [Required]
    public int jobCandidateId{get; set;}

    [Required]
    public int interviewTypeId{get; set;}

    [Required]
    public DateTime interviewDate{get; set;}

    public List<string> roundHandlersIds{get; set;} = new List<string>();

}