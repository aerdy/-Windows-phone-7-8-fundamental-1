using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AutoComplete.Resources;

namespace AutoComplete 
{
    public partial class MainPage : IEnumerable
    {
        // Constructor
        public IEnumerable AutoCompletions = new List<string>()
    {
         "Lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "Nullam", "felis", "dui", "gravida", "at", 
         "condimentum", "eget", "mattis", "non", "est", "Duis", "porta", "ornare", "tellus", "at", "convallis", "nibh", "aliquam", 
         "faucibus", "Vivamus", "molestie", "fringilla", "ullamcorper", "Aenean", "non", "diam", "eu", "sapien", "pretium", "iaculis", 
         "Quisque", "at", "ante", "libero", "eu", "tincidunt", "urna", "Cras", "libero", "ligula", "hendrerit", "at", "posuere", "at", 
         "tempor", "at", "nulla", "Aliquam", "feugiat", "sagittis", "dolor", "convallis", "porttitor", "neque", "commodo", "ut", "Praesent",
         "egestas", "tincidunt", "lectus", "et", "pharetra", "enim", "semper", "et", "Fusce", "placerat", "orci", "vel", "iaculis", 
         "dictum", "nulla", "sem", "convallis", "nunc", "in", "viverra", "leo", "mauris", "eu", "odio", "Nullam", "et", "ultricies", 
         "sapien", "Proin", "quis", "mi", "a", "sapien", "semper", "lobortis", "ut", "eget", "est", "Suspendisse", "scelerisque", "porta",
         "mattis", "In", "eleifend", "tellus", "vel", "nulla", "aliquam", "ornare", "Praesent", "tincidunt", "dui", "ut", "libero",
         "iaculis", "consequat", "Nunc", "interdum", "eleifend", "rhoncus", "Curabitur", "sollicitudin", "nulla", "sagittis", "quam", 
         "vehicula", "cursus", "Fusce", "laoreet", "arcu", "vitae", "fringilla", "scelerisque", "nisi", "purus", "laoreet", "ipsum", "id", 
         "suscipit", "erat", "tellus", "eu", "sapien", "Proin", "pharetra", "tortor", "nisl", "Etiam", "et", "risus", "eget", "lectus",
         "vulputate", "dignissim", "ac","sed", "erat", "Nulla", "vel", "condimentum", "nunc", "Suspendisse", "aliquam", "euismod", "dictum",
         "Ut", "arcu", "enim", "consectetur", "at", "rhoncus", "at", "porta", "ut", "lacus", "Donec", "nisi", "quam", "faucibus", "tempor",
         "tincidunt", "eu", "porttitor", "id", "ipsum", "Proin", "nec", "neque", "nulla", "Suspendisse", "sapien", "metus", "aliquam", "nec", 
         "dapibus", "consequat", "rutrum", "id", "leo", "Donec", "ac", "fermentum", "tortor", "Pellentesque", "nisl", "orci", "tincidunt", 
         "at", "iaculis", "vitae", "consequat", "scelerisque", "ante", "Suspendisse", "potenti", "Maecenas", "auctor", "justo", "a", "nibh",
         "sagittis", "facilisis", "Phasellus", "ultrices", "lectus", "a", "nisl", "pretium", "accumsan"
    };

        public IEnumerator GetEnumerator()
        {
            return AutoCompletions.GetEnumerator();
        }
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
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