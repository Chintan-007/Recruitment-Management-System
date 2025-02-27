using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class OrganisationType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}

    [Required]
    [StringLength(100,MinimumLength=2)]
    public string organisationType{get; set;} = string.Empty;

    public DateTime createdAt{get;} = DateTime.Now;

    public List<Organisation> organisations{get;set;} = new List<Organisation>();
}
