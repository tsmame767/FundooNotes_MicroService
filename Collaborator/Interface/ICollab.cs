using Collaborator.Models;

namespace Collaborator.Interface
{
    public interface ICollab
    {
        Task<CollabResponse> GetCollab(int NoteId);
        Task<int> AddCollab(int NoteId, string Email,int UserId);
        Task<int> RemoveCollab(int NoteId, string Email);
    }
}



