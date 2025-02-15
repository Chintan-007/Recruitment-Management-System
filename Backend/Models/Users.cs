
using Microsoft.AspNetCore.Identity;

namespace RecruitmentManagement.Models;

public class Users:IdentityUser{
  
    public string firstName{get;set;} = string.Empty;
    public string lastName{get;set;} = string.Empty;
    public int age{get;set;}
    public int? positionId{get; set;}
    public Position position{get; set;}
    public int yearsOfExperience{get; set;}
     public bool isActive{get; set;} = true;
    public string disableReason{get; set;} = string.Empty;

}