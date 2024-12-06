using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // /GET api/tasks
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = AppDBContext._taskList;
            if (!tasks.Any())
            {
                return NoContent();
            }
            return Ok(tasks);
        }

        // /GET api/tasks/{id}
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = AppDBContext._taskList.Find(task => task.Id == id);
            if (task == null)
            {
                return NotFound("Task was not found");
            }
            return Ok(task);
        }

        // /Post api/tasks
        [HttpPost]
        public IActionResult PostTask([FromBody] MyTask newTask)
        {
            newTask.Id = AppDBContext._taskList.Count + 1;
            newTask.IsCompleted = false;
            newTask.CreatedAt = DateTime.Now;
            newTask.CompletedAt = null;

            AppDBContext._taskList.Add(newTask);
            return CreatedAtAction(nameof(GetTask), newTask.Id, newTask);
        }

        // /PUT api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult PutTask([FromBody] MyTask updatedTask, int id)
        {
            var taskOld = AppDBContext._taskList.Find(task => task.Id == id);
            if (taskOld == null)
            {
                return NotFound("Task not found");
            }

            taskOld.Title = updatedTask.Title;
            taskOld.Description = updatedTask.Description;
            if (updatedTask.IsCompleted && taskOld.CompletedAt == null)
            {
                taskOld.CompletedAt = DateTime.Now;
            }
            taskOld.IsCompleted = updatedTask.IsCompleted;

            return Ok(taskOld);
        }

        // /DELETE api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = AppDBContext._taskList.Find(task => task.Id == id);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            AppDBContext._taskList.Remove(task);
            //return Ok("Task deleted successfully");
            return Ok(task);
        }
    }
}
