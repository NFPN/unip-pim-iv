﻿using BlackRiver.Desktop.Extensions;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for QuartoControl.xaml
    /// </summary>
    public partial class QuartoControl : UserControl, IControlUpdate
    {
        public QuartoControl()
        {
            InitializeComponent();
        }

        public void UpdateControlData()
        {
            //TODO: refresh data
        }

        private void btnAddQuarto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarQuartoWindow().SafeShowDialog();
        }

        private void btnEditarQuarto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new EditarQuartoWindow().SafeShowDialog();
        }
    }
}
