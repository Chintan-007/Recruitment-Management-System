namespace RecruitmentManagement.Models;

public class CandidateSkill
{
    public string candidateId{get;set;} = string.Empty;
    public Candidate candidate{get;set;}

    public int skillId{get;set;}
    public Skill skill{get;set;}
}