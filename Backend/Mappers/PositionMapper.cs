using Microsoft.AspNetCore.Http.Connections;
using RecruitmentManagement.DTOs.Positions;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class PositionMapper
{
    public static GetPositionDto ModelToGetPositionDto(this Position positionModel){
        return new GetPositionDto{
            id = positionModel.id,
            position = positionModel.position
        };
    }

    public static Position DtoToPositionModel(this NewPositionDto positionDto){
        return new Position{
            position = positionDto.position
        };
    }
}