

namespace RecruitmentManagement.DTOs.CandidateDocs;

public class AddedCandidateDocsDto
{
    public string candidateId{get;set;} = string.Empty;

   public string firstName{get; set;} = string.Empty;

    public string lastName{get;set;} = string.Empty;

    public string userName{get; set;} = string.Empty;
    
    public int age{get;set;}
    
    // public string email{get;set;} = string.Empty;

    // public string phoneNumber{get;set;} = string.Empty;

    // public string  position{get; set;} = string.Empty;
    
    // public int yearsOfExperience{get; set;}

    public string resumeLink{get; set;} = string.Empty;
    
    // public string organisationName{get;set;} = string.Empty;

    public IEnumerable<DisplayDocs> documentDetails{get;set;}=[];

}