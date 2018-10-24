namespace CimmytApp.Core.Parcel.ViewModels
{
    using CimmytApp.DTO.Parcel;

    /// <summary>
    ///     Defines the <see cref="ParcelViewModel" />
    /// </summary>
    public class ParcelViewModel
    {
        /// <summary>
        ///     Gets or sets a value indicating whether IsOptionsVisible
        /// </summary>
        public bool IsOptionsVisible { get; set; }

        /// <summary>
        ///     Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel { get; set; }
    }
}