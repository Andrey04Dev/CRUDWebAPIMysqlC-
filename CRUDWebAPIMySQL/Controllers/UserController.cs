using CRUDWebAPIMySQL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CRUDWebAPIMySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbUsersContext dbContext;

        public UserController(DbUsersContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var List = await dbContext.Users.Select(
                    s => new UserDTO
                    {
                        Id = s.Id,
                        Firstname = s.Firstname,
                        Lastname = s.Lastname,
                        Username = s.Username,
                        Password = s.Password,
                        EnrollmentDate = s.EnrollmentDate
                    }
                ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int Id)
        {
            UserDTO User = await dbContext.Users.Select(
                    s => new UserDTO
                    {
                        Id = s.Id,
                        Firstname = s.Firstname,
                        Lastname = s.Lastname,
                        Username = s.Username,
                        Password = s.Password,
                        EnrollmentDate = s.EnrollmentDate
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }
        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser([FromForm] UserDTO User)
        {
            var entity = new User()
            {
                Firstname = User.Firstname,
                Lastname = User.Lastname,
                Username = User.Username,
                Password = User.Password,
                EnrollmentDate = User.EnrollmentDate
            };

            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<HttpStatusCode> UpdateUser([FromForm] UserDTO User)
        {
            var entity = await dbContext.Users.FirstOrDefaultAsync(s => s.Id == User.Id);

            entity!.Firstname = User.Firstname;
            entity.Lastname = User.Lastname;
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.EnrollmentDate = User.EnrollmentDate;

            await dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new User()
            {
                Id = Id
            };
            dbContext.Users.Attach(entity);
            dbContext.Users.Remove(entity);
            await dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
