
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagement.Models;

public class Employee : Users{

    public string? organisationId{get;set;}

    public Organisation? organisation{get; set;}
    
    public List<CandidateDocs> candidateDocs{get;set;} = new List<CandidateDocs>();
    public List<RoundHandler> roundHandlers{get; set;} = new List<RoundHandler>();
    
}