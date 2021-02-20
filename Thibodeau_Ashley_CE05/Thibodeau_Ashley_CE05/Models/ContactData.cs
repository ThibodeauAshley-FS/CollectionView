/*
    Ashley Thibodeau
    Interface Programming
    Code Exercise 05
    C20210201

    GitHub Link: https://github.com/InterfaceProgramming/ce5-ThibodeauAshley-FS
 */
using System;
using System.Collections.Generic;

namespace Thibodeau_Ashley_CE05.Models
{
    public class ContactData
    {
        public string Filename { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhotoURL { get; set; }
    }

    public class Contacts
    {
        public static List<ContactData> Get()
        {
            return new List<ContactData>();
        }
    }
}
