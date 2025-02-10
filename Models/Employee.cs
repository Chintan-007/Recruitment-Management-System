
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.Models;

public class Employee : Users{

    public int? organisationId{get;set;}

    public Organisation? organisation{get; set;}
    
    public int? positionId{get; set;}
    public Position position{get; set;}

    [Required]
    public int yearsOfExperience{get; set;}
}