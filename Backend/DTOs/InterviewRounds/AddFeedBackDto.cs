using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs;

public class AddFeedBackDto
{   
    [Required]
    public string feedback{get;set;} = string.Empty;

    [Required]
    public int rating{get;set;}


}