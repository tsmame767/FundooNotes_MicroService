using Collaborator.Context;
using Collaborator.Interface;
using Collaborator.Models;
using Dapper;

namespace Collaborator.Service
{
    public class CollabService:ICollab
    {
        private readonly ContextDB context;
        public CollabService(ContextDB _context) 
        { 
            context= _context;
        }
        public async Task<int> AddCollab(int NoteId, string Email,int UserId)
        {
            var res = 0;
            var query="insert into Collaborator_Details (collabemail,noteid,userid) values(@collabemail,@noteid,@collabid)";

            using (var connect = context.CreateConnection())
            {
                res = await connect.ExecuteAsync(query,new { collabemail = Email, noteid= NoteId, collabid=UserId });
                
            }
            return res;
        }

        public async Task<CollabResponse> GetCollab(int NoteId)
        {
            CollabResponse res = null;
            var query = "select * from Collaborator_Details where noteid=@noteid";

            using (var connect = context.CreateConnection())
            {
                res = await connect.QueryFirstOrDefaultAsync<CollabResponse>(query, new { noteid = NoteId });
            }
            return res;
        }

        public async Task<int> RemoveCollab(int NoteId, string Email)
        {
            var res = 0;
            var query = "delete from Collaborator_Details where collabemail='tsmane789@gmail.com' and noteid=1;";



            using (var connect = context.CreateConnection())
            {
                res =await connect.ExecuteAsync(query,new { NoteId = NoteId, collabEmail=Email});
            }
            return res;
        }
    }
}
