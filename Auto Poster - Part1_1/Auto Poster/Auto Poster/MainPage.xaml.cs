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
using AutoPosterData;
using AutoPosterData.Common;
using Microsoft.Phone.Shell;

namespace Auto_Poster
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region | Variable Decelration |

        bool successfullyLoaded = true;

        #endregion

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            using (AutoPosterDataContext context = new AutoPosterDataContext(Shared.ConnectionString))
            {
                if (context.DatabaseExists())
                {
                    successfullyLoaded = true;
                }
                else
                {
                    // create database if it does not exist
                    context.CreateDatabase();
                    successfullyLoaded = true;
                }
            }
            lstMessage.DataContext = App.MainViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (successfullyLoaded)
                {
                    App.MainViewModel.LoadMessages();

                    if (lstMessage.Items.Count > 0)
                    {
                        lstMessage.SelectedIndex = 0;

                        ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = true;
                        ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).IsEnabled = true;
                    }
                    else
                    {
                        ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
                        ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MessagePage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lstMessage.SelectedItem != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete the selected message?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        //Delete the message
                        MessageViewModel messageViewModel = (MessageViewModel)lstMessage.SelectedItem;
                        App.MainViewModel.DeleteMessage(messageViewModel);

                        //Reload the messages
                        App.MainViewModel.LoadMessages();

                        if (lstMessage.Items.Count > 0)
                        {
                            lstMessage.SelectedIndex = 0;
                            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = true;
                            ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).IsEnabled = true;
                        }
                        else
                        {
                            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
                            ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (lstMessage.SelectedItem != null)
                {
                    MessageViewModel messageViewModel = (MessageViewModel)lstMessage.SelectedItem;
                    NavigationService.Navigate(new Uri(String.Format("/MessagePage.xaml?MessageId={0}", messageViewModel.MessageId.ToString()), UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>Page
        /// Schedule 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            try
            {
                //TODO: Part 2
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

        private void lstMessage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}