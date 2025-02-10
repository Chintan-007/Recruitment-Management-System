using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class InterviewType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}

    [Required]
    [StringLength(100,MinimumLength=2)]
    public string interviewType{get; set;} = string.Empty;

    public DateTime createdAt{get;} = DateTime.Now;
}
