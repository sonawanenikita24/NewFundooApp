//--------------------------------------------------------------------------------------------------------------------
// <copyright file="LogoutPage.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using FundooNotesApp.Interface;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// logout from application
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutPage"/> class.
        /// </summary>
        public LogoutPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        /// <summary>
        /// Handles the Clicked event of the Logout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IDatabaseInterface>().LogoutWithFirebaseAuth();
            Navigation.PushModalAsync(new LoginPage());
        }
    }
}