using DomainOrEntities.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class TodoContext : DbContext
{
        public DbSet<Todo> Todos { get; set; } // this represents the Todo table in the database

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //C:\Users\grzes\OneDrive - ViaUC\DNP1\Tutorials\BlazorTodoAppTG\EfcData\Todo.db
                optionsBuilder.UseSqlite("Data Source = ../EfcData/Todo.db");
        }
        
        /*
         *  Note
            The above method is a simple approach, however we have now hardcoded the database info,
            and it may not be easy to modify. Usually the connection info will go into a settings file,
            and the program will read from that. It is left to the reader to google how to do that, if they're interested.
            
            Other database providers
            If you wanted to use a different DBMS, e.g. Postgres, you would add a NuGet package for a Postgres driver.
            That would then include a method UsePostgres(...), in which you would provide connection arguments.
         */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // if the Todo class didn't annotate Id as a Primary Key, then then line below would do the same job
            modelBuilder.Entity<Todo>().HasKey(todo => todo.Id);
            // below an example of a composite key 
            // modelBuilder.Entity<Todo>().HasKey(todo => new {todo.Id, todo.Title});
        }

        public void Seed()
        {
            if (Todos.Any()) return;

            Todo[] ts =
            {
                new Todo(1, "Dishes"),
                new Todo(1, "Walk the dog"),
                new Todo(2, "Do DNP homework"),
                new Todo(3, "Eat breakfast"),
                new Todo(4, "Mow lawn")
            };
            Todos.AddRange(ts);
            SaveChanges();
        }
}