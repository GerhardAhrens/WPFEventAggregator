namespace WPFEventAggregator.Features
{
    using System;

    public interface IEventAggregator
    {
        IDisposable Subscribe<TEvent>(Func<TEvent, CancellationToken, ValueTask> handler);

        ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);
    }
}
