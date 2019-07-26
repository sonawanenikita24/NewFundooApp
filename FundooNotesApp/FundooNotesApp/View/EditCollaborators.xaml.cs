//--------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCollaborators.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using System;
    using System.Collections.ObjectModel;
    using Firebase.Database;
    using FundooNotesApp.Model;
    using FundooNotesApp.Repository;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// class for displaying all collaborator note
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCollaborators : ContentPage
    {
        /// <summary>
        /// The notes repository
        /// </summary>
        private NotesRepository notesRepository = new NotesRepository();

        /// <summary>
        /// The repo
        /// </summary>
        private CollaboratorsRepo repo = new CollaboratorsRepo();

        /// <summary>
        /// The value
        /// </summary>
        private string value = null;
        
        /// <summary>
        /// The firebase is instance of firebase client  to store path of database
        /// </summary>
      private FirebaseClient firebase = new FirebaseClient("https://user-9206e.firebaseio.com/");

        /// <summary>
        /// Gets or sets the notes repository.
        /// </summary>
        /// <value>
        /// The notes repository.
        /// </value>
        public NotesRepository NotesRepository
        {
            get
            {
                return this.notesRepository;
            }

            set
            {
                this.notesRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the firebase.
        /// </summary>
        /// <value>
        /// The firebase.
        /// </value>
        public FirebaseClient Firebase
        {
            get
            {
                return this.firebase;
            }

            set
            {
                this.firebase = value;
            }
        }

        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        public CollaboratorsRepo Repo
        {
            get
            {
                return this.repo;
            }

            set
            {
                this.repo = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCollaborators"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public EditCollaborators(string key)
        {
            this.Value = key;
            InitializeComponent();
        }

        /// <summary>
        /// Get all the user.
        /// </summary>
        public async void GetallUser()
        {
            var users = await Firebase.Child("Users").OnceAsync<RegisterUser>();
            lstLabels.ItemsSource = users;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var alllabels = await this.Repo.GetAllcollaborators();
            lstLabels.ItemsSource = alllabels;
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// return back to previous page
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Handles the Clicked event of the Save button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Savebtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditNote(this.Value));
        }

        /// <summary>
        /// Handles the clicked event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CancelButton_clicked(object sender, EventArgs e)
        {
            //// some code here
        }      
    }
}