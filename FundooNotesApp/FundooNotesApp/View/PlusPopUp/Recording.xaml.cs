//--------------------------------------------------------------------------------------------------------------------
// <copyright file="Recording.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View.PlusPopUp
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// recording class 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Recording : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recording"/> class.
        /// </summary>
        public Recording()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recording"/> class.
        /// </summary>
        /// <param name="noteid">The note id.</param>
        public Recording(string noteid)
        {
            this.InitializeComponent();
        }
    }
}