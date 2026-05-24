namespace WPFEventAggregator.Beispiele
{
    using System.Data;
    using System.Windows;

    using WPFEventAggregator.TemplateCore;

    /// <summary>
    /// Interaktionslogik für ContentLinksUC.xaml
    /// </summary>
    public partial class ContentLinksUC : UserControlBase
    {
        public ContentLinksUC()
        {
            this.InitializeComponent();
            WeakEventManager<UserControlBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);

            this.SendStatusCommand = new CommandBase(this.OnSendStatus, () => true);
            this.SendTableCommand = new CommandBase(this.OnSendTable, () => true);
            this.DataContext = this;
        }

        public string StatusMessage
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        public CommandBase SendStatusCommand { get; private set; }
        public CommandBase SendTableCommand { get; private set; }

        private async void OnSendStatus()
        {
            if (App.EventAgg.IsSubscription<StatusEvent>() == true)
            {
                await App.EventAgg.PublishAsync(new StatusEvent(Guid.NewGuid(), this.StatusMessage));
            }
        }

        private async void OnSendTable()
        {
            // 1. Neue DataTable erstellen
            DataTable dt = new DataTable("Produkte");

            // 2. Spalten definieren
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Preis", typeof(decimal));

            // 3. Zeilen hinzufügen (Methode 1: Über DataRow)
            DataRow row1 = dt.NewRow();
            row1["ID"] = 1;
            row1["Name"] = "Laptop";
            row1["Preis"] = 899.99m;
            dt.Rows.Add(row1);

            // 3. Zeilen hinzufügen (Methode 2: Direkt als Objekt-Array)
            dt.Rows.Add(2, "Smartphone", 499.50m);
            dt.Rows.Add(3, "Monitor", 189.00m);
            dt.AcceptChanges();

            _ = new DialogService<ListDialogWindow>("Tabelle anzeigen").CenterToScreen().Show();

            if (App.EventAgg.IsSubscription<TableEvent>() == true)
            {

                await App.EventAgg.PublishAsync(new TableEvent(Guid.NewGuid(), dt));
            }
        }
    }
}

