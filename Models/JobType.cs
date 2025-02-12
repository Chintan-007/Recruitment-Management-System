using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class JobType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}

    [Required]
    [StringLength(100,MinimumLength=2)]
    public string type{get; set;} = string.Empty;

    public List<JobOpening> jobOpenings{get;set;} = new List<JobOpening>();

    public DateTime createdAt{get;} = DateTime.Now;
}
