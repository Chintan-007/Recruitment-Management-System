using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagement.Models;

public class CandidateDocs
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{get;set;}

    public string candidateId{get;set;} = string.Empty;
    public Candidate? candidate{get;set;} 

    public int documentTypeId{get;set;}
    public DocumentType? documentType{get;set;}

    public string documentLink{get;set;} = string.Empty;

    public bool isVerified{get;set;} = false;

    public string? verifiedById{get;set;}
    public Employee? verifiedBy{get;set;}
}