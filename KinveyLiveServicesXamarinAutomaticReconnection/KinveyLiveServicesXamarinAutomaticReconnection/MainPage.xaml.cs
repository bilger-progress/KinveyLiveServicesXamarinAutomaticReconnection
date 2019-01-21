using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Kinvey;

namespace KinveyLiveServicesXamarinAutomaticReconnection
{
    public partial class MainPage : ContentPage
    {
        private readonly Client KinveyClient;
        private string appKey = "xxx";
        private string appSecret = "xxx";
        private string userName = "xxx";
        private string userPassword = "xxx";

        public MainPage()
        {
            InitializeComponent();
            Client.Builder Builder = new Client.Builder(this.appKey, this.appSecret);
            this.KinveyClient = Builder.Build();
            this.KinveyPing();
            this.ProceedKinveyLiveServices();
        }

        private async void KinveyPing()
        {
            try
            {
                PingResponse PingResponse = await this.KinveyClient.PingAsync();
                Console.WriteLine("Kinvey Ping Response: " + PingResponse.kinvey);
            }
            catch (Exception PingException)
            {
                Console.WriteLine("Kinvey Ping Exception: " + PingException.Message);
            }
        }

        private async void ProceedKinveyLiveServices()
        {
            if (!Client.SharedClient.IsUserLoggedIn())
            {
                await User.LoginAsync(this.userName, this.userPassword);
            }

            await Client.SharedClient.ActiveUser.RegisterRealtimeAsync();
            DataStore<Book> Books = DataStore<Book>.Collection("Books");
            await Books.Subscribe(new KinveyDataStoreDelegate<Book>
            {
                OnNext = (result) => {
                    Console.WriteLine("KLS Book title: " + result.Title);
                },
                OnStatus = (status) => {
                    Console.WriteLine("KLS Subscription Status Change: " + status.Message);
                },
                OnError = (error) => {
                    Console.WriteLine("KLS Error: " + error.Message);
                }
            });
        }
    }
}
