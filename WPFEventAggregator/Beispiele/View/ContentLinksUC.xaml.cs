namespace WPFEventAggregator.Beispiele
{
    using System.Windows;

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

        private async void OnSendStatus()
        {
            if (App.EventAgg.IsSubscription<StatusEvent>() == true)
            {
                await App.EventAgg.PublishAsync(new StatusEvent(Guid.NewGuid(), this.StatusMessage));
            }
        }
    }
}

