using System.Text.Json;
using DomainOrEntities.Models;

namespace FileData.DataAccess;

public class FileContext
{
    private string todoJsonFilePath = "todos.json";
    
    // the line below is only a bare field variable 
    private ICollection<Todo> todos;
    
    // the line below, on the contrary, is actually NOT method, but still acts, at least partly, like the one. 
    public ICollection<Todo> Todos
    {
        get
        {
            if (todos == null)
            {
                LoadData();
            }
            // in the tutorial it was just "todos", but Troels had this "!" in his file on Github
            return todos!;
        }
    }
    
    public FileContext()
    {
        if (!File.Exists(todoJsonFilePath))
        {
            Seed();
        }
    }
    
    private void Seed()
    {
        Todo[] ts = {
            new Todo(1, "Dishes") {
                Id = 1,
            },
            new Todo(1, "Walk the dog") {
                Id = 1,
            },
            new Todo(2, "Do DNP homework") {
                Id = 3,
            },
            new Todo(3, "Eat breakfast") {
                Id = 4,
            },
            new Todo(4, "Mow lawn") {
                Id = 5,
            },
        };
        todos = ts.ToList();
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        string TodosSerialized = JsonSerializer.Serialize(Todos);
        File.WriteAllText(todoJsonFilePath,TodosSerialized);
        todos = null;
    }
    
    private void LoadData()
    {
        string jsonFileContent = File.ReadAllText(todoJsonFilePath);
        todos = JsonSerializer.Deserialize<List<Todo>>(jsonFileContent);
    }
}