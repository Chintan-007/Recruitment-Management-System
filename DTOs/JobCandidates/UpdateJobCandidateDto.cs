using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.JobCandidates;

public class UpdateJobCandidateDto
{
    // [Required]
    // public string candidateId{get;set;} = string.Empty;

    [Required]
    public int interviewRounds{get;set;}

    [Required]
    public bool isFiltered{get;set;}

    [Required]
    public bool isSelected{get;set;}

}