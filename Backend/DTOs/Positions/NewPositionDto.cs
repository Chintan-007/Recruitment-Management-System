using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.Positions;

public class NewPositionDto
{   
    [Required]
    [StringLength(100),MinLength(2)]
    public string position{get;set;} = string.Empty;
}