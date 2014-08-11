using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace AutoPosterData
{
    [Table(Name = "Messages")]
    public class Message
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int MessageId
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string MessageContent
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string Url
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string Name
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string Description
        {
            get;
            set;
        }

    }
}