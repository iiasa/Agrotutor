namespace Helper.DTO
{
    using Helper.Map;
    using Prism.Events;

    public class LivePositionEvent : PubSubEvent<GeoPosition>
    {
    }
}