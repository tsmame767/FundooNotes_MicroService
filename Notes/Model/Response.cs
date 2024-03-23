namespace Notes.Model
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class NoteModel
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public string IsArchived { get; set; }
        public string IsDeleted { get; set; }
        public int CreatedBy { get; set; }
    }

    public class NoteCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
    }

    public class NoteUpdateModel
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
    }

    public class GetUserDataModel
    {
        
    }

    public class NoteReadModel
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public UserModel UserDetails { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public string Email { get; set; }
    }
}