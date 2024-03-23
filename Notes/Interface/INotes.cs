using Notes.Model;

namespace Notes.Interface
{
    public interface INotes
    {
        Task<List<NoteReadModel>> GetAllNotes(int UserId);
        Task<int> CreateNote(int UserId, NoteCreateModel Inputs);
        Task<int> UpdateNote(int UserId, NoteUpdateModel Inputs);
        Task<int> DeleteNote(int UserId, int NoteId);
        Task<UserModel> GetUserDetails(int UserId);
    }
}
