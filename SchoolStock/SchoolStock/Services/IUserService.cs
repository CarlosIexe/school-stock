using SchoolStock.Data.DTO.User;

namespace SchoolStock.Services;

public interface IUserService
{
    Task<List<UserResponse>> FindAll();

    Task<UserResponse?> FindById(string id);

    Task<UserResponse> Create(CreateUserRequest request);

    Task<bool> UpdateRole(
        string id,
        UpdateUserRoleRequest request
    );

    Task<bool> Delete(string id);
}