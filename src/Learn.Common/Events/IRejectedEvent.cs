namespace Learn.Common.Events
{
    public interface IRejectedEvent: IEvent
    {
         string Reason{get;}
        //Make Code as Enum
         string Code{get;}
    }
}