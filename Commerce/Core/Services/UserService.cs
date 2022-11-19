using AutoMapper;
using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Exceptions;
using Commerce.Core.Models;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task DeleteUser(Guid id)
    {
        await GetUserById(id);
        await userRepository.Delete(id);
    }

    public async Task<List<User>> GetAllUsers()
    {

        return await userRepository.GetAll();
    }

    public async Task<User> GetUserById(Guid id)
    {
        var user = await userRepository.GetById(id);
        if (user is null)
        {
            throw new AppException("El usuario no existe", HttpStatusCode.NotFound);
        }
        return user;
    }

    public async Task InsertUser(UserRequestDTO userDTO)
    {
        User user = new()
        {
            LastName = userDTO.LastName,
            Adress = userDTO.Adress,
            Name = userDTO.Name
        };

        await userRepository.Insert(user);
    }

    public async Task UpdateUser(User user)
    {
        await GetUserById(user.UserId);
        await userRepository.Update(user);
    }
}
