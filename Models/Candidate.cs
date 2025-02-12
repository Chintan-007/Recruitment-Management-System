namespace RecruitmentManagement.Models;

public class Candidate : Users{
    public string resumeLink{get; set;} = string.Empty;
    
    public string organisationName{get;set;} = string.Empty;
   
    public List<CandidateSkill> candidateSkills{get;set;} = new List<CandidateSkill>();    

    public List<CandidateDocs> candidateDocs{get;set;} = new List<CandidateDocs>(); 

    public List<JobCandidate> jobCandidates{get; set;} = new List<JobCandidate>();

    // public List<ScheduledInterview> scheduledInterviews{get; set;} = new List<ScheduledInterview>();

}