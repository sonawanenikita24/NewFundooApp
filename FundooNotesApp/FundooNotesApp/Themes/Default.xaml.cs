//--------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.Themes
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Default theme for page 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ResourceDictionary" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Default : ResourceDictionary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Default"/> class.
        /// </summary>
        public Default()
        {
            InitializeComponent();
        }
    }
}