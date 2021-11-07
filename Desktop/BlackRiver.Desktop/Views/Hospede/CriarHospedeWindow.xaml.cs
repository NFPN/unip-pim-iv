﻿using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarHospedeWindow.xaml
    /// </summary>
    public partial class CriarHospedeWindow : Window
    {
        public CriarHospedeWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnNovoHospedeCadastrar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Create with API call
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
