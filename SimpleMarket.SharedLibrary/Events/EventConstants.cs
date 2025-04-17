namespace SimpleMarket.SharedLibrary.Events;

public class EventConstants
{
    public const string EventIdHeaderKey = "message.event_id";
    public const string EventTimeHeaderKey = "message.publish_time";

    public class Formats
    {
        public const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";
    }
}