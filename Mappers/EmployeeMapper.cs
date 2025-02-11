
using RecruitmentManagement.DTOs.Employees;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class EmployeeMapper
{
    public static RegisteredEmployeeDto EmployeeModelToDto(this Employee employeeModel, string empToken){
        return new RegisteredEmployeeDto{
            firstName = employeeModel.firstName,
            lastName = employeeModel.lastName,
            userName = employeeModel.UserName,
            email = employeeModel.Email,
            phoneNumber = employeeModel.PhoneNumber,
            age = employeeModel.age,
            token = empToken,
            organisationName = employeeModel.organisation.UserName,
            position = employeeModel.position.position,
            yearsOfExperience = employeeModel.yearsOfExperience
        };
    }

    public static Employee EmployeeDtoToModel(this CreateEmployeeDto employeeDto, Organisation organisation, Position position){
        return new Employee{
                firstName = employeeDto.firstName,
                lastName = employeeDto.lastName,
                UserName = employeeDto.userName,
                Email = employeeDto.email,
                PhoneNumber = employeeDto.phoneNumber,
                age = employeeDto.age,
                position = position,
                organisation = organisation,
                yearsOfExperience = employeeDto.yearsOfExperience
        };

    }
}