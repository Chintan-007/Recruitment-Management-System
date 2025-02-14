using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class JobCandidate{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}

    public int noOfInterviewRounds{get; set;}

    public bool isFiltered{get; set;} = false;

    public bool isSelected{get; set;} = false;

    public int jobOpeningId{get; set;}
    public JobOpening? jobOpening{get; set;}

    public string candidateId{get; set;} = string.Empty;
    public Candidate? candidate{get; set;}

    public List<ScheduledInterview> scheduledInterviews{get; set;} = new List<ScheduledInterview>();
    public DateTime createdAt{get;} = DateTime.Now;
}