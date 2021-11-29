﻿using BlackRiver.Desktop.Views;
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
            Application.Current.Dispatcher.Invoke(delegate
            {
                _ = window.ShowDialog();
            });
        }

        public static T GetParentWindow<T>(this Control control) where T : Window
        {
            return (T)Window.GetWindow(control);
        }
    }
}
