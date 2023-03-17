﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventService.Features.Stub
{
    [Route("api/[controller]")]
    [ApiController]
    public class StubController : ControllerBase
    {
       

        // POST api/<StubController>
        [HttpPost("authstub")]
        [Authorize]
        public string Post([FromBody] string value)
        {
            return "hello";
        }

     
    }
}