using SistemaTarefa.Models;

public class UserModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public virtual ICollection<TaskModel> Tasks { get; set; }

    public UserModel()
    {
        Tasks = new List<TaskModel>();
    }

    public static implicit operator UserModel(TaskModel v)
    {
        throw new NotImplementedException();
    }
}
