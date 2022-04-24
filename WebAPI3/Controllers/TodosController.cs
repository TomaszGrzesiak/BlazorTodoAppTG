using DomainOrEntities.ContractsOrInterfaces;
using DomainOrEntities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI3.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class TodosController : ControllerBase
{
    private ITodoHomeOrDaoOrRep todoHomeOrDaoOrRep;
    public TodosController(ITodoHomeOrDaoOrRep todoHomeOrDaoOrRep)
    {
        this.todoHomeOrDaoOrRep = todoHomeOrDaoOrRep;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Todo>>> GetALl()
    {
        try
        {
            ICollection<Todo> todos = await todoHomeOrDaoOrRep.GetAsync();
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
            Todo todo = await todoHomeOrDaoOrRep.GetById(id);
            return Ok(todo);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Microsoft.AspNetCore.Mvc.Route("/TodosPostBlabla/")] // without this line, it would work like there 'd be "/Todos"
    public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
    {
        try
        {
            Todo added = await todoHomeOrDaoOrRep.AddAsync(todo);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
       
    [HttpDelete]
    [Microsoft.AspNetCore.Mvc.Route("{id:int}")] // this works like "/Todos/{id}"
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