using Microsoft.EntityFrameworkCore;
using SistemaTarefa.Data;
using SistemaTarefa.Models;
using SistemaTarefa.Repositories.Interfaces;

namespace SistemaTarefa.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly SystemTaskDBContext _dbContext;

        public UserRepositorie(SystemTaskDBContext systemTaskDBContext)
        {
            _dbContext = systemTaskDBContext;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            // Corrigido: Busca na tabela Users, não Tasks
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserModel>> SearchAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> Add(UserModel user)
        {
            bool emailExists = await _dbContext.Users.AnyAsync(x => x.Email == user.Email);

            if (emailExists)
            {
                throw new Exception("Este e-mail já está cadastrado!");
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }


        public async Task<bool> Delete(int id)
        {
            UserModel userById = await GetUserById(id);

            // Corrigido: Verifica se é NULO para lançar exceção
            if (userById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado.");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel userById = await GetUserById(id);

            if (userById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado.");
            }

            // Corrigido: Atribui os valores do parâmetro 'user' ao 'userById'
            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<UserModel> SearchById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
