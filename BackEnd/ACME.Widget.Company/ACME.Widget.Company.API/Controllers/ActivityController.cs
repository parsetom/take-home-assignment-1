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

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities([FromQuery] string searchKeyword)
        {
            var response = await this.activityService.GetActivitiesAsync(searchKeyword);
            return GetObjectResult(response);
        }

        [HttpGet("{activityId}/participants")]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants(int activityId)
        {
            var response = await this.activityService.GetParticipantsAsync(activityId);
            return GetObjectResult(response);
        }

        [HttpPost("{activityId}/sign-up")]
        public async Task<IActionResult> SignUp(int activityId, SignUpInformation info)
        {
            var person = new Person
            {
                FirstName = info.FirstName,
                LastName = info.LastName,
                Email = info.Email
            };// Let us use ORM next time.

            var response = await this.activityService.RegisterParticipantAsync(activityId, info.Comments, person);
            if (response.Result)
            {
                return new ObjectResult(response.Result) { StatusCode = StatusCodes.Status201Created };
            }
            else
            {
                return BadRequest(response.ErrorCode);
            }
        }

        private ObjectResult GetObjectResult(IServiceResult response)
        {
            if (response.ErrorCode != Common.ErrorCodes.None)
            {
                return BadRequest(response.ErrorCode);
            }
            return Ok(response.ObjectResult);
        }
    }
}
