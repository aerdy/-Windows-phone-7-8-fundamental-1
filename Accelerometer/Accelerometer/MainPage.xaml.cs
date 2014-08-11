using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Accelerometer.Resources;

namespace Accelerometer
{
    public partial class MainPage : PhoneApplicationPage
    {
        double screenWidth;
        double screenHeight;
        const double radius = 12;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            screenHeight = Application.Current.Host.Content.ActualHeight / 2;
            screenWidth = Application.Current.Host.Content.ActualWidth / 2;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            Dot.Height = radius * 2;
            Dot.Width = radius * 2;

          
        }

       

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}