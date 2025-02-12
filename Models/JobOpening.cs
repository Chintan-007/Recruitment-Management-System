using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class JobOpening
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int JobOpeningId{get;set;}

    public string jobDescription{get;set;} = string.Empty;
    public int experienceRequired{get;set;}
    public double minSalary{get;set;}
    public double maxSalary{get;set;}
    public int requiredCandidates{get;set;}
    public DateTime deadLine{get;set;}
    public string addtionalInfo{get;set;} = string.Empty;

    public int positionId;
    public Position position;

    public string organisationId{get;set;}
    public Organisation organisation{get;set;}

    public int jobTypeId{get;set;}
    public JobType jobType{get;set;}

    public int jobStatusId{get;set;}
    public JobStatus jobStatus{get;set;}

    public List<JobSkill> jobSkills{get; set;} = new List<JobSkill>();
    public List<JobCandidate> jobCandidates{get; set;} = new List<JobCandidate>();
    // public List<ScheduledInterview> scheduledInterviews{get; set;} = new List<ScheduledInterview>();

    public DateTime createdAt = DateTime.Now;
}