using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using System.Net;
using System.Windows.Ink;
using System.Windows.Media.Animation;
using System.Linq;
using AutoPosterData.Common;

namespace AutoPosterData
{
    public class MainViewModel 
    {
        public MainViewModel()
        {
            this.Messages = new ObservableCollection<MessageViewModel>();
        }

        /// <summary>
        /// A collection for MessageViewModel  objects.
        /// </summary>
        public ObservableCollection<MessageViewModel> Messages { get; private set; }

        /// <summary>
        /// Creates and adds a few MessageViewModel  objects into the Messages collection.
        /// </summary>
        public void LoadMessages()
        {
            this.Messages.Clear();

            IList<Message> messagesList = new List<Message>();
            using (AutoPosterDataContext context = new AutoPosterDataContext(Shared.ConnectionString))
            {

                IQueryable<Message> query = from s in context.Messages select s;
                messagesList = query.ToList();
            }

            foreach (Message msg in messagesList)
            {
                MessageViewModel messageViewModel = Shared.MessageTomessageViewModel(msg);
                this.Messages.Add(messageViewModel);
            }

        }

        public MessageViewModel GetMessage(int messageId)
        {
            MessageViewModel messageViewModel = new MessageViewModel();
            using (AutoPosterDataContext context = new AutoPosterDataContext(Shared.ConnectionString))
            {
                IQueryable<Message> messageQuery = from c in context.Messages where c.MessageId == messageId select c;
                Message messageToupdate = messageQuery.FirstOrDefault();

                messageViewModel = Shared.MessageTomessageViewModel(messageToupdate);
            }
            return messageViewModel;
        }

        public void AddMessage(Message messageToAdd)
        {
            using (AutoPosterDataContext context = new AutoPosterDataContext(Shared.ConnectionString))
            {
                Message message = new Message();
                message.MessageContent = messageToAdd.MessageContent;
                message.Url = messageToAdd.Url;
                message.Name = messageToAdd.Name;
                message.Description = messageToAdd.Description;

                // add the new category to the context
                context.Messages.InsertOnSubmit(message);

                // save changes to the database
                context.SubmitChanges();
            }
        }

        public void DeleteMessage(MessageViewModel message)
        {
            using (AutoPosterDataContext context = new AutoPosterDataContext(Shared.ConnectionString))
            {
                //Pick up the message to be deleted
                IQueryable<Message> messageQuery = from c in context.Messages where c.MessageId == message.MessageId select c;
                Message messageToupdate = messageQuery.FirstOrDefault();

                context.Messages.DeleteOnSubmit(messageToupdate);

                // save changes to the database
                context.SubmitChanges();
            }
        }

        public void UpdateMessage(Message message)
        {
            using (AutoPosterDataContext context = new AutoPosterDataContext(Shared.ConnectionString))
            {
                //Pick the message to update
                IQueryable<Message> messageQuery = from c in context.Messages where c.MessageId == message.MessageId select c;
                Message messageToupdate = messageQuery.FirstOrDefault();

                messageToupdate.MessageContent = message.MessageContent;
                messageToupdate.Url = message.Url;
                messageToupdate.Name = message.Name;
                messageToupdate.Description = message.Description;

                // save changes to the database
                context.SubmitChanges();
            }
        }

    }
}
