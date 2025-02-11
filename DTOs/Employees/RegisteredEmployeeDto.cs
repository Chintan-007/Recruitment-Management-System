using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.Employees;

public class RegisteredEmployeeDto
{
    
    public string firstName{get; set;} = string.Empty;

    public string lastName{get;set;} = string.Empty;

    public string userName{get; set;} = string.Empty;

    public string email{get;set;} = string.Empty;

    public string phoneNumber{get;set;} = string.Empty;

   public string? organisationName{get;set;}

    public int? age{get;set;}

    public string? position{get; set;}
    
    public int yearsOfExperience{get; set;}

    public string token{get;set;} = string.Empty;
}