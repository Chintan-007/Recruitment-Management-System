using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RecruitmentManagement.Models;

public class JobSkill
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int jobSkillId{get; set;}

    public int jobOpeningId{get; set;}
    public JobOpening jobOpening{get; set;}

    public int skillId{get; set;}
    public Skill skill{get; set;}

    public bool isRequired = true;
    public DateTime createdAt{get;} = DateTime.Now;
}
