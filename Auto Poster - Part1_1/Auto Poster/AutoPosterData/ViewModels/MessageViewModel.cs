using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace AutoPosterData
{
    public class MessageViewModel : INotifyPropertyChanged
    {

        public MessageViewModel()
        {
            _messageContent = string.Empty;
            _name = string.Empty;
            _url = string.Empty;
            _description = string.Empty;
        }

        /// <summary>
        /// This field is used for consistency purposes between POCO (MessageModel) and this View Model
        /// </summary>
        public int MessageId
        {
            get;
            set;
        }

        private string _messageContent;
        /// <summary>
        /// Display message of the contact
        /// </summary>
        /// <returns></returns>
        public string MessageContent
        {
            get
            {
                return _messageContent;
            }
            set
            {
                if (value != _messageContent)
                {
                    _messageContent = value;
                    NotifyPropertyChanged("MessageContent");
                }
            }
        }


        private string _url;
        /// <summary>
        /// Url of the contact
        /// </summary>
        /// <returns></returns>
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    NotifyPropertyChanged("Url");
                }
            }
        }

        private string _name;
        /// <summary>
        /// Name of the contact
        /// </summary>
        /// <returns></returns>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _description;
        /// <summary>
        /// Description of the contact
        /// </summary>
        /// <returns></returns>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}