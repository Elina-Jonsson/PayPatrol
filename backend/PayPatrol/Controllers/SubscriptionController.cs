using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PayPatrol.Application.Interfaces;
using PayPatrol.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PayPatrol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()   
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _subscriptionService.GetAllSubscriptionsByUserIdAsync(userId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _subscriptionService.GetSubscriptionByIdAsync(id, userId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _subscriptionService.CreateSubscriptionsAsync(dto, userId);

            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSubscriptionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var success = await _subscriptionService.UpdateSubscriptionAsync(dto, userId, id);

            if (!success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized();
            }

            var success = await _subscriptionService.DeleteSubscriptionAsync(id, userId);
            
            if (!success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized();
            }

            var summary = await _subscriptionService.GetSubscriptionSummaryAsync(userId);
            return Ok(summary);
        }

    }
}
