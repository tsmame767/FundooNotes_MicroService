namespace Collaborator.Models
{
    public class Response
    {
        public int Status { get; set; }
        public string Message {  get; set; }
        public bool IsSuccess {  get; set; }
    }

    public class CollabResponse
    {
        public int CollabId { get; set; }
        public string CollabEmail { get; set; }
        public int UserId { get; set; }
        public int NoteId { get; set; }
    }
}
