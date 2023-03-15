namespace EventService.Models.Entities
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Start { get; set; }

        public DateTime End { get; set; }  
        
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid IdImage { get; set; }

        public Guid IdSpace { get; set; }
    }
}
