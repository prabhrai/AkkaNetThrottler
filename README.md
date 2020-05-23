# AkkaNetThrottler
akka net dispatching throttling

Implementation of throttling behavior in Akka.Net 

ThrottlerActor allows throttled processing of messages, initialize using params for
* number of messages to process
* the timespan interval for the given number of messages.

<code>
public ThrottlerActor(TimeSpan interval, uint numberOfMessages)
</code>
