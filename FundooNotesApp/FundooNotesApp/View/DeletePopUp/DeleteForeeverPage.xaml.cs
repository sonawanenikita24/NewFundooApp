﻿//--------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteForeeverPage.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using System;
    using FundooNotesApp.Interface;
    using FundooNotesApp.Repository;
    using Plugin.Toast;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Delete forever page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteForeeverPage : ContentPage
    {
        /// <summary>
        /// The note key
        /// </summary>
        private string notekey;

        /// <summary>
        /// The helper is instance of firebase helper class to access database
        /// </summary>
        private NotesRepository firebase = new NotesRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteForeeverPage"/> class.
        /// </summary>
        /// <param name="notekey">The note key.</param>
        public DeleteForeeverPage(string notekey)
        {
            this.Notekey = notekey;
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
        /// Gets or sets the firebase.
        /// </summary>
        /// <value>
        /// The firebase.
        /// </value>
        public NotesRepository Firebase
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
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var uid = DependencyService.Get<IDatabaseInterface>().GetId();
                await this.Firebase.DeleteNote(this.notekey, uid);
                CrossToastPopUp.Current.ShowToastMessage("Note is deleted");
                await Navigation.PushModalAsync(new Dashboard());
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
           }
        }       
    }
}