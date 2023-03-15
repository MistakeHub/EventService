namespace EventService.Models.Entities
{
    public class Space
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

       
    }
}
