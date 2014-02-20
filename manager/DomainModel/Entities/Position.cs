namespace DomainModel.Entities
{
    public class Position : Entity
    {
        protected internal Position() { }

        protected internal Position(string publicId, string name)
        {
            PublicId = publicId;
            Name = name;
        }

        public string PublicId { get; private set; }
        public string Name { get; private set; }
    }
}
