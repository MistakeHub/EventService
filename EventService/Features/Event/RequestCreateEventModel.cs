namespace EventService.Features.Event
{
    public class RequestCreateEventModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IdImage { get; set; }
        public string IdSpace { get; set; }
    }
}
