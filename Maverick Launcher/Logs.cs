using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Logs : Form
    {
        public static ObservableCollection<string> LogEntries= new ObservableCollection<string>();

        public Logs()
        {
            LogEntries.CollectionChanged += OnListChanged;

            InitializeComponent();
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            LogEntries.ToList().ForEach(Log => LogsTextBox.AppendText(Log + Environment.NewLine) );
        }

        private void OnListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            foreach (var item in args.NewItems)
            {
                LogsTextBox.AppendText(item + Environment.NewLine);
            }

            LogsTextBox.Refresh();

            Console.WriteLine("Added New Text to Logs");
        }

        private void ShowLogs_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
