using System.ComponentModel.DataAnnotations;

namespace DomainOrEntities.Models;

public class Todo
{
    [Key]
    public int Id { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than{1}")]
    public int OwnerId { get; set; }
    
    [Required, MaxLength(128)] // or: [Required] [MaxLength]
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
        // IsCompleted is "false" by default;
        // "Id" will be set automatically based on what's already in the persistent data
    }

    public Todo()
    {
    }

    public override string ToString()
    {
        return $"Todo id: {Id}, {Title}, OwnerId: {OwnerId}, IsCompleted: {IsCompleted}";
    }
}