using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Acceloromenter7
{
    
    public partial class MainPage : PhoneApplicationPage
    {
        double halfScreenWidth;
        double halfScreenHeight;
        const double radius = 12;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            halfScreenHeight =
        Application.Current.Host.Content.ActualHeight / 2;
            halfScreenWidth =
                Application.Current.Host.Content.ActualWidth / 2;
            Dot.Height = radius * 2;
            Dot.Width = radius * 2;

        }

        
    }

}
