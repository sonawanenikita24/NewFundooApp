//--------------------------------------------------------------------------------------------------------------------
// <copyright file="PieChartExample.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using FundooNotesApp.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// pie chart graph
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PieChartExample : ContentPage
    {
        /// <summary>
        /// The view model is instance of pie chart view model
        /// </summary>
        PieChartViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="PieChartExample"/> class.
        /// </summary>
        public PieChartExample()
        {
            InitializeComponent();
            vm = new PieChartViewModel();
            this.BindingContext = vm;
        }
    }
}