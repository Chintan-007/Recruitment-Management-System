using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.DTOs.CandidateDocs;

public class NewCandidateDocsDto
{
    [Required]
    public string candidateId{get;set;} = string.Empty;

    [Required]
    public List<DocumentData> documentDatas  = new List<DocumentData>();

}