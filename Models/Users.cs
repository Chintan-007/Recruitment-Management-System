
using Microsoft.AspNetCore.Identity;

namespace RecruitmentManagement.Models;

public class Users:IdentityUser{
  
    public string firstName{get;set;}
    public string lastName{get;set;}
    public int age{get;set;}

    public bool isActive{get; set;} = true;
    public string disableReason{get; set;} = string.Empty;

}