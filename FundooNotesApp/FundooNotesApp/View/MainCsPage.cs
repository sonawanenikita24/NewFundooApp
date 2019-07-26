//--------------------------------------------------------------------------------------------------------------------
// <copyright file="MainCsPage.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using System;
    using Xamarin.Forms;

    /// <summary>
    /// Main page in google
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public class MainCsPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainCsPage"/> class.
        /// </summary>
        public MainCsPage()
        {
            this.Title = "Google Login";
            this.BackgroundColor = Color.White;

            var loginButton = new Button
            {
                Text = "Login with Google",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#db4437"),
#pragma warning disable CS0618 // Type or member is obsolete
                Font = Font.BoldSystemFontOfSize(26),
#pragma warning restore CS0618 // Type or member is obsolete
                FontSize = 26
            };

            loginButton.Clicked += this.LoginWithFacebook_Clicked;

            this.Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label
                    {
                        Text = "Login with Google API",
                        FontSize = 66,
                        TextColor = Color.Black
                    },
                    loginButton
                }
            };
        }

        /// <summary>
        /// Handles the Clicked event of the LoginWithFacebook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoogleProfilePage());
        }
    }
}