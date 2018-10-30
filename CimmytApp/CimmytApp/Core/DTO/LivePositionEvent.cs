namespace CimmytApp.Core.DTO
{
    using CimmytApp.Core.Map;
    using Prism.Events;

    public class LivePositionEvent : PubSubEvent<GeoPosition>
    {
    }
}