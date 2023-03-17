namespace EventService.Models.Entities
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }  
        
        public string? Title { get; set; }

        public string? Description { get; set; }

        public Guid? IdImage { get; set; }

        public Guid? IdSpace { get; set; }

        public List<Ticket> Tickets { get; set; }=new List<Ticket>();

        // ReSharper disable once UnusedMember.Global 
        public bool IsAvailableTickets
        {
            get { return Tickets.Select(v=>v.IdOwner!=null).Count() != 0; }
        }
    }
}
