using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace backend.Controllers;

[Route("hello")]
[ApiController]
public class SimpleController : ControllerBase
{
    private SimpleService _service;
    public SimpleController(SimpleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetMessage()
    {
        List<Message> messages = _service.GetMessages();
        return Ok(messages);
    }
}