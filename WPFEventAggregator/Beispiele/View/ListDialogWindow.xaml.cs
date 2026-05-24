namespace WPFEventAggregator.Beispiele
{
    using System.ComponentModel;
    using System.Data;
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für ListDialogWindow.xaml
    /// </summary>
    public partial class ListDialogWindow : WindowBase
    {
        public ListDialogWindow(string param)
        {
            this.InitializeComponent();
            WeakEventManager<WindowBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            WeakEventManager<WindowBase, CancelEventArgs>.AddHandler(this, "Closing", this.OnWindowClosing);
            this.WindowTitel = param;
            this.DataContext = this;
        }

        public ListDialogWindow(string param,DataTable table)
        {
            this.InitializeComponent();
            WeakEventManager<WindowBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            WeakEventManager<WindowBase, CancelEventArgs>.AddHandler(this, "Closing", this.OnWindowClosing);
            this.WindowTitel = param;
            this.DemoTabelle = table;
            this.DataContext = this;
        }

        public string WindowTitel
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public DataTable DemoTabelle
        {
            get => base.GetValue<DataTable>();
            set => base.SetValue(value);
        }

        #region WindowEventHandler
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnCloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (this.Owner != null)
            {
                this.DialogResult = false;
            }
        }
        #endregion WindowEventHandler
    }
}
