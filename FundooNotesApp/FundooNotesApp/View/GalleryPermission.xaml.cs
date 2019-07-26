//--------------------------------------------------------------------------------------------------------------------
// <copyright file="GalleryPermission.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View
{
    using Firebase.Storage;
    using FundooNotesApp.Repository;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using FundooNotesApp.Helper;

    /// <summary>
    /// contain method for giving permission for camera and method for taking photo 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPermission : ContentPage
    {
        /// <summary>
        /// The helpers is instance of firebase helper class
        /// </summary>
        FirebaseHelper helpers = new FirebaseHelper();

        /// <summary>
        /// The user repository
        /// </summary>
        UserRepository userRepository = new UserRepository();

        /// <summary>
        /// The file
        /// </summary>
        MediaFile file;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryPermission"/> class.
        /// </summary>
        public GalleryPermission()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the button upload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            var storage = await helpers.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
            string imageurl = storage;
            await userRepository.GetimageSouce(imageurl);
        }

        /// <summary>
        /// Handles the Clicked event of the pink button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }

                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}