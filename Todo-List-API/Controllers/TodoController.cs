using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Todo_List_API.Context;
using Todo_List_API.Models;
using Todo_List_API.Models.Common;

namespace Todo_List_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Register")]
        public Response PostRegister(string username, string password)
        {
            using (var context = new TodoDbContext())
            {
                if (context.Users.Where(u => u.Username == username).Any())
                    return new Response { Type = "ERROR", Message = "Username already exists in database" };

                context.Users.Add(new Models.User { Username = username, Password = password });
                context.SaveChanges();

                return new Response();
            }
        }

        [HttpPost("Login")]
        public Response PostLogin(string username, string password)
        {
            using (var context = new TodoDbContext())
            {
                if (context.Users.Where(u => u.Username == username && u.Password == password).Any())
                    return new Response { Message = context.Users.Where(u => u.Username == username && u.Password == password).Single() };

                return new Response { Type = "ERROR", Message = "Login data not valid" };
            }
        }

        [HttpPost("CategoryAdd")]
        public Response PostCategoryAdd(string name, int userId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.Categories.Add(new Category { Name = name, OwnerId = userId, Owner = context.Users.Where(u => u.Id == userId).Single() });
                    context.SaveChanges();
                    return new Response { Message = "Category created successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while creating category" };
                }
            }
        }

        [HttpGet("CategoryList")]
        public Response GetCategoryList(int userId)
        {
            using (var context = new TodoDbContext())
            {
                if (context.Categories.Where(c => c.OwnerId == userId).Any())
                    return new Response { Message = context.Categories.Where(c => c.OwnerId == userId).ToArray() };

                return new Response { Type = "ERROR", Message = "No categories found for user" };
            }
        }
    }
}
