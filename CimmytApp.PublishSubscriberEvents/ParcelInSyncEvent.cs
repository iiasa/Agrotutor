using CimmytApp.DTO;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.PublishSubscriberEvents
{
    public class ParcelInSyncEvent: PubSubEvent<Parcel>
    {
    }
}
