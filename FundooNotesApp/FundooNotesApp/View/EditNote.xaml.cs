//--------------------------------------------------------------------------------------------------------------------
// <copyright file="EditNote.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FundooNotesApp.Interface;
    using FundooNotesApp.Model;    
    using FundooNotesApp.Repository;
    using FundooNotesApp.View.PlusPopUp;
    using FundooNotesApp.View.PopUp;
    using FundooNotesApp.ViewModels;
    using Plugin.Toast;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static FundooNotesApp.Model.TypeOfNote;

    /// <summary>
    /// Edit our note
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNote : ContentPage
    {
        /// <summary>
        /// The helper
        /// </summary>
        private NotesRepository firebaseHelperVar = new NotesRepository();

        /// <summary>
        /// Gets or sets the color notes.
        /// </summary>
        /// <value>
        /// The color notes.
        /// </value>
        public Color ColorNotes { get; set; }        

        /// <summary>
        /// The current note type
        /// </summary>
        private NoteType currentNotetype;

        /// <summary>
        /// The list
        /// </summary>
        private IList<string> list = new List<string>();

        /// <summary>
        /// The list
        /// </summary>
        private IList<string> list1 = new List<string>();

        /// <summary>
        /// The note key
        /// </summary>
        private string noteKey = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditNote"/> class.
        /// </summary>
        /// <param name="idValue">The identifier value.</param>
        public EditNote(string idValue)
        {
            this.NoteKey = idValue;
            this.EditNoteData();
            this.InitializeComponent();
        }
            
        /// <summary>
        /// Gets or sets the firebase helper variable.
        /// </summary>
        /// <value>
        /// The firebase helper variable.
        /// </value>
        public NotesRepository FirebaseHelperVar
        {
            get
            {
                return this.firebaseHelperVar;
            }

            set
            {
                this.firebaseHelperVar = value;
            }
        }

        /// <summary>
        /// Gets or sets the current note type.
        /// </summary>
        /// <value>
        /// The current note type.
        /// </value>
        public NoteType CurrentNotetype
        {
            get
            {
                return this.currentNotetype;
            }

            set
            {
                this.currentNotetype = value;
            }
        }

        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        /// <value>
        /// The list.
        /// </value>
        public IList<string> Lists
        {
            get
            {
                return this.list;
            }

            set
            {
                this.list = value;
            }
        }

        /// <summary>
        /// Gets or sets the note key.
        /// </summary>
        /// <value>
        /// The note key.
        /// </value>
        public string NoteKey
        {
            get
            {
                return this.noteKey;
            }

            set
            {
                this.noteKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the list1.
        /// </summary>
        /// <value>
        /// The list1.
        /// </value>
        public IList<string> List1
        {
            get
            {
                return this.list1;
            }

            set
            {
                this.list1 = value;
            }
        }

        /// <summary>
        /// Edit the note data.
        /// </summary>
        public async void EditNoteData()
        {
            try
            {
                var uid = DependencyService.Get<IDatabaseInterface>().GetId();
                ////Get note data of specified key
                var note = await this.FirebaseHelperVar.GetUserNote(this.noteKey);
                
                TitleText.Text = note.Title;
                NoteText.Text = note.UserNote;

                var placemarks = await Geocoding.GetPlacemarksAsync(note.NoteLocation.Latitude, note.NoteLocation.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                Location.Text = "Note created at " + placemark.AdminArea;

                list = note.LabelsList;
                //labelsList.ItemsSource = lists;
                this.CurrentNotetype = note.NoteType;
                this.BackgroundColor = Color.FromHex(FrameColorSetter.GetHexColor(note));
                ToolbarItems.Clear();

                if (note.NoteType == NoteType.isNote)
                {
                    ToolbarItems.Add(this.PinnedNote);
                    ToolbarItems.Add(this.ReminderNote);
                    ToolbarItems.Add(this.ArchiveNote);
                }
                else if (note.NoteType == NoteType.isArchive)
                {
                    ToolbarItems.Add(this.PinnedNote);
                    ToolbarItems.Add(this.ReminderNote);
                    ToolbarItems.Add(this.UnArchiveNote);
                }
                else if (note.NoteType == NoteType.ispin)
                {
                    ToolbarItems.Add(this.UnPinNote);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// add labels inside the frame 
        /// </summary>
        /// <param name="list">The list.</param>
        public async void LableFrames(IList<string> list)
        {
            var userid = DependencyService.Get<IDatabaseInterface>().GetId();
            LabelRepository firebasedata = new LabelRepository();
            var alllabels = await firebasedata.GetAllLabels();

            ////display labels
            foreach (NoteLabel model in alllabels)
            {
                foreach (var labelId in list)
                {
                    if (model.Labelkey.Equals(labelId))
                    {
                        ////add label name to Label text
                        var labelName = new Label
                        {
                            Text = model.Noteslabel,
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.Start,
                            FontSize = 11
                        };

                        ////add new label frame inside the note
                        var labelFrame = new Frame
                        {
                            CornerRadius = 28,
                            HeightRequest = 14,
                            WidthRequest = 14,
                            Content = labelName,
                            BorderColor = Color.Gray,
                            BackgroundColor = Color.Aqua
                        };
                       // Note note = await this.FirebaseHelperVar.GetNoteByNoteId(NoteKey, userid);

                        //BackgroundColor = note.NoteColor;
                        layout.Children.Add(labelFrame);

                        var labelimage = new TapGestureRecognizer();
                        labelimage.Tapped += TapLabel_tapped;
                        labelFrame.GestureRecognizers.Add(labelimage);
                    }
                }
            }
        }

       /// <summary>
        /// add labels inside the frame 
        /// </summary>
        /// <param name="list">The list.</param>
        public async void CollaboratorsFrame(string noteid)
        {
            var userid = DependencyService.Get<IDatabaseInterface>().GetId();
            CollaboratorsRepo firebasedata = new CollaboratorsRepo();
            var allData = await firebasedata.GetAllcollaborators();

            ////display labels
          foreach (CollaboratorModelClass model in allData)
            {      
                        if (model.Noteid.Equals(noteid))
                        {
                                var labelname = new Label
                                {
                                    Text = model.name,
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Start,
                                    FontSize = 11,
                                    TextColor=Color.Black
                                };
                            var imagebutton = new ImageButton()
                            {
                                Source = "Accountphoto.png",
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.Start
                            };

                            ////add new label frame inside the note
                            var labelFrame = new Frame
                            {
                                CornerRadius = 28,
                                HeightRequest = 14,
                                Content = labelname,
                                BorderColor = Color.Gray,
                                WidthRequest=5,
                                BackgroundColor = Color.Aqua
                            };

                            layout1.Children.Add(labelFrame);

                            var tapimage = new TapGestureRecognizer();

                            tapimage.Tapped += Tapimage_Tapped;

                            imagebutton.GestureRecognizers.Add(tapimage);
                        }
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
            var userid = DependencyService.Get<IDatabaseInterface>().GetId();
          
            var note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
          
            var list = note.LabelsList;
            this.LableFrames(list);
           // var lists = note.CollaboratosList;
            this.CollaboratorsFrame(this.NoteKey);
          
        }

        /// <summary>
        /// When overridden, allows the application developer to customize behavior as the <see cref="T:Xamarin.Forms.Page" /> disappears.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
         protected async override void OnDisappearing()
          {           
              try
              {
                var uid = DependencyService.Get<IDatabaseInterface>().GetId();
                  //// create note instance with updated value
                Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);

                Note notes = new Note()
                {
                    Title = TitleText.Text,
                    UserNote = NoteText.Text,
                    NoteType = note.NoteType
                };

                //// calling updated function to update edited note in database
                await this.FirebaseHelperVar.UpdateUserNote(notes, this.NoteKey);
               // await this.FirebaseHelperVar.UpdateUserNoteByUserId(notes, NoteKey);
             //  await this.FirebaseHelperVar.UpdateUserNote(notes) 
                  //// go back to note page
                  base.OnDisappearing();    
              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex.Message);
              }
          }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// return to main page
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                var uid = DependencyService.Get<IDatabaseInterface>().GetId();

                Note note = new Note()
                {
                    Title = TitleText.Text,
                    UserNote = NoteText.Text,                   
                    LabelsList = Lists                    
                };

                var result = this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
                return base.OnBackButtonPressed();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Called when [pin note clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnPinNoteClicked(object sender, EventArgs e)
        {
            try
            {
                Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
                note.NoteType = NoteType.ispin;
                this.CurrentNotetype = note.NoteType;
                await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
                await this.DisplayAlert("success", "Note is pinned", "ok");
                await Navigation.PushAsync(new NotesPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called when [reminder note changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnReminderNoteChanged(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new RemindersPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called when [archive note changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnArchiveNoteChanged(object sender, EventArgs e)
        {
            try
            {
                Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
                note.NoteType = NoteType.isArchive;
                this.CurrentNotetype = note.NoteType;
                await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
                await this.DisplayAlert("success", "Note is Archived", "ok");
                await Navigation.PushAsync(new NotesPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called when [un archived note changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnUnArchivedNoteChanged(object sender, EventArgs e)
        {
            try
            {
                Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
                note.NoteType = NoteType.isNote;
                this.CurrentNotetype = NoteType.isNote;
                await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
                CrossToastPopUp.Current.ShowToastMessage("Note is UnArchived");
                await Navigation.PushModalAsync(new NotesPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called when /[unpinned note].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnUnpinnedNote(object sender, EventArgs e)
        {
            try
            {
                Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
                note.NoteType = NoteType.isNote;
                this.CurrentNotetype = note.NoteType;
                await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
                await this.DisplayAlert("success", "Note is unpin ", "ok");
                ////  await Navigation.PushAsync(new NotePage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called when /[pop menu button clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnPopMenuButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PopUpPage(this.NoteKey));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// ListViews the clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ListViewClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PlusPopUpPage(this.NoteKey));
        }

        /// <summary>
        /// Handles the tapped event of the TapLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TapLabel_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeleteCollaboratorPage(this.NoteKey));
        }

        /// <summary>
        /// Handles the Tapped event of the Tap image control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Tapimage_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeleteCollaboratorPage(this.NoteKey));
        }

        /// <summary>
        /// Handles the Clicked event of the Aqua control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Aque_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Aqua;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }
            /// <summary>
            /// Handles the Clicked event of the DarkGoldenrod control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            private async void DarkGoldenrod_Clicked(object sender, EventArgs e)
        {            
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.DarkGoldenrod;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Green control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Green_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Green;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Gold control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Gold_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Gold;
            note.NoteColor = this.BackgroundColor;            
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the GreenYellow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void GreenYellow_Clicked(object sender, EventArgs e)
        {            
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.GreenYellow;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Gray control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Gray_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Gray;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Lavender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Lavender_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Lavender;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Red control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Red_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Red;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Yellow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Yellow_Clicked(object sender, EventArgs e)
        {
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Yellow;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Orange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void  Orange_Clicked(object sender, EventArgs e)
        {            
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Orange;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Teal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Teal_Clicked(object sender, EventArgs e)
        {            
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Teal;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Brown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Brown_Clicked(object sender, EventArgs e)
        {            
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Brown;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }

        /// <summary>
        /// Handles the Clicked event of the Purple control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Purple_Clicked(object sender, EventArgs e)
        {            
            Note note = await this.FirebaseHelperVar.GetUserNote(this.NoteKey);
            this.BackgroundColor = Color.Purple;
            note.NoteColor = this.BackgroundColor;
            await this.FirebaseHelperVar.UpdateUserNote(note, this.NoteKey);
        }
    }
}