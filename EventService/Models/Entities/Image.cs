namespace EventService.Models.Entities
{
    public class Image
    {
        public Guid Id { get; set; }=Guid.NewGuid();

        public string FilePath { get; set; }
    }
}
