using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Organisation;

public class CreateOrganisationDto{

    [Required]
    [StringLength(100),MinLength(2)]
    public string organisationName{get; set;} = string.Empty;

    [Required]
    [StringLength(70),MinLength(5)]
    public string email{get;set;} = string.Empty;

    [Required]
    [StringLength(15),MinLength(10)]
    public string contact{get;set;} = string.Empty;

    [Required]
    public string AddressLine1{get; set;} = string.Empty;

    public string AddressLine2{get; set;} = string.Empty;

    [Required]
    public string about{get; set;} = string.Empty;

    [Required]
    public string password{get; set;} = string.Empty;

    public int? organisationTypeId{get;set;}
    public OrganisationType? organisationType{get; set;}

    public List<Employee> employees{get;set;} = new List<Employee>();
}