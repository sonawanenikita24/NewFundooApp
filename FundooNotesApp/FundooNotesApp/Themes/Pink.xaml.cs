//--------------------------------------------------------------------------------------------------------------------
// <copyright file="Pink.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.Themes
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// change theme to pink color
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ResourceDictionary" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pink : ResourceDictionary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pink"/> class.
        /// </summary>
        public Pink()
        {
            InitializeComponent();
        }
    }
}