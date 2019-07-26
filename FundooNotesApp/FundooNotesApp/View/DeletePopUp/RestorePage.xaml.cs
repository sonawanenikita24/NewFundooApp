//--------------------------------------------------------------------------------------------------------------------
// <copyright file="RestorePage.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------    
namespace FundooNotesApp.View
{ 
    using FundooNotesApp.Interface;
    using FundooNotesApp.Model;
    using FundooNotesApp.Repository;
    using Plugin.Toast;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static FundooNotesApp.Model.TypeOfNote;

    /// <summary>
    /// restore page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestorePage : ContentPage
    {
        /// <summary>
        /// The note key
        /// </summary>
        private string notekey;

        /// <summary>
        /// The firebase helper
        /// </summary>
        private NotesRepository firebasehelper = new NotesRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="RestorePage"/> class.
        /// </summary>
        /// <param name="noteid">The note id.</param>
        public RestorePage(string noteid)
        {
            this.Notekey = noteid;
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the note key.
        /// </summary>
        /// <value>
        /// The note key.
        /// </value>
        public string Notekey
        {
            get
            {
                return this.notekey;
            }

            set
            {
                this.notekey = value;
            }
        }

        /// <summary>
        /// Gets or sets the firebase helper.
        /// </summary>
        /// <value>
        /// The firebase helper.
        /// </value>
        public NotesRepository Firebasehelper
        {
            get
            {
                return this.firebasehelper;
            }

            set
            {
                this.firebasehelper = value;
            }
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
       /* protected async override void OnAppearing()
        {
            base.OnAppearing();
            string userid = DependencyService.Get<IDatabaseInterface>().GetId();

            //// get user note
            Note notes = await this.Firebasehelper.GetUserNote(this.Notekey);

            //// get user note by giving note id
            Note note = await this.Firebasehelper.GetNoteByNoteId(this.Notekey, userid);

            //// checking for note type
            note.NoteType = NoteType.isNote;

            //// update in firebase
            await this.Firebasehelper.UpdateUserNote(note, this.Notekey);            

            //// display message
            CrossToastPopUp.Current.ShowToastMessage("Note is restored");

            //// navigate to another page
            await Navigation.PushModalAsync(new Dashboard());

            base.OnDisappearing();
        }*/
    }
}