using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class PositionService : IPositionRepository{
    private readonly ApplicationContext applicationContext;
    public PositionService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }


    //Create
    public async Task<Position> AddPosition(Position position)
    {
        var result =  await applicationContext.Positions.AddAsync(position);
        await applicationContext.SaveChangesAsync();
        return result.Entity;
    }



    //Read
    public async Task<IEnumerable<Position>> GetPositions()
    {
        return await applicationContext.Positions.ToListAsync();
    }
    public async Task<Position> GetPositionById(int id)
    {
        return await applicationContext.Positions.FindAsync(id);
    }

    public async Task<Position> GetPositionByName(string positionName)
    {
        return await applicationContext.Positions.FirstOrDefaultAsync(position => position.position.ToLower().Equals(positionName.ToLower()));
    }




    //Update
    public async Task<Position> UpdatePositionById(int id, Position position)
    {
        var result = await GetPositionById(id);
        if(result != null){
            result.position = position.position;
            await applicationContext.SaveChangesAsync();
            return result;
        }
        return null;
    }


    //Delete
    public async Task<Position> DeletePositionById(int id)
    {
        var result = await GetPositionById(id);
        if(result != null){
            applicationContext.Remove(result);
            await applicationContext.SaveChangesAsync();
            return result;
        }
        return null;
    }
}