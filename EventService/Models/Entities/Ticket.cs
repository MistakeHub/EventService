namespace EventService.Models.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? IdOwner { get; set; }

        public int Place { get; set; }

    }
}
