
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Update
{
    public class UpdateEventCommand:IRequest<ScResult<string>>
    {
        public Guid Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid IdImage { get; set; }

        public Guid IdSpace { get; set; }
    }
}
