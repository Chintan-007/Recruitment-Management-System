using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class UserType{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    public long Id{get; set;}

    [Required]
    public String TypeOfUser{get; set;}

}