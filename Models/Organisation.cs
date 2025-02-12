using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentManagement.Models;

public class Organisation : Users
{
 
    public string AddressLine1{get; set;} = string.Empty;

    public string AddressLine2{get; set;} = string.Empty;

    public string about{get; set;} = string.Empty;

    public int? organisationTypeId{get;set;}
    public OrganisationType? organisationType{get; set;}
    public List<Employee> employees{get;set;} = new List<Employee>();
    public List<JobOpening> jobOpenings{get;set;} = new List<JobOpening>();

}