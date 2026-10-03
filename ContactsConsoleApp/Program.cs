using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBusinessLayer;
using ContactsDataAccessLayer;


namespace ContactsConsoleApp {
    public class Program {

        public static void TestFindByIDFuncion(int ID) {

            clsContact contact;
            contact = clsContact.FindByID(ID);
            if (contact != null) { 
            Console.WriteLine("ID: "+contact.ContactID);
            Console.WriteLine("First Name :"+contact.FirstName);
            Console.WriteLine("Last Name :"+contact.LastName);
            Console.WriteLine("Email : "+contact.Email);
            Console.WriteLine("Phone : "+contact.Phone);
            Console.WriteLine("Address : "+contact.Address);
            Console.WriteLine("Date Of Birth :"+contact.DateOfBirth);
            Console.WriteLine("Country ID:"+contact.CountryID);
            }
            else {
                Console.WriteLine("id is not founded:...");
            }
        }
        public static void TestUpdate(int ID) {
            clsContact contact = clsContact.FindByID(ID); ;
            if (contact != null) {
                contact.FirstName = "faris";
                contact.LastName = "fahad";
                contact.Email = "2222@test.com";
                contact.Phone = "222222222";
                contact.Address = "Riyadah";
                contact.DateOfBirth = DateTime.Now;
                contact.ImagePath = "afafaf";
                contact.CountryID = 1;
                contact.Save();
            }
            else {
                Console.WriteLine("wrong ID:...");
            }
            TestFindByIDFuncion(ID);

        }
        public static void TestAddContact() {
            clsContact contact = new clsContact();
            contact.FirstName = "fahad";
            contact.LastName = "Salman";
            contact.Email = "adfsaf@gmail.com";
            contact.Phone = "125125125";
            contact.Address = "Hail Saud iarabia";
            contact.DateOfBirth = DateTime.Now;
            contact.ImagePath = "sdgdgsdgssddgsdgsdgsdgsdtgsd";
            contact.CountryID = 1;

            if (contact.Save()) {
                Console.WriteLine("\n Save Successfully: "+contact.CountryID);
            }
        }
        public static void TestDelete(int ID) {
            if (clsContact.CheckIfExistContact(ID)) {
                if (clsContact.DeleteContactByID(ID)) {
                    Console.WriteLine("Delete Successfully: " + ID);
                }
                else {
                    Console.WriteLine("Can't Be Deleted");
                }
            }
            else {
                Console.WriteLine($"There's No Contact Has That ID {ID}");
            }
        }
        public static void TestGetAllContact() {
            DataTable  dt= new DataTable();

            dt = clsContact.GetAllContcts();
            foreach(DataRow row in dt.Rows){
                Console.WriteLine($"ID :{row["ContactID"]} , Name : {row["FirstName"]} {row["LastName"]} ,Phone number :{row["Phone"]}");
            }
        }
        public static void TestIfExistOrNO(int ID) {
            if (clsContact.CheckIfExistContact(ID)) {
                Console.WriteLine($"The Contact ID({ID}) Is On The System");
            }
            else {
                Console.WriteLine($"There's no Contact Has({ID}) ID on the System");
            }
        }
        //--------------------------------------Country Section---------------------------------------------------
        public static void TestFoundCountryByName(string countryName) {
            clsCountries.FindCountryByName(countryName);

        }
        static void Main(string[] args) {


            TestFoundCountryByName("United State");
            TestFoundCountryByName("United Kingdom");
            TestFoundCountryByName("Hail");


        }
    }
}
