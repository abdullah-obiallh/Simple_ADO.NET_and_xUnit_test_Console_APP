using System;
using System.Diagnostics;
using ContactsBusinessLayer;
using ContactsDataAccessLayer;

namespace XUnitTest {
    public class DataLayerTest {
        [Fact]
        public void TryConnection_ShouldReturnTrue() {
            bool result = dbConnection.TryConnection();
            Assert.True(result);
        }
        [Fact]
        public void TryRetriveDataByID_ShouldReturnFullObject() {
            clsContact contact=null ;

            contact = clsContact.FindByID(1);

            Assert.Equal(1, contact.ContactID);
            Assert.Equal("Mohammed", contact.FirstName);
            Assert.Equal("Abu-Hadhoud", contact.LastName);
            Assert.Equal("M@A.com", contact.Email);
            Assert.Equal("1234", contact.Phone);
            Assert.Equal("Adress 1", contact.Address);
            Assert.Equal("C:\\Photos\\Mohammed AbuHadhoud.JPG", contact.ImagePath);
            Assert.Equal("11/6/1977 10:30:00 AM", contact.DateOfBirth.ToString());
            Assert.Equal(1, contact.CountryID);
        }
        [Fact]
        public void TryRetriveDataByID_ShouldReturnNull() {
            clsContact contact = null;

            contact = clsContact.FindByID(72);

            Assert.Null(contact);
        }
        [Fact]
        public void TryCheckIfExiestContact_ShoudReturnTrue() {

            Assert.True(clsContact.CheckIfExistContact(1));
        }
        [Fact]
        public void TryCheckReturnAllContact_ShouldReturnNotNUllObject() {

            Assert.NotNull(clsContact.GetAllContcts());
        }
        [Fact]
        public void TryCheckCountryByName_ShouldReturnNumber() {

            Assert.Equal(1, clsCountries.FindCountryByName("United States"));
        }









    }

}

