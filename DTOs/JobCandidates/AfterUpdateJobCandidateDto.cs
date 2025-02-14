using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class AfterUpdateJobCandidateDto
{
    [Required]
    public string candidateId{get;set;} = string.Empty;

    [Required]
    public int jobId{get;set;}

    [Required]
    public int interviewRounds{get;set;}

    [Required]
    public bool isFiltered{get;set;}

    [Required]
    public bool isSelected{get;set;}

}