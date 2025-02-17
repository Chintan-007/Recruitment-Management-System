namespace RecruitmentManagement.DTOs.DocumentTypes;

public class GetDocTypeDto
{
     public int id{get; set;}

    public string documentType{get; set;}  = string.Empty;    
    public DateTime createdAt{get;set;}
}