//--------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using System;
    using FundooNotesApp.Interface;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// login page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            this.InitializeComponent();
            themePicker.SelectedIndexChanged += ThemePicker_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ThemePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThemeManager.ChangeTheme(themePicker.SelectedItem.ToString());
        }

        /// <summary>
        /// Handles the clicked event of the Logout_button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Logout_button_clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IDatabaseInterface>().LogoutWithFirebaseAuth();
            this.DisplayAlert("Success", "Logout succeseefully", "ok");
            Navigation.PushModalAsync(new LoginPage());
        }

        /// <summary>
        /// Handles the Clicked event of the Reset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Reset_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ResetPassword());
        }
    }
}