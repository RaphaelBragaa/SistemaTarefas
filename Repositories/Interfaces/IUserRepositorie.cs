using SistemaTarefa.Models;

namespace SistemaTarefa.Repositories.Interfaces
{
    public interface IUserRepositorie
    {
        Task<UserModel> GetUserById(int id);
        Task<List<UserModel>> SearchAllUsers();
        Task<UserModel> Add(UserModel user);
        Task<bool> Delete(int id);
        Task<UserModel> Update(UserModel user, int id);
        Task<UserModel> SearchById(int id);
        Task<UserModel> GetUserByEmail(string email);
    }

}
