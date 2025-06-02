using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTarefa.Models;
using SistemaTarefa.Repositories.Interfaces;

namespace SistemaTarefa.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepositorie _taskRepositorie;
        private object _taskRepositorieService;

        public TaskController(ITaskRepositorie taskRepositorie)
        {
            _taskRepositorie = taskRepositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> SearchAllTasks()
        {
            List<TaskModel> tasks = await _taskRepositorie.SearchAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> SearchById(int id)
        {
            TaskModel task = await _taskRepositorie.SearchById(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Add([FromBody] TaskModel taskModel)
        {
            TaskModel task = await _taskRepositorie.Add(taskModel);

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Up([FromBody] TaskModel taskModel, int id)
        {
            TaskModel task = await _taskRepositorie.Update(taskModel, id);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> Delete(int id)
        {
            bool delete = await _taskRepositorie.Delete(id);

            return Ok(delete);
        }
    }
}