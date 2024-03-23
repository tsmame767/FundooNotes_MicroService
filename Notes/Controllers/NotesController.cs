using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Interface;
using Notes.Model;
using System.IdentityModel.Tokens.Jwt;

namespace Notes.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class NotesController : ControllerBase
    {
        public readonly INotes Service;

        public NotesController(INotes service)
        {
            this.Service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllNotes()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid);
            int UserId = int.Parse(userIdClaim.Value);
            var res = await this.Service.GetAllNotes(UserId);
            if(res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpGet("getbyid")]
        [Authorize]
        public async Task<UserModel> GetUserDetails()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid);
            int UserId = int.Parse(userIdClaim.Value);

            var res = await this.Service.GetUserDetails(UserId);
            if (res != null)
            {
                return new UserModel { Id = UserId, Email = res.Email, first_Name = res.first_Name, last_Name = res.last_Name };
            }
            return new UserModel { };
        }

        [HttpPost]
        [Authorize]
        public async Task<Response> CreateNote(NoteCreateModel Inputs)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid);
            int UserId = int.Parse(userIdClaim.Value);
            var res = await this.Service.CreateNote(UserId,Inputs);
            if (res != 0)
            {
                return new Response { IsSuccess = true, Message = "Created Note", Status = 201 };
            }
            return new Response { IsSuccess = false, Message = "Not Created Note", Status = 403 };
        }

        [HttpPut]
        [Authorize]
        public async Task<Response> UpdateNote(NoteUpdateModel Inputs)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid);
            int UserId = int.Parse(userIdClaim.Value);
            var res = await this.Service.UpdateNote(UserId, Inputs);
            if (res != 0)
            {
                return new Response { IsSuccess = true, Message = "Updated Note", Status = 200 };
            }
            return new Response { IsSuccess = false, Message = "Not Updated Note", Status = 403 };
        }

        [HttpDelete]
        [Authorize]
        public async Task<Response> DeleteNote(int NoteId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid);
            int UserId = int.Parse(userIdClaim.Value);
            var res = await this.Service.DeleteNote(UserId, NoteId);
            if (res != 0)
            {
                return new Response { IsSuccess = true, Message = "Deleted Note", Status = 204 };
            }
            return new Response { IsSuccess = false, Message = "Not Deleted Note", Status = 403 };
        }


    }
}
