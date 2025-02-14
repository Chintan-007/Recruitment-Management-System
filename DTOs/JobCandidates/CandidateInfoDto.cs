namespace RecruitmentManagement.DTOs.JobCandidates;

public class CandidateInfoDto
{
    public string candidateId{get;set;} = string.Empty;

    public string candidateUserName{get;set;} = string.Empty;

    public int interviewRounds{get;set;}

    public bool isFiltered{get;set;}

    public bool isSelected{get;set;}
}