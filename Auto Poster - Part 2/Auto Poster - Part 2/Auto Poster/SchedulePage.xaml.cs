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
using System.Windows.Navigation;

using Facebook;

using AutoPosterData.Common;
using Microsoft.Phone.Scheduler;

namespace Auto_Poster
{
    public partial class SchedulePage : PhoneApplicationPage
    {
        string taskName = "Auto Poster";
        string taskDescription = "Posts random status feeds on authenticated user's wall.";

        private const string AppId = "434081719968538";
        private const string AppSecret = "xxx";

        private const string ExtendedPermissions = "publish_stream";
        private readonly FacebookClient _fb = new FacebookClient();

        public SchedulePage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(AutoPost_Loaded);
        }

        public string AccessToken { get; set; }

        void AutoPost_Loaded(object sender, RoutedEventArgs e)
        {
            //Enable Scheduler turning on if an access key and user id is found in App Settings
            string value;

            value = string.Empty;
            if (AppSettings.TryGetSetting<string>("AccessToken", out value))
            {
                //Check if this user token has expired
                if (IsUserAccessTokenExpired())
                {
                    //Set schedular state that it reflects the user needs to authorize again
                    SetSchedulerStateDisabled(true);
                }
                else
                {
                    SetSchedulerStateEnabled();
                }
            }
            else
            {
                SetSchedulerStateDisabled(false);
            }
        }

        void AuthenticationBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            FacebookOAuthResult oauthResult;
            if (!_fb.TryParseOAuthCallbackUrl(e.Uri, out oauthResult))
            {
                return;
            }

            if (oauthResult.IsSuccess)
            {
                var accessToken = oauthResult.AccessToken;
                LoginSucceded(accessToken);
            }
            else
            {
                // user cancelled
                MessageBox.Show(oauthResult.ErrorDescription);
            }
        }

        private void LoginSucceded(string accessToken)
        {
            var fb = new FacebookClient(accessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();
                var id = (string)result["id"];
                var name = (string)result["name"];
                //At this time the user short life access token is avaialble and we need to 
                //make another call to retrieve long life user access token that will be valid across 60 days
                //using which app will make calls through scheduler

                GetExtendedToken(accessToken, id, name);
            };

            fb.GetAsync("me?fields=id,name");
        }

        private void GetExtendedToken(string accessToken, string id, string name)
        {
            var fb = new FacebookClient(accessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();
                var longLivedAccessToken = (string)result["access_token"];

                AppSettings.StoreSetting("UserName", name);

                AppSettings.StoreSetting("AccessToken", longLivedAccessToken);

                AppSettings.StoreSetting<DateTime>("AccessTokenDate", DateTime.Now);

                Dispatcher.BeginInvoke(() => MessageBox.Show("User access token valid for next 60 days is retrieved. Press OK to continue to configure auto post scheduler.", "Information", MessageBoxButton.OK));

                Dispatcher.BeginInvoke(() => SetSchedulerStateEnabled());

                //Give focus to Configure pivot control now
                Dispatcher.BeginInvoke(() => PanoControl.SelectedIndex = 1);

            };

            string longLivesAccessTokenEndPoint = "https://graph.facebook.com/oauth/access_token?client_id={0}&client_secret={1}&grant_type=fb_exchange_token&fb_exchange_token={2}";
            longLivesAccessTokenEndPoint = string.Format(longLivesAccessTokenEndPoint, AppId, AppSecret, accessToken);

            fb.GetAsync(longLivesAccessTokenEndPoint);

        }

        private Uri GetFacebookLoginUrl(string appId, string extendedPermissions)
        {
            var parameters = new Dictionary<string, object>();
            parameters["client_id"] = appId;
            parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
            parameters["response_type"] = "token";
            parameters["display"] = "touch";
            //parameters["display"] = "popup";
            //parameters["display"] = "wap";

            //We can provdie a a comma-delimited list of permissions however for the purpose of this app we 
            //will only make use of publish_stream
            parameters["scope"] = extendedPermissions;

            return _fb.GetLoginUrl(parameters);
        }

        private void SetSchedulerStateEnabled()
        {
            //Now scheduler can be turned on of off
            ToggleSwitchBackgrondTask.IsEnabled = true;

            TextBlockStatus.Visibility = System.Windows.Visibility.Visible;
            TextBlockStatus.Text = string.Format("Last user access token was retrieved on {0} against user {1} and will expire on {2}.", GetAccessTokenDate(), GetUserName(), GetAccessTokenDate().AddDays(60));
        }

        private void SetSchedulerStateDisabled(bool userTokenExpired)
        {
            ToggleSwitchBackgrondTask.IsEnabled = false;
            TextBlockStatus.Visibility = System.Windows.Visibility.Visible;

            if (userTokenExpired)
            {
                TextBlockStatus.Text = "Last user access token has expired. Please slide left and authorize.";
            }
            else
            {
                TextBlockStatus.Text = "No user is authorized to auto post. Please slide left and authorize.";
            }
        }

        private DateTime GetAccessTokenDate()
        {
            DateTime value = DateTime.Now;
            AppSettings.TryGetSetting<DateTime>("AccessTokenDate", out value);
            return value;
        }

        private string GetUserName()
        {
            string value = string.Empty;
            AppSettings.TryGetSetting<string>("UserName", out value);
            return value;
        }

        private bool IsUserAccessTokenExpired()
        {
            DateTime startDate = GetAccessTokenDate();
            DateTime endDate = DateTime.Now;
            TimeSpan t = endDate.Subtract(startDate);

            //Long Life user access token expires after 60 days
            if (t.Days > 60)
            {
                return true;
            }
            return false;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLoginPage();
        }

        private void LoadLoginPage()
        {
            var loginUrl = GetFacebookLoginUrl(AppId, ExtendedPermissions);
            AuthenticationBrowser.Navigate(loginUrl);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadLoginPage();
        }

        private void ToggleSwitchBackgrondTask_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                //TODO: Part 3
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. We apologize for inconvenience.", "Error", MessageBoxButton.OK);
            }
        }

    }
}