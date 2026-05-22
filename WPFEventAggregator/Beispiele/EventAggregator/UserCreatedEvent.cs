namespace WPFEventAggregator.Beispiele
{
    using System;

    public sealed class UserCreatedEvent
    {
        public UserCreatedEvent(Guid id, string email)
        {
            this.Id = id;
            this.Email = email;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
    }
}
