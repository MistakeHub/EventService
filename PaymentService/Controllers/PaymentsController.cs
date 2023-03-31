using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]

public class PaymentsController : ControllerBase
{
    private readonly BasePaymentService _basePaymentService;

    public PaymentsController(BasePaymentService basePaymentService)
    {
        _basePaymentService = basePaymentService;
    }


    // PUT api/<PaymentsController>/5
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute Решарпер предлагает перенести id в общий маршрут
    [HttpPut("{id}/confirmation")]
    public ScResult<bool> Confirmation(string id)
    {
        var result = _basePaymentService.ChangeState(Guid.Parse(id), 2);
        return new ScResult<bool>(result);
    }

    // POST api/<PaymentsController>
    [HttpPost]
    public ScResult<Guid> Creation(string description)
    {
        var result = _basePaymentService.CreatePayment(description);
        return new ScResult<Guid>(result);
    }

    // PUT api/<PaymentsController>/5
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute Решарпер предлагает перенести id в общий маршрут
    [HttpPut("{id}/cancellation")]
    public ScResult<bool> Canclellation(string id)
    {
        var result=  _basePaymentService.ChangeState(Guid.Parse(id), 1);
        return new ScResult<bool>(result);
    }

}