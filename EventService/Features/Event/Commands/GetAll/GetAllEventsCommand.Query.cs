
using EventService.Models.ViewModels;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.GetAll
{   
    public class GetAllEventsCommand:IRequest<ScResult<List<EventViewModel>>>
    {
    }
}
