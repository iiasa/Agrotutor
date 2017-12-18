namespace CimmytApp.DTO.Parcel
{
    using System;

    public class Technology
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}