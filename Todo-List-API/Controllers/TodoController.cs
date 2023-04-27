using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        #region Login Queries
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
        #endregion

        #region Category Queries
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

        [HttpPost("CategoryEdit")]
        public Response PostCategoryEdit(int categoryId, string name)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.Categories.Where(c => c.Id == categoryId && c.DeletedAt == null).Single().Name = name;
                    context.SaveChanges();
                    return new Response { Message = "Category edited successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while editing category" };
                }
            }
        }

        [HttpPost("CategoryDelete")]
        public Response PostCategoryDelete(int categoryId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.Categories.Where(c => c.Id == categoryId).Single().DeletedAt = DateTime.Now;
                    context.ListCategories.Where(l => l.CategoryId == categoryId).ToList().ForEach(l => { l.Category = null; l.CategoryId = null; });
                    context.SaveChanges();
                    return new Response { Message = "Category deleted successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while deleting category" };
                }
            }
        }
        #endregion

        #region List Queries
        [HttpPost("ListAdd")]
        public Response PostListAdd(int userId, string name, int categoryId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.Lists.Add(new List { Name = name, OwnerId = userId });
                    context.SaveChanges();
                    context.ListCategories.Add(new ListCategory { UserId = userId, ListId = context.Lists.Where(l => l.OwnerId == userId && l.Name == name).Single().Id, CategoryId = categoryId });
                    context.SaveChanges();
                    return new Response { Message = "List created successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while creating list" };
                }
            }
        }

        [HttpPost("ListEdit")]
        public Response PostListEdit(int userId, int listId, string name, int categoryId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.Lists.Where(l => l.Id == listId).Single().Name = name;
                    context.ListCategories.Where(l => l.ListId ==  listId && l.UserId == userId).Single().CategoryId = categoryId;
                    context.SaveChanges();
                    return new Response { Message = "List edited successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while editing list" };
                }
            }
        }

        [HttpPost("ListDelete")]
        public Response PostListDelete(int listId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.Lists.Where(l => l.Id == listId).Single().DeletedAt = DateTime.Now;
                    context.ListCategories.Where(l => l.ListId == listId).ExecuteDelete();
                    context.Tasks.Where(t => t.ListId == listId).ToList().ForEach(t => t.DeletedAt = DateTime.Now);
                    context.SaveChanges();
                    return new Response { Message = "List deleted successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while deleting list" };
                }
            }
        }

        [HttpPost("ListAddUser")]
        public Response PostListAddUser(string username, int listId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.ListCategories.Add(new ListCategory { UserId = context.Users.Where(u => u.Username == username).Single().Id, ListId = listId });
                    context.SaveChanges();
                    return new Response { Message = "User added to list successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while adding user to list" };
                }
            }
        }

        [HttpPost("ListDeleteUser")]
        public Response PostListDeleteUser(int userId, int listId)
        {
            using (var context = new TodoDbContext())
            {
                try
                {
                    context.ListCategories.Where(u => u.UserId == userId && u.ListId == listId).ExecuteDelete();
                    context.SaveChanges();
                    return new Response { Message = "User deleted from list successfully!" };
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new Response { Type = "ERROR", Message = "An error occured while deleting user from list" };
                }
            }
        }
        #endregion

        #region Task Queries

        #endregion
    }
}
