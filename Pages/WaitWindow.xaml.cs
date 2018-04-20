using System;
using System.Windows;

namespace I2P_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для WaitWindow.xaml
    /// </summary>
    public partial class WaitWindow : Window
    {
        public WaitWindow()
        {
            InitializeComponent();
            XamlAnimatedGif.AnimationBehavior.SetSourceUri(img_loading, new Uri("/Sprites/Anim/loading_cicle.gif", UriKind.Relative));
        }
    }
}
