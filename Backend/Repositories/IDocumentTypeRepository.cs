
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IDocumentTypeRepository
{
    Task<IEnumerable<DocumentType>> GetDocumentTypes();
    Task<IEnumerable<OrganisationType>> GetOrganisationTypes();

    Task<DocumentType> GetDocumentTypeById(int id);

    Task<DocumentType> AddDocumentType(DocumentType documentType);

    Task<DocumentType> UpdateDocumentTypeById(int id, DocumentType documentType);

    Task<DocumentType> DeleteDocumentTypeById(int id);

    Task<DocumentType> GetDocumentTypeByDocumentType(string type);
}