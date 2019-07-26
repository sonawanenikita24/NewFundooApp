//--------------------------------------------------------------------------------------------------------------------
// <copyright file="Drawaing.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View.PlusPopUp
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// drawing something in text area of note
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Drawaing : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Drawaing"/> class.
        /// </summary>
        public Drawaing()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Drawaing"/> class.
        /// </summary>
        /// <param name="noteid">The note id.</param>
        public Drawaing(string noteid)
        {
            this.InitializeComponent();
        }
    }
}