using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.Features.Stub;

/// <summary>
/// Тестовый контроллер для ауентификации
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StubController : ControllerBase
{


    /// <summary>
    /// Тестовый post запрос
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    // ReSharper disable once StringLiteralTypo решарпер хочет authStub
    [HttpPost("authstub")]

    public string Post()
    {
        return "hello";
    }

     
}