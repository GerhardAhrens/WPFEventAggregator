//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Lifeprojects.de">
//     Class: MainWindow
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>05.03.2026 18:21:36</date>
//
// <summary>
// WPF Template mit Minimalfunktionen
// </summary>
//-----------------------------------------------------------------------

namespace WPFEventAggregator
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using WPFEventAggregator.Beispiele;
    using WPFEventAggregator.TemplateCore;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            this.InitializeComponent();
            WeakEventManager<WindowBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            WeakEventManager<WindowBase, CancelEventArgs>.AddHandler(this, "Closing", this.OnWindowClosing);

            this.SetVectorIcon("IconEventAggregator", 64);

            this.QuitCommand = new CommandBase(this.OnQuit, () => true);
            this.StartCommand = new CommandBase(this.OnStart);
            this.ChangeDialogCommand = new CommandBase(this.OnChangeDialog);
            this.SendMessageCommand = new CommandBase(this.OnSendMessage);
            this.DemoDialogServiceCommand = new CommandBase(this.OnDemoDialogService);

            this.InformationCommand = new CommandBase(this.OnInformationPopup);
            this.CloseInformationPopupCommand = new CommandBase(this.OnCloseInformation);
            this.SettingsCommand = new CommandBase(this.OnSettingsPopup);
            this.CloseSettingsPopupCommand = new CommandBase(this.OnCloseSettingsPopup);

            this.WindowTitel = LocalizationValue.Get("WindowsTitelZeile");
            this.ApplikationVersion = base.ApplicationVersion.ToString();
            this.LaufzeitVersion = base.RuntimeVersion;
            this.WinVersion = base.WindowsVersion;
            this.DataContext = this;

            var subscriptionMessage = App.EventAgg.Subscribe<MessageEvent>(async (evt, ct) =>
            {
                await Task.Delay(10, ct);
                this.Message.Hinweis("Event Aggregator Demo", evt.Message);
            });

            App.EventAgg.Subscribe<StatusEvent>(async (evt, ct) => this.OnUpdateStatusBar(evt));
        }

        private async void OnSendMessage()
        {
            if (App.EventAgg.IsSubscription<MessageEvent>() == true)
            {
                await App.EventAgg.PublishAsync(new MessageEvent(Guid.NewGuid(), "Das ist eine Nachricht mit dem Event Aggregator"));
            }
        }

        #region Commands
        public CommandBase QuitCommand { get; private set; }
        public CommandBase StartCommand { get; private set; }
        public CommandBase ChangeDialogCommand { get; private set; }
        public CommandBase SendMessageCommand { get; private set; }
        public CommandBase DemoDialogServiceCommand { get; private set; }
        public CommandBase InformationCommand { get; private set; }
        public CommandBase CloseInformationPopupCommand { get; private set; }
        public CommandBase SettingsCommand { get; private set; }
        public CommandBase CloseSettingsPopupCommand { get; private set; }
        public CommandBase ShowMessageCommand { get; private set; }
        #endregion Commands

        #region Properties
        public string WindowTitel
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string ApplikationVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string LaufzeitVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string WinVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public UserControl WorkContentLeft
        {
            get { return base.GetValue<UserControl>(); }
            set { base.SetValue(value); }
        }

        public UserControl WorkContentRight
        {
            get { return base.GetValue<UserControl>(); }
            set { base.SetValue(value); }
        }

        private MessageBase Message { get; } = new MessageBase();
        #endregion Properties

        #region Windows Event Handler
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StatusbarMain.Statusbar.DatabaseInfo = "Keine";
            StatusbarMain.Statusbar.DatabaseInfoTooltip = "Keine Datenbank verbunden";
            StatusbarMain.Statusbar.Notification = "Bereit";

            this.ChangeControl("Home");
        }

        private void OnCloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            MessageBoxResult msgYN;
            if (this.Tag != null)
            {
                msgYN = this.Message.AppExitMessage(this.Tag.ToString());
            }
            else
            {
                msgYN = this.Message.AppExitMessage();
            }

            if (msgYN == MessageBoxResult.Yes)
            {
                App.ApplicationExit();
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion Windows Event Handler

        #region Command Event Handler
        private void OnQuit()
        {
            this.Tag = null;
            this.Close();
        }

        private void OnStart()
        {
            this.ChangeControl("Demo");
        }

        private void OnChangeDialog()
        {
            this.ChangeControl("Home");
        }

        private void OnDemoDialogService()
        {
            int variante = 5;
            if (variante == 1)
            {
                object parm = $"Einfacher Aufruf: \n var response = new DialogService<DialogWindow>().ShowDialog();";
                var response = new DialogService<DialogWindow>(parm).WithOwner(this).ShowDialog();
                if (response.DialogResult == true)
                {
                    // OK
                }
                else
                {
                    // Abbrechen
                }
            }
            else if (variante == 2)
            {
                object parm = $"Aufruf mit Fluent-API: \n var response = new DialogService<DialogWindow>()\n.WithTitle(\"Dialog Window\")\n.WithSize(700, 450)\n.CenterToScreen()\n.WithFont(\"Segoe UI\")\n.TopMost()\n.ShowDialog();";
                var response = new DialogService<DialogWindow>(parm)
                    .WithTitle("Dialog Window")
                    .WithSize(700, 450)
                    .CenterToScreen()
                    .WithFont("Segoe UI")
                    .TopMost()
                    .ShowDialog();
                if (response.DialogResult == true)
                {
                    // OK
                }
                else
                {
                    // Abbrechen
                }
            }
            else if (variante == 3)
            {
                object parm = $"Aufruf mit Konstruktor Parameter: \n var response = new DialogService<DialogWindow>()\n.WithTitle(\"Benutzer\")\n.ShowDialog();";
                var response = new DialogService<DialogWindow>(parm,"Max Mustermann", 42)
                    .WithTitle("Benutzer")
                    .ShowDialog();
                if (response.DialogResult == true)
                {
                    // OK
                }
                else
                {
                    // Abbrechen
                }
            }
            else if (variante == 4)
            {
                object parm = $"Einfacher Aufruf; Animation fading: \n var response = new DialogService<DialogWindow>()\n.WithFadeAnimation()\n.ShowDialog();";
                var response = new DialogService<DialogWindow>(parm).WithOwner(this).WithFadeAnimation().ShowDialog();

                if (response.DialogResult == true)
                {
                    // OK
                }
                else
                {
                    // Abbrechen
                }
            }
            else if (variante == 5)
            {
                object parm = $"Einfacher Aufruf: \n var response = new DialogService<DialogWindow>()\n.Show();";
                var response = new DialogService<DialogWindow>(parm).Show();

                if (response.DialogResult == true)
                {
                    // OK
                }
                else
                {
                    // Abbrechen
                }
            }
        }

        private void OnInformationPopup()
        {
            this.InformationPopup.SetValue(MaskLayerBehavior.IsOpenProperty, true);
        }

        private void OnCloseInformation()
        {
            this.InformationPopup.SetValue(MaskLayerBehavior.IsOpenProperty, false);
        }

        private void OnSettingsPopup()
        {
            this.SettingsPopup.SetValue(MaskLayerBehavior.IsOpenProperty, true);
        }

        private void OnCloseSettingsPopup()
        {
            this.SettingsPopup.SetValue(MaskLayerBehavior.IsOpenProperty, false);
        }
        #endregion Command Event Handler

#pragma warning disable CA1822 // Member als statisch markieren
        private void OnUpdateStatusBar(StatusEvent evt)
        {
            StatusbarMain.Statusbar.Notification = evt.Message;
        }
#pragma warning restore CA1822 // Member als statisch markieren

        private void ChangeControl(string currentWorkContent)
        {
            this.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait);

            this.WorkContentLeft = null;
            this.WorkContentRight = null;

            if (currentWorkContent == "Home")
            {
                this.BorderRight.Visibility = Visibility.Collapsed;

                this.BorderLeft.Visibility = Visibility.Visible;
                this.WorkContentLeft = new HelloUC();
                this.WorkContentLeft.DataContext = this;
                this.WorkContentLeft.Focusable = true;
            }
            else if (currentWorkContent == "Demo")
            {
                this.BorderLeft.Visibility = Visibility.Visible;
                this.WorkContentLeft = new ContentLinksUC();
                this.WorkContentLeft.Focusable = true;

                this.BorderRight.Visibility = Visibility.Visible;
                this.WorkContentRight = new ContentRechtsUC();
                this.WorkContentRight.Focusable = false;
            }

            this.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Arrow);

        }
    }
}