using Dapper;
using Microsoft.AspNetCore.Mvc;
using Notes.ContextDB;
using Notes.Interface;
using Notes.Model;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Notes.Service
{

    public class NotesService:INotes
    {
        //private readonly IHttpClientFactory _client;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration config;
        private readonly DBContext context;

        public NotesService(IConfiguration config, DBContext context, HttpClient client)
        {
            this._httpClient= client;
            this.config = config;
            this.context = context;
        }

        
        public async Task<List<NoteReadModel>> GetAllNotes(int UserId)
        {
            var query = "select NoteId,Title,[Description],Colour,CreatedBy from Note_Details where CreatedBy=@userid";

            using(var connect=context.CreateConnection())
            {
                var res = await connect.QueryAsync<NoteReadModel>(query, new {  UserId = UserId });
                return res.ToList();
                //return new NoteReadModel { }
            }
        }

        public async Task<int> CreateNote(int UserId, NoteCreateModel Inputs)
        {
            var query = "insert into Note_Details(title,[Description],Colour,IsArchived,IsDeleted,CreatedBy) values(@title,@description,@colour, 0,0,@UserId);";

            using(var connect= context.CreateConnection())
            {
                var res = await connect.ExecuteAsync(query, new { title = Inputs.Title, description = Inputs.Description, colour = Inputs.Colour, UserId = UserId });
                return res;
            }
        }

        public string CheckInp(string newstr, string oldstr)
        {
            if (newstr == "")
            {
                return oldstr;
            }
            return newstr;
        }

        public async Task<int> UpdateNote(int UserId, NoteUpdateModel Inputs)
        {
            var query1 = "select title,[description],colour from Note_Details where noteid=@noteid and CreatedBy=@Createdby";

            var parameters = new DynamicParameters();

            var query2 = "update Note_Details set title=@title, [description]=@description, colour=@colour where noteid=@noteid and createdby=@Createdby;";
            string PrevTitle, PrevDesc, PrevColour;

            using (var connect = context.CreateConnection())
            {
                var Query1res = await connect.QueryAsync<NoteCreateModel>(query1, new { noteid = Inputs.NoteId, Createdby = UserId });
                if (Query1res != null)
                {

                    //Storing Previous Values
                    PrevTitle = Query1res.ToList()[0].Title;
                    PrevDesc = Query1res.ToList()[0].Description;
                    PrevColour = Query1res.ToList()[0].Colour;

                    //Update Operation
                    parameters.Add("title", CheckInp(Inputs.Title, PrevTitle), DbType.String);
                    parameters.Add("description", CheckInp(Inputs.Description, PrevDesc), DbType.String);
                    parameters.Add("colour", CheckInp(Inputs.Colour, PrevColour), DbType.String);
                    parameters.Add("noteid", Inputs.NoteId, DbType.Int32);
                    parameters.Add("createdby", UserId, DbType.Int32);

                    //Execute Update
                    var Query2res = await connect.ExecuteAsync(query2, parameters);
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            return 0;
        }

        public async Task<int> DeleteNote(int UserId, int NoteId)
        {
            var query = "delete from Note_Details where CreatedBy=@userid and noteid=@noteid";

            using (var connect = context.CreateConnection())
            {
                var res = await connect.ExecuteAsync(query, new { userid = UserId, noteid = NoteId });
                return res;
            }
        }

        public async Task<UserModel> GetUserDetails(int UserId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/User?UserId={UserId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user=JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return user;
            }
            return null;
        }
    }
}
