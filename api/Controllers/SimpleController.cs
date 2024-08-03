using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/hello")]
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
        Message msg = _service.GetMessages().First();
        return Ok(msg);
    }
}