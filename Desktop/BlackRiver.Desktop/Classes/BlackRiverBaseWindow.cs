using System.Windows;

namespace BlackRiver.Desktop
{
    class BlackRiverBaseWindow: Window
    {
        public BlackRiverBaseWindow() 
            => MouseDown += delegate { DragMove(); };
    }
}
