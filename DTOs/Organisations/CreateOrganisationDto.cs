using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Organisations;

public class CreateOrganisationDto{

    [Required]
    [StringLength(100),MinLength(2)]
    public string firstName{get; set;} = string.Empty;

    [Required]
    [StringLength(100),MinLength(2)]
    public string lastName{get;set;} = string.Empty;

    [Required]
    [StringLength(100),MinLength(2)]
    public string userName{get; set;} = string.Empty;

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

    [Required]
    public int organisationTypeId{get;set;}


}