/*
    Ashley Thibodeau
    Interface Programming
    Code Exercise 05
    C20210201

    GitHub Link: https://github.com/InterfaceProgramming/ce5-ThibodeauAshley-FS
 */
using System;
using System.Collections.Generic;
using Thibodeau_Ashley_CE05.Models;
using System.IO;

using Xamarin.Forms;

namespace Thibodeau_Ashley_CE05
{
    public partial class AddContactPage : ContentPage
    {
        public AddContactPage()
        {
            InitializeComponent();
            btnSave.Clicked += BtnSave_Clicked;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            string filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CE05.txt");
            string messageType = "New";

            File.WriteAllText(filename, $"{nameEntry.Text},{phoneEntry.Text},{emailEntry.Text}");
            MessagingCenter.Send<String>(messageType, "ModifiedMessage");
            Navigation.PopAsync();

        }
    }
}
