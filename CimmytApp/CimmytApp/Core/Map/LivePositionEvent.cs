namespace CimmytApp.Core.Map
{
    using Helper.Map;
    using Prism.Events;

    public class LivePositionEvent : PubSubEvent<GeoPosition>
    {
    }
}