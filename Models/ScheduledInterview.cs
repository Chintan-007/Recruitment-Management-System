using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class ScheduledInterview
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int scheduledInterviewId{get; set;}

    public DateTime interviewDate{get; set;}

    public double rating{get; set;}

    public string feedback{get; set;} = string.Empty;

    public bool isCleared{get; set;} = false;

    
    public int jobOpeningId{get; set;}
    // public JobOpening jobOpening{get;set;}
    public string candidateId{get;set;} = string.Empty;
    // public Candidate candidate{get;set;}
    public JobCandidate jobCandidate{get; set;}

    public int interviewTypeId{get; set;}
    public InterviewType interviewType{get; set;}

    public List<RoundHandler> roundHandlers{get; set;} = new List<RoundHandler>();
}