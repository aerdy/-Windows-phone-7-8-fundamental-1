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
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace AutoPosterData.Common
{

    public static class Shared
    {
        public const string ConnectionString = "Data Source='isostore:/AutoPoster.sdf';Password=geek;";

        public static MessageViewModel MessageTomessageViewModel(Message message)
        {
            MessageViewModel messageViewModel = new MessageViewModel();
            messageViewModel.MessageId = message.MessageId;
            messageViewModel.MessageContent = message.MessageContent;
            messageViewModel.Url = message.Url;
            messageViewModel.Name = message.Name;
            messageViewModel.Description = message.Description;

            return messageViewModel;
        }

        public static Message MessageViewModelToMessage(MessageViewModel messageViewModel)
        {
            Message message = new Message();
            message.MessageId = messageViewModel.MessageId;
            message.MessageContent = messageViewModel.MessageContent;
            message.Url = messageViewModel.Url;
            message.Name = messageViewModel.Name;
            message.Description = messageViewModel.Description;

            return message;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> obsColl = new ObservableCollection<T>();
            foreach (T element in source)
            {
                obsColl.Add(element);
            }
            return obsColl;
        }

    }

}