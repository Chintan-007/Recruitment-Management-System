using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Organisations;

public class RegisteredOrganisationDto{


    public string firstName{get; set;} = string.Empty;

    public string lastName{get;set;} = string.Empty;

    public string userName{get; set;} = string.Empty;

    public string email{get;set;} = string.Empty;

    public string contact{get;set;} = string.Empty;

    public string AddressLine1{get; set;} = string.Empty;

    public string AddressLine2{get; set;} = string.Empty;

    public string about{get; set;} = string.Empty;

    public string token{get;set;} = string.Empty;

    public String? organisationType{get; set;}

    public List<Employee> employees{get;set;} = new List<Employee>();

}