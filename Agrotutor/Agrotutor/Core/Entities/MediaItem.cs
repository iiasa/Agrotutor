using System;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

namespace Agrotutor.Core.Entities
{
    public class MediaItem
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        [NotMapped]
        public ImageSource ImageSource => ImageSource.FromFile(Path);

        public bool IsUploaded { get; set; }
        public bool IsCreated { get; set; }
        public bool IsVideo { get; set; }
    }
}
