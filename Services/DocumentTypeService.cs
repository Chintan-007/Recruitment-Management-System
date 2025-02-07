
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class DocumentTypeService : IDocumentTypeRepository
{
    private readonly ApplicationContext applicationContext;

    public DocumentTypeService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }

    //Create
    public async Task<DocumentType> AddDocumentType(DocumentType documentType)
    {
        var result = await applicationContext.DocumentTypes.AddAsync(documentType);
        await applicationContext.SaveChangesAsync();
        return result.Entity;
    }


    //Read
    public async Task<IEnumerable<DocumentType>> GetDocumentTypes()
    {
        return await applicationContext.DocumentTypes.ToListAsync();
    }
    public async Task<DocumentType> GetDocumentTypeById(int id)
    {
        return await applicationContext.DocumentTypes.FindAsync(id);
    }

     public async Task<DocumentType> GetDocumentTypeByDocumentType(string documentType)
    {
        return await applicationContext.DocumentTypes
                    .FirstOrDefaultAsync(dt => dt.documentType.Equals(documentType));
    }
    

    //Update
    public async Task<DocumentType> UpdateDocumentTypeById(int id, DocumentType documentType)
    {
        var result = await GetDocumentTypeById(id);
        if(result != null){
            result.documentType = documentType.documentType;
            await applicationContext.SaveChangesAsync();
            return result;
        }

        return null;
    }


    //Delete
    public async Task<DocumentType> DeleteDocumentTypeById(int id)
    {
        var result = await GetDocumentTypeById(id);
        if(result != null){
            applicationContext.Remove(result);
            await applicationContext.SaveChangesAsync();
            return result;
        }

        return null;
    }
}