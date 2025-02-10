using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class Organisation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}

    [Required]
    [StringLength(100),MinLength(2)]
    public string organisationName{get; set;}

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

    public bool isActive{get; set;} = true;

    public string disableReason{get; set;} = string.Empty;
    public DateTime createdAt{get;} = DateTime.Now;


    public int? organisationTypeId{get;set;}
    public OrganisationType? organisationType{get; set;}

    public List<Employee> employees{get;set;} = new List<Employee>();

}