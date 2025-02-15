using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class NewJobCandidateDto
{
    [Required]
    public string candidateId{get;set;} = string.Empty;

    [Required]
    public int interviewRounds{get;set;}

}