﻿namespace Helper.DTO
{
    using Prism.Events;

    using Map;

    public class LivePositionEvent : PubSubEvent<GeoPosition>
    {
    }
}