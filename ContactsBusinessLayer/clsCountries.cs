using ContactsDataAccessLayer;
using System;


namespace ContactsBusinessLayer {
    public class clsCountries {
        public static int FindCountryByName(string countryName) {
            int countryID = RetriveFromDataBase.GetCountryIDByName(countryName);
            if (countryID > 0) {
                Console.WriteLine($"Country Name is Found: {countryName} and it's ID is {countryID}");
            }
            else {
                Console.WriteLine($"Country Name is Not Found...{countryName}");
            }
            return countryID;
        }   

        
    }
}
