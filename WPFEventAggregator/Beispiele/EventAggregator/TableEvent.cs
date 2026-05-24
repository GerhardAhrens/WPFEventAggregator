namespace WPFEventAggregator.Beispiele
{
    using System;
    using System.Data;

    public sealed class TableEvent
    {
        public TableEvent(Guid id, DataTable messageAsTable)
        {
            this.Id = id;
            this.MessageAsTable = messageAsTable;
        }

        public Guid Id { get; private set; }
        public DataTable MessageAsTable { get; private set; }
    }
}
