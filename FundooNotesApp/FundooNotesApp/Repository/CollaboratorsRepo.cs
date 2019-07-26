//--------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsRepo.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FundooNotesApp.Interface;
    using FundooNotesApp.Model;
    using Xamarin.Forms;

    /// <summary>
    /// class contain method for creating new collaborators
    /// </summary>
    public class CollaboratorsRepo
    {
        /// <summary>
        /// The firebase
        /// </summary>
        private FirebaseClient firebase = new FirebaseClient("https://user-9206e.firebaseio.com/");

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
        /// Creates the collaborator.
        /// </summary>
        /// <param name="collaborater">The collaborate.</param>
        /// <returns>added in database</returns>
        public async Task CreateCollaborator(string collaborater)
        {
            var users = await Firebase.Child("Users").OnceAsync<RegisterUser>();
            var userid = DependencyService.Get<IDatabaseInterface>().GetId();
            await this.Firebase.Child("Users").Child(userid).Child("collaborators").PostAsync<CollaboratorMadel>(new CollaboratorMadel
            {
                Email = collaborater
            });
        }

        public async Task AddCollaborator(string username,string receiverid,string noteid)
        {
          //  var users = await Firebase.Child("Users").OnceAsync<RegisterUser>();
         //   var userid = DependencyService.Get<IDatabaseInterface>().GetId();
            await this.Firebase.Child("Collaborators").Child("collaborators").PostAsync<CollaboratorModelClass>(new CollaboratorModelClass
            {
                name=username,
               ReceiverId=receiverid,
               Noteid=noteid
            });
        }


        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="collborateKey">The collaborate key.</param>
        public void DeleteCollaborator(string collborateKey)
        {
            try
            {
                ////Get current user id
                var currentUserId = DependencyService.Get<IDatabaseInterface>().GetId();

                ////delete the data from the database
                this.Firebase.Child("Users").Child(currentUserId).Child("collaborators").Child(collborateKey).DeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Gets the collaborator data.
        /// </summary>
        /// <param name="ckey">The c key.</param>
        /// <returns>return data</returns>
        public async Task<CollaboratorMadel> GetCollaboratorData()
        {
            try
            {
                var userid = DependencyService.Get<IDatabaseInterface>().GetId();

                return await this.Firebase.Child("Collaborators").Child(userid).Child("collaborators").OnceSingleAsync<CollaboratorMadel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>return list</returns>
        public async Task<List<CollaboratorModelClass>> GetAllcollaborators()
        {
            try
            {
                    ////Used to add the User note label to the database with Firebase authentication Id
                    return (await this.Firebase.Child("Collaborators").Child("collaborators").OnceAsync<CollaboratorModelClass>()).Select(item => new CollaboratorModelClass
                    {                        
                        ReceiverId=item.Object.ReceiverId,
                        Noteid=item.Object.Noteid,
                        name=item.Object.name
                    }).ToList();
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Updates the labels to notes.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="keyNote">The key note.</param>
        public async void UpdateCollaboratorToNotes(Note note, string keyNote)
        {
            ////Get current user id
            var currentUserId = DependencyService.Get<IDatabaseInterface>().GetId();

            ////Update note
            await this.Firebase.Child("Users").Child(currentUserId).Child("UserNotes").Child(keyNote).PutAsync(new Note()
            {
                Title = note.Title,
                UserNote = note.UserNote,
                LabelsList = note.LabelsList,                
                NoteType = note.NoteType,
                NoteColor = note.NoteColor,
            });
        }

        /// <summary>
        /// Updates the labels to notes.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="keyNote">The key note.</param>
        public async void DeletecollaboratorsFromNotes(Note note, string keyNote)
        {
            ////Get current user id
            var currentUserId = DependencyService.Get<IDatabaseInterface>().GetId();

            ////Update note
            await this.Firebase.Child("Users").Child(currentUserId).Child("UserNotes").Child(keyNote).PutAsync(new Note()
            {
                Title = note.Title,
                UserNote = note.UserNote,
                NoteType = note.NoteType,
                NoteColor = note.NoteColor
            });
        }
    }
}