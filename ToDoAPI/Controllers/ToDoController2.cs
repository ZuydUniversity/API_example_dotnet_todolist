using Microsoft.AspNetCore.Mvc;
using ToDoListModel;
using ToDoListModel.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController2 : ControllerBase
    {
        // GET: api/<ToDoController2>
        [HttpGet]
        public IEnumerable<ToDoTask> Get()
        {
            return ToDoTask.ReadAll();
        }

        // GET api/<ToDoController2>/5
        [HttpGet("{id}")]
        public ToDoTask Get(int id)
        {
            return ToDoTask.Read(id);
        }

        // POST api/<ToDoController2>
        [HttpPost]
        public void Post([FromBody] string description)
        {
            ToDoTask newTask = new ToDoTask(description);
            newTask.Create();
        }

        // PUT api/<ToDoController2>/5
        [HttpPut("{id}")]
        public void Put(int id)
        {
            ToDoTask finishedTask = ToDoTask.Read(id);
            finishedTask.FinishTask();
        }

        // DELETE api/<ToDoController2>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ToDoTask deleteTask = ToDoTask.Read(id);
            deleteTask.Delete();
        }
    }
}
