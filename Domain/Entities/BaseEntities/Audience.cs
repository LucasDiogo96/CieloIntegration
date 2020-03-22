namespace Domain.Entities.BaseEntities
{
    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
        public string Name { get; set; }
    }
}
