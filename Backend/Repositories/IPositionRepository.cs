using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IPositionRepository
{
     //Create
    Task<Position> AddPosition(Position position);

    //Read
    Task<IEnumerable<Position>> GetPositions();
    Task<Position> GetPositionById(int id);
    Task<Position> GetPositionByName(string positionName);

    //Update
    Task<Position> UpdatePositionById(int id, Position position);

    //Delete
    Task<Position> DeletePositionById(int id);
}