using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class RoundHandler
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int roundHandlerId{get; set;}

    public int scheduledInterviewJobOpeningId{get; set;}
    public string scheduledInterviewCandidateId{get;set;} = string.Empty;
    public int scheduledInterviewInterviewTypeId{get;set;}
    
    public int scheduledInterviewId{get;set;}
    public ScheduledInterview? scheduledInterview{get; set;}

    public string employeeId{get; set;} = string.Empty;
    public Employee? employee{get; set;}

    public int rating{get;set;}
    public string feedback{get;set;} = string.Empty;

    public bool isCompleted{get;set;} = false;
}