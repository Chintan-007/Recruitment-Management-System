using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.CandidateDocs;

public class DocumentData{

    [Required]
    public int documentTypeId{get;set;}
    
    [Required]
    public string documentLink{get;set;} = string.Empty;
}