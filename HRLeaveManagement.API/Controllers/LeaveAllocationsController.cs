using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await mediator.Send(new GetLeaveAllocationsQuery());
            return Ok(leaveAllocations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailsQuery(id));
            return Ok(leaveAllocation);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post(CreateLeaveAllocationCommand createLeaveAllocation)
        {
            var response = await mediator.Send(createLeaveAllocation);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveAllocationCommand updateLeaveAllocation)
        {
            await mediator.Send(updateLeaveAllocation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
