//--------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryNote.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Category note into pie chart or bar graph
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryNote : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryNote"/> class.
        /// </summary>
        public CategoryNote()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the clicked event of the Bar chart Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BarchartButton_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingPage());
        }

        /// <summary>
        /// Handles the Clicked event of the Pie Chart Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PieChartButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PieChartExample());
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("Alert", "Please logout first from app", "Ok");

            ////go to Edit page with that note id
            Navigation.PushAsync(new LogoutPage());
            return base.OnBackButtonPressed();
        }
    }
}