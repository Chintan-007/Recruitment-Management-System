using RecruitmentManagement.DTOs.DocumentTypes;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class DocTypeMapper
{
    public static GetDocTypeDto ModelToGetDocTypeDto(this DocumentType documentTypeModel){
        return new GetDocTypeDto{
            id = documentTypeModel.id,
            documentType = documentTypeModel.documentType,
            createdAt = documentTypeModel.createdAt
        };
    }

    public static DocumentType DtoToDocTypeModel(this NewDocTypeDto newDocTypeDto){
        return new DocumentType{
            documentType = newDocTypeDto.documentType
        };
    }
}