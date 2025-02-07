using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class DocumentType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get; set;}

    [Required]
    [StringLength(100, MinimumLength =2)]
    public String documentType{get; set;}

    public DateTime createdAt = DateTime.Now;
}