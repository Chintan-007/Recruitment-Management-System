namespace RecruitmentManagement.Models;

public class Candidate : Users{
    public string resumeLink{get; set;} = string.Empty;
    
    public string organisationName{get;set;} = string.Empty;
    public int yearsOfExperience{get;set;}

    public int positonId{get;set;}
    public Position position{get;set;}
    public List<CandidateSkill> candidateSkills{get;set;} = new List<CandidateSkill>();     
}