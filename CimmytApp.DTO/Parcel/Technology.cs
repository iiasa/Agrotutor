namespace CimmytApp.DTO.Parcel
{
    public class Technology
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}