using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class RoundHandler
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int roundHandlerId{get; set;}

    public int scheduledInterviewJobOpeningId{get; set;}
    public string scheduledInterviewCandidateId{get;set;} = string.Empty;
    public int scheduledInterviewInterviewTypeId{get;set;}
    
    public ScheduledInterview? scheduledInterview{get; set;}

    public string employeeId{get; set;} = string.Empty;
    public Employee? employee{get; set;}
}