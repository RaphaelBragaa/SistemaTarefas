using SistemaTarefa.Models;

namespace SistemaTarefa.Repositories.Interfaces
{
    public interface ITaskRepositorie
    {
        Task<TaskModel> GetTaskById(int id);
        Task<List<TaskModel>> SearchAllTasks();
        Task<TaskModel> Add(TaskModel task);
        Task<bool> Delete(int id);
        Task<TaskModel> Update(TaskModel task, int id);
        Task<TaskModel> SearchById(int id);
    }

}
