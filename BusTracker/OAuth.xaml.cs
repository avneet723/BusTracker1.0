using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using LinqToTwitter;
using Microsoft.Phone.Controls;

namespace BusTracker
{
    public partial class OAuth : PhoneApplicationPage
    {
        PinAuthorizer pinAuth;

        public OAuth()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
            OAuthWebBrowser.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(OAuthWebBrowser_LoadCompleted);
        }

        void OAuthWebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            EnterPinTextBlock.Visibility = Visibility.Visible;
            PinTextBox.IsEnabled = true;
            AuthenticateButton.IsEnabled = true;
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.pinAuth = new PinAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = "sK8vt9R10Dfh9dwY2ieQ",
                    ConsumerSecret = "QZpsi0JH9v3QSbKc7xi4mBRtO4GQHQZ4leWdvt7ZNs"
                },
                UseCompression = true,
                GoToTwitterAuthorization = pageLink =>
                    Dispatcher.BeginInvoke(() => OAuthWebBrowser.Navigate(new Uri(pageLink, UriKind.Absolute)))
            };

            this.pinAuth.BeginAuthorize(resp =>
                Dispatcher.BeginInvoke(() =>
                {
                    switch (resp.Status)
                    {
                        case TwitterErrorStatus.Success:
                            break;
                        case TwitterErrorStatus.TwitterApiError:
                        case TwitterErrorStatus.RequestProcessingException:
                            MessageBox.Show(
                                resp.Exception.ToString(),
                                resp.Message,
                                MessageBoxButton.OK);
                            break;
                    }
                }));
        }

        private void AuthenticateButton_Click(object sender, RoutedEventArgs e)
        {
            pinAuth.CompleteAuthorize(
                PinTextBox.Text,
                completeResp => Dispatcher.BeginInvoke(() =>
                {
                    switch (completeResp.Status)
                    {
                        case TwitterErrorStatus.Success:
                            SharedState.Authorizer = pinAuth;
                            NavigationService.GoBack();
                            break;
                        case TwitterErrorStatus.TwitterApiError:
                        case TwitterErrorStatus.RequestProcessingException:
                            MessageBox.Show(
                                completeResp.Exception.ToString(),
                                completeResp.Message,
                                MessageBoxButton.OK);
                            break;
                    }
                }));
        }
    }
}