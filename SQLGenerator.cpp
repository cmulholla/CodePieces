// SQLGenerator.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <string>
#include <cstdlib>
#include <time.h>
#include <vector>

using namespace std;

// generate an amount of insert statements for the managers table. Names based upon the Narray.
void printManInsert(string table, string* Narray, int outnum) {
    for (int i = 0; i < outnum; i++)
        cout << "INSERT INTO " << table << " VALUES('" << 
                Narray[rand() % 10] << "', '" << 
                Narray[rand() % 10] << "', " << 
                rand() % 2500 + 2500 << ");" << endl;
}

// generate an amount of insert statements for the Gas Station table. Names based upon the Narray.
void printGasInsert(string table, string* Narray, int outnum) {
    string times[2] = { "6AM-10PM", "24H" };
    for (int i = 0; i < outnum; i++)
        cout << "INSERT INTO " << table << " VALUES('" <<
        rand() % 20000+1000 << " " << Narray[rand() % 10] << " Rd, " << Narray[rand() % 10] << ", MI " << rand() % 10000 + 40000 << "', '" <<
        times[rand() % 2] << "', '(810) " <<
        rand() % 898 + 101 << "-" << rand() % 8998 + 1001 << "', " << 
        rand() % 4 + 1 << ", " <<
        (rand() % 30 + 300)/100.f << ", " <<
        (rand() % 4 + 1) * 2 << ");\n";
}

// generate an amount of insert statements for the employees table. Names based upon the Narray.
void Employees(string table, string* Narray, int outnum) {
    string positions[2] = { "Cashier", "Stocker" };
    for (int i = 0; i < outnum; i++)
        cout << "INSERT INTO " << table << " VALUES('" <<
        Narray[rand() % 10] << "', '" <<
        Narray[rand() % 10] << "', 1700, '" << 
        positions[rand() % 2] << "', " << 
        rand() % 15 + 1 << ");" << endl;
}

// generate an amount of insert statements for the customers table. Names based upon the Narray.
void Customers(string table, string* Narray, int outnum, string (&StoreCustomer)[200][2]) {
    for (int i = 0; i < outnum; i++) {
        StoreCustomer[i][0] = to_string(rand() % 89998 + 10000);
        StoreCustomer[i][1] = to_string(rand() % 99999 + 1000);
        cout << "INSERT INTO " << table << " VALUES(" <<
            StoreCustomer[i][0] << ", " <<
            StoreCustomer[i][1] << ", '" <<
            Narray[rand() % 10] << "', '" <<
            Narray[rand() % 10] << "');" << endl;
    }
}

// generate an amount of insert statements for the categories table. Names of categories based upon the items array.
void Category(string table, string* items, int outnum) {
    for (int i = 0; i < outnum; i++)
        cout << "INSERT INTO " << table << " VALUES('" <<
        items[i] << "');" << endl;
}

// generate all of the possible insert statements for the inventory table. pre-defined inventory defined in InvItems, categories of the items based upon items, and prices for each item defined in StorePrice.
void Inventory(string table, vector<vector<string>> InvItems, string* items, float (&StorePrice)[7][18][15][2]) {
    int serial = 1;
    for (int i = 0; i < 7; i++) {
        for (int j = 0; j < InvItems[i].size(); j++) {
            for (int c = 1; c <= 15; c++) {
                StorePrice[i][j][c - 1][0] = ((rand() % 1800) / 100.f) + 2; //price
                StorePrice[i][j][c - 1][1] = serial++; //keep track of serial number
                cout << "INSERT INTO " << table << " VALUES(" <<
                    StorePrice[i][j][c - 1][0] << ", '" <<
                    InvItems[i][j] << "', " <<
                    c << ", " << 
                    rand() % 19 + 1 << ", " <<
                    rand () % 5 + 20 << ", '" <<
                    items[i] << "');\n";
            }
        }
    }
}

// generate an amount of insert statements for the purchases table. Customer names based upon StoreCustomer, customerID based on customer, and storeID is self explanatory.
void Purchases(string table, string(&StoreCustomer)[200][2], int customer, int StoreID) {
    cout << "INSERT INTO " << table << " VALUES(" <<
        StoreCustomer[customer][0] << ", '" <<
        StoreCustomer[customer][1] << "', " <<
        StoreID << ");" << endl;
}

