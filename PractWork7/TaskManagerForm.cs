using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace PractWork7
{
    public partial class TaskManagerForm : Form
    {
        public TaskManagerForm()
        {
            InitializeComponent();
        }

        private void StartNewProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProcessForm newProcess = new NewProcessForm();
            newProcess.ShowDialog(); 
        }

        private void TaskManagerForm_Load(object sender, EventArgs e)
        {            
            FillProcessesListView();
            processesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            FillDetailsListView();
            detailsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            processesStatusLabel.Text = $"Running: {detailsListView.Items.Count}";
        }
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshProcessesListView();
            RefreshDetailsListView();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var processId = Convert.ToInt32(detailsListView.SelectedItems[0].SubItems[1].Text);
            var temp = Process.GetProcessById(processId);
            temp.Kill();
            temp.WaitForExit();
            RefreshDetailsListView();
        }

        private void FillProcessesListView()
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes.OrderBy(p => p.ProcessName))
            {
                if (process.MainWindowTitle != string.Empty)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        applicationColumn.Name = process.MainWindowTitle,
                        statrTimeColumn.Name = process.StartTime.ToString()
                    });
                    processesListView.Items.Add(item);
                }
            }
        }

        private void FillDetailsListView()
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes.OrderBy(p => p.ProcessName))
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    nameColumn.Name = process.ProcessName,
                    idColumn.Name = process.Id.ToString(),
                    memoryColumn.Name = (process.WorkingSet64 >> 10).ToString()
                });
                detailsListView.Items.Add(item);
            }
        }

        private void RefreshDetailsListView()
        {
            detailsListView.Items.Clear();
            FillDetailsListView();
        }

        private void RefreshProcessesListView()
        {
            processesListView.Items.Clear();
            FillProcessesListView();
        }

    }
}
