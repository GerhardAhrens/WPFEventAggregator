namespace WPFEventAggregator.Features
{
    using System;

    public interface IEventAggregator
    {
        IDisposable Subscribe<TEvent>(Func<TEvent, CancellationToken, ValueTask> handler);

        ValueTask PublishAsync<TEvent>(TEvent eventItem, CancellationToken cancellationToken = default);
    }
}
