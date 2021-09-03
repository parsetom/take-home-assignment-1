using ACME.Widget.Company.API.Models;
using ACME.Widget.Company.Common.Models;
using ACME.Widget.Company.Services;
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
        private ActivityService activityService;
        public ActivityController(ActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet("{activityId}/participants")]
        public async Task<IActionResult> GetParticipants(int activityId)
        {
            var response = await this.activityService.GetParticipantsAsync(activityId);

            if (response.ErrorCode != Common.ErrorCodes.None)
            {
                return BadRequest(response.ErrorCode);
            }

            return Ok(response.Result);
        }

        [HttpPost("{activityId}/sign-up")]
        public async Task<IActionResult> SignUp(SignUpInformation info)
        {
            var person = new Person
            {
                FirstName = info.FirstName,
                LastName = info.LastName,
                Email = info.Email
            };// Let us use ORM next time.

            var response = await this.activityService.RegisterParticipantAsync(info.ActivityId, info.Comments, person);
            if (response.Result)
            {
                return new ObjectResult(response.Result) { StatusCode = StatusCodes.Status201Created };
            }
            else
            {
                return BadRequest(response.ErrorCode);
            }
        }
    }
}
