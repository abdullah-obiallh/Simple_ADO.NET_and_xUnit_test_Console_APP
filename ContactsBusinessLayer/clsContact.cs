using System;
using System.Data;
using System.Net;


namespace ContactsDataAccessLayer {
    public enum enumMode {UnsignedMode=-1, UpdateMode = 1, AddMode = 2 }
    public class clsContact {
        public int ContactID { get; private set; } = -1;
        public enumMode Mode { get; private set; } =enumMode.UnsignedMode;
        public string FirstName { get; set; }
         public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateOfBirth ;
        public int CountryID { get; set; }
        
        public clsContact() {
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.ImagePath = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = 0;
            this.Mode = enumMode.AddMode;
        }
        
        private clsContact(int ContactID,string FirstName,string LastName,string Email,string Phone,string Address
            ,string ImagePath,DateTime DateOfBirth , int CountryID) {

            this.ContactID = ContactID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.Mode = enumMode.UpdateMode;
        }
       

        private bool _AddContact() {
            this.ContactID = ContactsDataAccessLayer.RetriveFromDataBase.InsertContact(this.FirstName, this.LastName,
          this.Address, this.Email, this.Phone, this.DateOfBirth, this.CountryID, this.ImagePath);
            return (this.ContactID !=-1);
        }
        private bool _UpdateContact() {
            return RetriveFromDataBase.UpdateContact(this.ContactID,this.FirstName, this.LastName,
          this.Address, this.Email, this.Phone, this.DateOfBirth, this.CountryID, this.ImagePath);
        }
        public static clsContact FindByID(int ID) {
            string firstName = "", lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            int countryID = -1;

            if (RetriveFromDataBase.GetContactInfoByID(ID, ref firstName, ref lastName, ref address, ref email, ref phone
                , ref imagePath, ref dateOfBirth, ref countryID)) {
                return new clsContact(ID, firstName, lastName, email, phone, address, imagePath, dateOfBirth, countryID);
            }
            else {
                return null;
            }
        }
        public static bool DeleteContactByID(int ID) {
            return RetriveFromDataBase.DeleteContactFromDatabase(ID);
        }
        public static DataTable GetAllContcts() {
            return RetriveFromDataBase.GetAllContact();
        }
        public static bool CheckIfExistContact(int ContactID) {
            return RetriveFromDataBase.IsExistContact(ContactID);
        }
        public bool Save() {
            bool isSaved = false;
            if (Mode != enumMode.UnsignedMode) {

                switch (Mode) {
                    case enumMode.AddMode:
                        if (_AddContact()) {
                            Console.WriteLine($"Change Mode from {this.Mode}to UpdateMode ");

                            this.Mode = enumMode.UpdateMode;
                            return true;
                        }
                        else {
                            Console.WriteLine("Something Occurred Durring ModeSwitch..");
                            return false;
                        }
                    case enumMode.UpdateMode:
                        if (_UpdateContact()) {
                            Console.WriteLine("Updated Successfully: ");
                            return true;
                        }
                        else {
                            Console.WriteLine("Somthing Happen Durring Update...");
                            return false;
                        }
                }



            }
            return isSaved;
        }
       
    }
}
