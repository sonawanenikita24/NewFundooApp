//--------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteCollaboratorPage.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View.PopUp
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// class contain method deleting particular collaborated person 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteCollaboratorPage : ContentPage
    {
        /// <summary>
        /// The value
        /// </summary>
        string value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCollaboratorPage"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public DeleteCollaboratorPage(string key)
        {
            InitializeComponent();
            this.value = key;
        }
    }
}