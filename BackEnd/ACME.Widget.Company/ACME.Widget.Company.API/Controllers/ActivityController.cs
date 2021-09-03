using ACME.Widget.Company.API.Models;
using ACME.Widget.Company.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Widget.Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public ActivityController()
        {

        }

        [HttpGet("{activityId}/participants")]
        public Participants GetParticipants(int activityId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{activityId}")]
        public IActionResult SignUp(SignUpInformation info)
        {

            return BadRequest("");
        }
    }
}
