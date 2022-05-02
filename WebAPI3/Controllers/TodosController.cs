using DomainOrEntities.ContractsOrInterfaces;
using DomainOrEntities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI3.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoHomeOrDaoOrRep todoHomeOrDaoOrRep;

    public TodosController(ITodoHomeOrDaoOrRep todoHomeOrDaoOrRep)
    {
        this.todoHomeOrDaoOrRep = todoHomeOrDaoOrRep;
    }

    // [HttpGet]
    // public async Task<ActionResult<ICollection<Todo>>> GetALl()
    // {
    //     Console.WriteLine($"UserId: , isCompleted: ");
    //     try
    //     {
    //         ICollection<Todo> todos = await todoHomeOrDaoOrRep.GetAsync();
    //         return Ok(todos);
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(500, e.Message);
    //     }
    // }

    // TODO: finish rest controller, so that the Blazor app can get filtered results when querying after todos with parameters (userId and isCompleted)
    [HttpGet]
    //[Microsoft.AspNetCore.Mvc.Route("{userId:int, isCompleted:boolean}")]
    public async Task<ActionResult<Todo>> GetAsync([FromQuery] int? userId, [FromQuery] bool? isCompleted)
    {
        Console.WriteLine($"RECEIVED PARAMETERS: UserId: {userId}, isCompleted: {isCompleted}");
        try
        {
            var todos = await todoHomeOrDaoOrRep.GetAsync(userId, isCompleted);
            foreach (var todo in todos) Console.WriteLine(todo.ToString());
            return Ok(todos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodoById(int id)
    {
        try
        {
            var todo = await todoHomeOrDaoOrRep.GetByIdAsync(id);
            return Ok(todo);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Route("/TodosPostBlabla/")] // without this line, it would work like there 'd be "/Todos"
    public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
    {
        try
        {
            var added = await todoHomeOrDaoOrRep.AddAsync(todo);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [HttpDelete]
    [Route("{id:int}")] // this works like "/Todos/{id}"
    public async Task<ActionResult> DeleteTodoById(int id)
    {
        try
        {
            await todoHomeOrDaoOrRep.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] Todo todo)
    {
        try
        {
            await todoHomeOrDaoOrRep.UpdateAsync(todo);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}