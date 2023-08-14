using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Notifications.Wpf.Core;
using WPFNotification.Model;
using WPFNotification.Services;

namespace SimpleWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiBaseUrl = "https://localhost:7157/api/Login/";

        public MainWindow()
        {
            InitializeComponent();
        }


        public bool isDarkTheme { get; set; }

        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if(isDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark) 
            {
                isDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }

            else
            {
                isDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark); 
            }
                paletteHelper.SetTheme(theme);

        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
                
        }

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {


            string username = txtUserName.Text;
            string password = txtPassword.Password;

            // Prepare the login model or payload to send to the API
            var loginModel = new
            {
                Username = username,
                Password = password
            };

            // Convert the login model to JSON
            string jsonData = JsonConvert.SerializeObject(loginModel);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Set the API endpoint for login
                    string loginEndpoint = ApiBaseUrl + "authenticate";

                    // Prepare the HTTP request content with the JSON data
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Send the POST request to the API
                    HttpResponseMessage response = await client.PostAsync(loginEndpoint, content);

                    // Handle the API response
                    if (response.IsSuccessStatusCode)
                    {
                        // Login successful, handle the response data if needed
                        // For example, you might extract and store an authentication token from the response.
                        string responseData = await response.Content.ReadAsStringAsync();

                        this.Close();

                        HomePage landingPage = new HomePage();
                        landingPage.Show();


                        var notificationManager = new NotificationManager();

                        var notificationContent = new NotificationContent
                        {
                            Title = "Successfully log in",
                            Message = "IKAW AY NAKAPASOK BET KITA!",
                            Type = NotificationType.Success
                        };

                        await notificationManager.ShowAsync(notificationContent);         

                        // Process responseData as needed
                    }
                    else
                    {
                        // Login failed, handle the error response if needed
                        string errorMessage = await response.Content.ReadAsStringAsync();


                        var notificationManager = new NotificationManager();

                        var notificationContent = new NotificationContent
                        {
                            Title = "Failed to log in",
                            Message = "SHUTANG INA MALI UNG CREDENTIALS MO!",
                            Type = NotificationType.Error
                        };

                        await notificationManager.ShowAsync(notificationContent);

                        // Display the error message or take appropriate actions
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the API call
                    // For example, network issues, server not reachable, etc.
                    // Display error messages or take appropriate actions
                    var notificationManager = new NotificationManager();

                    var notificationContent = new NotificationContent
                    {
                        Title = "Warning",
                        Message = "BULBOL KABA!",
                        Type = NotificationType.Information
                    };

                }

            }
        }
    
    }
}
