using Microsoft.EntityFrameworkCore;
using SistemaTarefa.Data;
using SistemaTarefa.Models;
using SistemaTarefa.Repositories.Interfaces;

namespace SistemaTarefa.Repositories
{
    public class TaskRepositorie : ITaskRepositorie
    {
        private readonly SystemTaskDBContext _dbContext;

        public TaskRepositorie(SystemTaskDBContext systemTaskDBContext)
        {
            _dbContext = systemTaskDBContext;
        }

        public async Task<TaskModel> GetTaskById(int id)
        {
            // Corrigido: Busca na tabela Users, não Tasks
            return await _dbContext.Tasks
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TaskModel>> SearchAllTasks()
        {
            
            return await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<TaskModel> Add(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel taskById = await GetTaskById(id);

            // Corrigido: Verifica se é NULO para lançar exceção
            if (taskById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado.");
            }

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TaskModel> Update(TaskModel task, int id)
        {
            TaskModel taskById = await GetTaskById(id);

            if (taskById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado.");
            }

            // Corrigido: Atribui os valores do parâmetro 'user' ao 'userById'
            taskById.Name = task.Name;
            taskById.Email = task.Email;

            _dbContext.Tasks.Update(taskById);
            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public Task<TaskModel> SearchById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
