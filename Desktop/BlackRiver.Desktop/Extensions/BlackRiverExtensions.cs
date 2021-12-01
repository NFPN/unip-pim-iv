using BlackRiver.Desktop.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Extensions
{
    public static class BlackRiverExtensions
    {
        public static void ShowMessage(string usermessage, string title = null)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                var errorWindow = new BlackRiverMessageWindow(usermessage, title).ShowDialog();
            });
        }

        public static void SafeShowDialog<T>(this T window) where T : Window
        {
            try
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    _ = window.ShowDialog();
                });
            }
            catch { }
        }

        public static T GetParentWindow<T>(this Control control) where T : Window
        {
            return (T)Window.GetWindow(control);
        }

        public static void SafeDragMove(this Window window)
        {
            try { window.DragMove(); }
            catch { }
        }

        public static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var column = dataGrid.Columns[columnIndex];

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }
    }
}
