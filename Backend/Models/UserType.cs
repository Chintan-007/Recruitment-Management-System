// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace RecruitmentManagement.Models;

// public class UserType{

//     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
//     public long Id{get; set;}

//     [Required]
//     [StringLength(100,MinimumLength = 2)]
//     public String TypeOfUser{get; set;} = string.Empty;

//     public DateTime createdAt{get;} = DateTime.Now;

//     public List<Users> users{get;set;} = new List<Users>();
// }