namespace DomainModel.Entities
{
    public class Country : Entity
    {
        protected internal Country() { }

        protected internal Country(string publicId, string name)
        {
            PublicId = publicId;
            Name = name;
        }

        public string PublicId { get; private set; }
        public string Name { get; private set; }
    }
}
