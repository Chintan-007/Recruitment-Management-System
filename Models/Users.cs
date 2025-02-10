
using Microsoft.AspNetCore.Identity;

namespace RecruitmentManagement.Models;

public class Users:IdentityUser{

    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public int id{get;set;}
  
    public int age{get;set;}

    public Boolean isActive{get;set;} = true;

    // public int? usersTypeId{get;set;}

    // public UserType? userType{get;set;}

}