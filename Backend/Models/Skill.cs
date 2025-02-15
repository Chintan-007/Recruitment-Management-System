using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class Skill
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}
    public DateTime createdAt{get;} = DateTime.Now;

    public string skillName{get;set;} = string.Empty;

    public List<CandidateSkill> candidateSkills = new List<CandidateSkill>();   
    public List<JobSkill> jobSkills{get; set;} = new List<JobSkill>();

}