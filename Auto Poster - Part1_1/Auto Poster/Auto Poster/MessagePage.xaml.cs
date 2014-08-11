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
using Microsoft.Phone.Tasks;
using System.Windows.Navigation;
using AutoPosterData;
using AutoPosterData.Common;

namespace Auto_Poster
{
    public partial class MessagePage : PhoneApplicationPage
    {
        int messageId = -1;
        MessageViewModel messageModel = new  MessageViewModel();

        // Constructor
        public MessagePage()
        {
            InitializeComponent();
            TiltEffect.SetIsTiltEnabled(this, true);

            this.DataContext = messageModel;

            this.Loaded += new RoutedEventHandler(MessagePage_Loaded);
        }

        void MessagePage_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxMessageContent.Focus();
        }

        // When page is navigated to look for MessageId and if found, load corresponding message from DB
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string value;

                //Try to Load Message Id (this will be edit case)
                if (NavigationContext.QueryString.TryGetValue("MessageId", out value))
                {
                    messageId = Convert.ToInt32(value);
                    messageModel = App.MainViewModel.GetMessage(messageId);
                    this.DataContext = messageModel;
                }
                TextBoxMessageContent.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            try 
            {
                Message message = Shared.MessageViewModelToMessage(messageModel);

                //Sync Items (since focus lost is not called on application bar button press)
                //Consider looking at Url below,
                //http://stackoverflow.com/questions/8168861/two-way-databinding-from-textbox-doesnt-update-when-button-in-applicationbar-is
                //However to keep it simple for the purpose of this app, I will simply reflect the data
                message.MessageContent = TextBoxMessageContent.Text.Trim();
                message.Url = TextBoxUrl.Text.Trim();
                message.Name = TextBoxName.Text.Trim();
                message.Description = TextBoxDescription.Text.Trim();

                if (string.IsNullOrEmpty(message.MessageContent))
                {
                    StatusMessage.Text = "Please input a message.";
                    TextBoxMessageContent.Focus();
                }
                else
                {
                    if (messageId == -1)
                    {
                        App.MainViewModel.AddMessage(message);
                    }
                    else
                    {
                        App.MainViewModel.UpdateMessage(message);
                    }

                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {

        }

    }
}