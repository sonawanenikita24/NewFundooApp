﻿//--------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteAndRestorePopUpPage.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace FundooNotesApp.View.DeletePopUp
{
    using System;
    using System.Collections.Generic;
    using FundooNotesApp.Model;
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Delete and restore menu page
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteAndRestorePopUpPage : PopupPage
    {
        /// <summary>
        /// The page items
        /// </summary>
        private List<MenuPageItems> pageItems;

        /// <summary>
        /// The note id
        /// </summary>
        private string noteid;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAndRestorePopUpPage"/> class.
        /// </summary>
        /// <param name="notekey">The note key.</param>
        public DeleteAndRestorePopUpPage(string notekey)
        {
            this.Noteid = notekey;
            this.InitializeComponent();
            TrashPopMenuList.ItemsSource = this.GetMenuList();
        }

        /// <summary>
        /// Gets or sets the note id.
        /// </summary>
        /// <value>
        /// The note id.
        /// </value>
        public string Noteid
        {
            get
            {
                return this.noteid;
            }

            set
            {
                this.noteid = value;
            }
        }

        /// <summary>
        /// Gets or sets the page items.
        /// </summary>
        /// <value>
        /// The page items.
        /// </value>
        public List<MenuPageItems> PageItems
        {
            get
            {
                return this.pageItems;
            }

            set
            {
                this.pageItems = value;
            }
        }

        /// <summary>
        /// Get the menu list.
        /// </summary>
        /// <returns>return menu list</returns>
        public List<MenuPageItems> GetMenuList()
        {
            this.PageItems = new List<MenuPageItems>();
            try
            {
                this.PageItems.Add(new MenuPageItems()
                {
                    Icon = "Restore.png",
                    MenuItem = "Restore",
                    TargetType = typeof(RestorePage)
                });

                this.PageItems.Add(new MenuPageItems()
                {
                    Icon = "DeleteCross.png",
                    MenuItem = "Delete Forever",
                    TargetType = typeof(DeleteForeeverPage)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //// return menulist
            return this.PageItems;
        }

        /// <summary>
        /// Menus the selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void MenuSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selectedmenu = (MenuPageItems)e.SelectedItem;
                string menu = selectedmenu.MenuItem;
                if (menu == "Restore")
                {
                    Navigation.PushModalAsync(new RestorePage(this.Noteid));
                    IsVisible = false;
                }

                if (menu == "Delete Forever")
                {
                    Navigation.PushModalAsync(new DeleteForeeverPage(this.Noteid));
                    IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}