// generate an amount of insert statements for the Purchases_Inventory table. This is an in-between table for linking purchases to the inventory.
void Purchases_Inventory(string table, float(&StorePrice)[7][18][15][2], vector<vector<string>> InvItems, string(&StoreCustomer)[200][2], int PurchaseCount) {
    float totalPrice = 0;
    for (int i = 1; i <= PurchaseCount; i++) {
        int numPurchases = rand() % 8 + 1;
        int store = rand() % 15;
        // generate purchases for a specific store
        Purchases("Purchases(CustomersCustomerID, CustomersPassw, StoreID)", StoreCustomer, rand() % 200, store + 1);
        // generate statements linking the purchases generated above to the inventory table found in InvItems.
        for (int j = 0; j < numPurchases; j++) {
            int category = rand() % 7;
            int item = rand() % InvItems[category].size();
            totalPrice += StorePrice[category][item][store][0];
            cout << "INSERT INTO " << table << " VALUES(" <<
                i << ", " <<
                StorePrice[category][item][store][1] << ");" << endl;
        }
        cout << "UPDATE Purchases SET TotalCost = " << totalPrice << " WHERE PurchaseNumber = " << i << ";\n";
        totalPrice = 0;
    }
}

// This was a fix I had to do because I had to change some project specifications.
void PurchasesFix(int PurchaseCount) {
    string date;
    for (int i = 0; i <= PurchaseCount; i++) {
        date = to_string(2001 + rand() % 20) + "-" + to_string(rand() % 12 + 1) + "-" + to_string(rand() % 28 + 1) + " " + to_string(rand() % 10 + 9) + ":" + to_string(rand() % 59) + ":" + to_string(rand() % 59);
        cout << "UPDATE Purchases SET PurchaseDate = '" << date << "' WHERE PurchaseNumber = " << i << ";\n";
    }

}


int main()
{
    // names used throughout the generated code
    string NameArray[10] = { "steven", "dave", "konan", "nagato", "itachi", "toby", "madara", "naruto", "danzou", "kakashi" };
    
    // specific categories used.
    string Categories[7] = {"Dairy", "Medicines", "Foods", "Meats and Produce", "Drinks", "Snacks", "Electronics and Equipment"};
    // all of the items used for every store. These line up with the categories array.
    vector<vector<string>> InvItems = { {"One Gallon Milk", "Half Gallon Milk", "Pint Milk", "Butter", "cottage cheese", "Eggs"},   
        {"Tylenol", "Aspirin", "Cough Meds", "Lip Balm", "Sinus Meds", "Throat Lozenges"}, 
        {"White Bread", "Hot Dogs", "Rye Bread", "Hot Dog Buns", "Hamburger Buns", "Dinner Rolls", "Vegetable Soup", "Chicken Noodle Soup", "Canned Meat", "Chili", "Stew", "Bagged Pasta", "Bagged Beans", "Pickles", "Relish", "Catsup", "Mustard", "Mayonnaise"}, 
        {"Bologna", "Ham","Pickle Loaf", "Salami", "Turkey", "Bacon"}, 
        {"Apple Juice", "Orange Juice", "Grape Juice", "Juice Boxes", "Coffee Creamer", "Tea", "Coca-Cola", "Pepsi", "Gatorade", "7-up", "dr. pepsi", "diet coke"},
        {"Hershey Bar", "Lays chips", "Beef Jerky", "Chewing Gum", "Pudding", "Sweet Cakes", "Nuts", "Single Pickles", "Nachos"}, 
        {"Batteries", "Cooler", "Flashlight", "Candles", "Windshield Cleaner", "Cleaning Fluid", "Transmission Fluid", "Motor Oil"} };
    
    //initialized for use by functions, and used later for linking tables to each other.
    float StorePrice[7][18][15][2]; // I hard coded these but these can also be dynamically found. [category][item][store][item details]
    string StoreCustomer[200][2]; // [200 customers][first name and last name]
    
    srand(time(0)); //generate a random seed
    //INSERT INTO GasStation(StoreAddress, OperatingHours, PhoneNumber, ManagerID, GasPrice, Pumps) VALUES ('8039 S State Rd, Goodrich, MI 48438', '6AM-10PM', '(810) 636-3222', 1, 3.18, 2);
    /*
    //INSERT INTO Managers(FirstName, LastName, MonthlyWage) VALUES('Connor', 'Mulholland', 5000);
    //Functions used to generate all of the SQL functions.
    printManInsert("Managers(FirstName, LastName, MonthlyWage)", NameArray, 3);
    printGasInsert("GasStation(StoreAddress, OperatingHours, PhoneNumber, ManagerID, GasPrice, Pumps)", NameArray, 13);
    Employees("Employees(FirstName, LastName, MonthlyWage, Position, GasStationStoreID)", NameArray, 60);
    Customers("Customers(CustomerID, Passw, FirstName, LastName)", NameArray, 200, StoreCustomer);
    Category("Category(CategoryName)", Categories, 7);
    Inventory("Inventory(Price, ProductName, GasStationStoreID, Stock, maxStockQuantity, CategoryName)", InvItems, Categories, StorePrice);
    Purchases_Inventory("Purchases_Inventory(PurchasesPurchaseNumber, InventorySerial)", StorePrice, InvItems, StoreCustomer, 1000);
    
    cout << "test: " << InvItems[2].size();
    */
    // I commented out all of the previous code because I just needed the fix after I generated all of the above code.
    PurchasesFix(1000);

}
