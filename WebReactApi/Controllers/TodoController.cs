
using Microsoft.AspNetCore.Mvc;
using WebReactApi.Core.Entities;
using WebReactApi.Service.Todos;

namespace WebReactApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : Controller
    {
        private ITodoService _todoService;
     
        public TodoController(
           ITodoService todoService
          
           )
        {
            _todoService = todoService;
           
        }
        public IActionResult Index()
        {
            return View();
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("list")]
        public IActionResult GetListTodo(string? searchValue)
        {
            var todos = _todoService.GetAll();
            if (!string.IsNullOrEmpty(searchValue))
            {
                todos= todos.Where(x=>x.Title.Contains(searchValue)).ToList();
            }
            return Ok(todos);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("update")]
        public IActionResult UpdateTodo(Todo model)
        {
            var entity = _todoService.Get(model.Id);
            if (entity != null) {
                entity.Completed = model.Completed;
                entity.Title = model.Title;
                _todoService.Update(entity);
            }
            else
            {
                model.CreatedOn = DateTime.Now;
                model.ModifiedOn = DateTime.Now;
                _todoService.Add(model);
            }
            return Ok(_todoService.GetAll());
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("delete")]
        public IActionResult DeleteTodo(Todo model)
        {
            var entity = _todoService.Get(model.Id);
            if (entity != null)
            {
               
                _todoService.Delete(entity);
            }
            return Ok(_todoService.GetAll());
        }
    }
}
