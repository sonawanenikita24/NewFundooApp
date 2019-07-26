//-----------------------------------------------------------------------
// <copyright file="CollaboratorPage.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotesApp.View.PopUp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FundooNotesApp.Interface;
    using FundooNotesApp.Model;
    using FundooNotesApp.Repository;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollaboratorPage : ContentPage
    {
        public NotesRepository notesRepository = new NotesRepository();
        public string id = null;
        public string value = null;
        public TapGestureRecognizer tapgester = new TapGestureRecognizer();
        public CollaboratorsRepo repo = new CollaboratorsRepo();

        public ObservableCollection<string> source = new ObservableCollection<string>();

        public FirebaseClient firebase = new FirebaseClient("https://user-9206e.firebaseio.com/");

        public CollaboratorPage(string key)
        {
            InitializeComponent();

            txtMail.ItemsSource = source;
            Data();

            this.value = key;

            //// when save button  clicked
            var savenote = new TapGestureRecognizer();
            savenote.Tapped += Savenote_Tapped;
            savebtn.GestureRecognizers.Add(savenote);

            this.tapgesterrec();
        }

        public void tapgesterrec()
        {
            tapgester.Tapped += Tapgester_Tapped;
            exit.GestureRecognizers.Add(tapgester);
        }

        public async void Data()
        {
            var users = await firebase.Child("User").OnceAsync<RegisterUser>();

            string uid = DependencyService.Get<IDatabaseInterface>().GetId();

            foreach (var items in users)
            {
                if (items.ToString() != uid)
                {
                    var email = await firebase.Child("Users").Child(items.Key).Child("User Information").OnceAsync<RegisterUser>();
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.UserName;
                        // var emailId = item.Key;
                        source.Add(emailDetails);
                    }
                }
            }
        }

        private void Tapgester_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("hello", "exit", "ok");
            txtMail.Text = string.Empty;
            Navigation.PushModalAsync(new EditNote(this.value));
        }

        private void Savenote_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("hello", "thisnote", "ok");
        }

        private async void Savebtn_Clicked(object sender, System.EventArgs e)
        {
            var users = await firebase.Child("Users").OnceAsync<RegisterUser>();

            string uid = DependencyService.Get<IDatabaseInterface>().GetId();
           
            foreach (var items in users)
            {
                //var emailsdetails = items.Object.UserName;

                if (items.ToString() != uid)
                {
                    var email = await firebase.Child("Users").Child(items.Key).Child("User Information").OnceAsync<RegisterUser>();
                   
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.UserName;
                        var name = item.Object.Firstname;
                        //// receiver id
                        id = items.Key;

                        if (txtMail.Text == emailDetails)
                        {
                            await repo.AddCollaborator(name, id, this.value);
                        }                     
                    }
                }
            }

            await Navigation.PushModalAsync(new NavigationPage(new EditNote(this.value)));
        }

       /* 
        * for adding collaborator into note
        * IList<Note> notes = new List<Note>();

        public async void GEtData()
        {
           var uid = DependencyService.Get<IDatabaseInterface>().GetId();

            //// for all notes 
            var allnotes = await notesRepository.GetAllUserNotes();
            if(allnotes!=null)
            {
                foreach(var item in allnotes)
                {
                    if(item.Uid==uid)
                    {
                        notes.Add(item);
                    }
                }
            }

            //// for all noteid in collaborator
            var allnotesfromcollaborator = await repo.GetAllcollaborators();
            if(allnotesfromcollaborator!=null)
            {
                foreach(var item in allnotesfromcollaborator)
                {
                    {
                        foreach(var items in notes)
                        {
                            if(item.Noteid==items.Key)
                            {
                                notes.Add(items);
                            }
                        }
                    }
                }
            }

            gridlayer(notes);
        }

        private void gridlayer(IList<Note> notes)
        {
            throw new NotImplementedException();
        }*/
    }
}