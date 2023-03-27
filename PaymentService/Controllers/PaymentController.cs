using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]

public class PaymentController : ControllerBase
{
    private readonly BasePaymentService _basePaymentService;

    public PaymentController(BasePaymentService basePaymentService)
    {
        _basePaymentService = basePaymentService;
    }


    // PUT api/<PaymentController>/5
    [HttpPut("confirmation/{id}")]
    public ScResult<bool> Confirmation(string id)
    {
        var result = _basePaymentService.ChangeState(Guid.Parse(id), 2);
        return new ScResult<bool>(result);
    }

    // POST api/<PaymentController>
    [HttpPost]
    public ScResult<Guid> Creation(string description)
    {
        var result = _basePaymentService.CreatePayment(description);
        return new ScResult<Guid>(result);
    }

    // PUT api/<PaymentController>/5
    [HttpPut("cancellation/{id}")]
    public ScResult<bool> Canclellation(string id)
    {
        var result=  _basePaymentService.ChangeState(Guid.Parse(id), 1);
        return new ScResult<bool>(result);
    }

}