namespace CimmytApp.DTO.Parcel
{
    public class Technology
    {
        public string Name { get; set; }

        public int? Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}