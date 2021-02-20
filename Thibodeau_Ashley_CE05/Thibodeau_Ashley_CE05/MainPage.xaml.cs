/*
    Ashley Thibodeau
    Interface Programming
    Code Exercise 05
    C20210201

    GitHub Link: https://github.com/InterfaceProgramming/ce5-ThibodeauAshley-FS
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Collections.ObjectModel;
using Thibodeau_Ashley_CE05.Models;

namespace Thibodeau_Ashley_CE05
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<ContactData> AllContacts;
        private ToolbarItem deleteItem;
        private ToolbarItem addContact;

        public MainPage()
        {
            InitializeComponent();
            this.AllContacts = new ObservableCollection<ContactData>(Contacts.Get());

            collectionView.SelectionMode = SelectionMode.Multiple;

            this.deleteItem = new ToolbarItem
            {
                Text = "Delete Selection",
                IconImageSource = ImageSource.FromFile("delete.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
            };

            this.addContact = new ToolbarItem
            {
                Text = "Add New Contact",
                IconImageSource = ImageSource.FromFile("add.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
            };

            this.ToolbarItems.Add(addContact);

            addContact.Clicked += AddContact_Clicked;

            deleteItem.Command = new Command((sender) =>
            {
                this.DeleteCommand();
            });

            collectionView.SelectionChanged += CollectionView_SelectionChanged;

            MessagingCenter.Subscribe<String>(this, "ModifiedMessage", (sender) =>
            {
                this.ReloadListData();
            });

            this.ReloadListData();


        }

        private void ReloadListData()
        {
            AllContacts.Clear();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.CE05.txt");
            foreach (var filename in files)
            {

                string strData = File.ReadAllText(filename);
                string[] data = strData.Split(',');


                    string name = data[0];
                    string phone = data[1];
                    string email = data[2];

                    AllContacts.Add(new ContactData
                    {
                        Filename = filename,
                        FullName = name,
                        Phone = phone,
                        Email = email,
                        PhotoURL = "profile.png"
                       
                    });
                

            }


        }

        async private void AddContact_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddContactPage
            {
                BindingContext = new ContactData()
            });
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool deleteAlreadyAdded = this.ToolbarItems.Contains(this.deleteItem);

            if (e.CurrentSelection.Count > 0)
            {
                if (!deleteAlreadyAdded)
                {
                    this.ToolbarItems.Add(this.deleteItem);
                }
            }
            else
            {
                if (deleteAlreadyAdded)
                {
                    this.ToolbarItems.Remove(this.deleteItem);
                }
            }
        }

        async private void DeleteCommand()
        {
            bool answer = await DisplayAlert("Confirm Delete", "The action you are trying to take cannot be reversed. Are you sure you would like to delete all your saved data?", "Yes", "No");

            if(answer)
            {
                foreach (ContactData contact in collectionView.SelectedItems)
                {
                    File.Delete(contact.Filename);
                    AllContacts.Remove(contact);

                }
                this.ToolbarItems.Remove(this.deleteItem);
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = AllContacts;

        }
    }
}
