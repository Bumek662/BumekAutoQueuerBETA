using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LoLLauncher;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
namespace Bumek_Queuer
{
    /// <summary>
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : UserControl
    {

        public UserPanel()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog
            {
                Multiselect = false,
                Title = "Find file 'lol.launcher.exe'",
                Filter = "lol.launcher.exe|lol.launcher.exe"
            };
            using (dialog)
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Connection.Path = dialog.FileName.Replace("lol.lauuncher.exe", "");
                    System.Diagnostics.Process.Start("http://bumektest.dejm.pl/donate/index.php");
                }
            }
            InitializeComponent();
            DispatcherHelper.Initialize();
            loggedName.Content = "Account: " + Connection.login;
            summNameLabel.Content = "Name: " + Connection.SummName;
            summLvlLabel.Content = "Lvl: " + Connection.SummLvl;
            summIPLabel.Content = "IP: " + Connection.summIP;
            Connection.lolConnection.Disconnect();
            Messenger.Default.Register<StatusUpdateMessage>(this, message =>
            {
                this.Dispatcher.Invoke((Action)(() => { statusLabel.Content = message.Text; }));
            });
        }


        public string Text { get; set; }
            public void StatusUpdateMessage(string text)
            {
               statusLabel.Content = text;
            }

        private void QueueUP_Click(object sender, RoutedEventArgs e)
        {
            QueueTypes queuetype = (QueueTypes)System.Enum.Parse(typeof(QueueTypes), queueType1.Text);
            Queueing bumekQueue = new Queueing(champion.Text, spell1.Text, spell2.Text, queuetype);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            Connection.lolConnection.Disconnect();
            System.Environment.Exit(0);
        }




    }

    public class StatusUpdateMessage
    {
        public string Text { get; set; }

        public StatusUpdateMessage(string text)
        {
            Text = text;
        }
    }
}
