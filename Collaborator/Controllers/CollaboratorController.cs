using Collaborator.Interface;
using Collaborator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Collaborator.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CollaboratorController : Controller
    {
        private readonly ICollab? Service;

        public CollaboratorController(ICollab? service)
        {
            this.Service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<CollabResponse> GetCollab(int NoteId)
        {
           
            var res = await this.Service.GetCollab(NoteId);
            if (res != null)
            {
                return new CollabResponse { CollabEmail=res.CollabEmail,CollabId=res.CollabId,NoteId=res.NoteId,UserId=res.UserId};
            }
            else
            {
                return new CollabResponse { CollabEmail = res.CollabEmail, CollabId = res.CollabId, NoteId = res.NoteId, UserId = res.UserId };
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<Response> AddCollab(int NoteId, string Email)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid);
            int UserId = int.Parse(userIdClaim.Value);
            var res = await this.Service.AddCollab(NoteId, Email,UserId);
            if (res != 0)
            {
                return new Response { IsSuccess = true, Message="Collaborator Added", Status=201 };
            }
            else
            {
                return new Response { IsSuccess = false, Message = "No Collaborator Added", Status = 204 };
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<Response> RemoveCollab(int NoteId, string Email)
        {
        
            var res = await this.Service.RemoveCollab(NoteId, Email);
            if (res != 0)
            {
                return new Response { IsSuccess = true, Message = "Collaborator Removed", Status = 204 };
            }
            else
            {
                return new Response { IsSuccess = false, Message = "No Collaborator Removed", Status = 403 };
            }
        }
    }
}
