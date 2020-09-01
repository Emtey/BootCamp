using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using AppTypes;
using System.Collections;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace TestHarness
{
    //This is the testing program, all the tests will be wired here,
    //then hooked up to the Class libraries.
    //Please find the appropriate test under the appropriate header.
    class Program
    {
        static void Main(string[] args)
        {
            TestSupplier();
            TestAppTypesException();
            TestSuppliers();
            TestProduct();
            TestProducts();
            TestGenericCollection();
            TestExplicitConversionOperators();
            TestSerialization();
            TestEvents();
            TestAttributes();
            Console.WriteLine("\nALL TESTS COMPLETED!");
            Wait();

        }
        #region Convenience Methods (Begin, Test, End, Wait, ShowCollection)
        static void Begin(string tsn)
        {
            Console.WriteLine(string.Format("===Begin Sequence: {0}",tsn));
        }
        static void Test(string tf)
        {   
            StringBuilder sb = new StringBuilder();
            sb.Append("\nTest:\t");
            sb.Append(tf);
            sb.Append("\t");

            Console.WriteLine(sb.ToString().PadRight(50,'-'));
        }
        static void End(string tsn)
        {
            Console.WriteLine(string.Format("\n===End Sequence: {0}\n", tsn));
        }
        static void Wait()
        {
            Console.ReadLine();
        }
        static void ShowCollection(IEnumerator myCollection)
        {   
            while (myCollection.MoveNext())
            {
                Console.WriteLine(myCollection.Current.ToString());
            }
        }
        #endregion //end convenience methods.

        #region TEST CODE: Supplier
        static void TestSupplier()
        {           
            string testName = "Supplier Test";
            Begin(testName);
            Supplier s1;
            Supplier s2;
            Supplier s3;

            #region Non-Default Constructor and ToString Method Test
            Test("Non-Default Constructor and ToString Method Test");
            //Non Default Constructor test
            s1 = new Supplier(1, "MyCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            Console.WriteLine(s1.ToString());
            #endregion

            #region Default Constructor and ToString Method Test
            Test("Default Constructor and ToString Method Test");
            //Default Constructor test
            s2 = new Supplier();
            s2.ID = 2;
            s2.CompanyName = "M";
            s2.ContactName = "n";
            s2.ContactTitle = "a";
            s2.Address = "b";
            s2.City = "c";
            s2.Region = "d";
            s2.PostalCode = "123";
            s2.Country = "1";
            s2.Phone = "1";
            s2.Fax = "1";
            s2.Homepage = "wwwwww";
            s2.Type = SupplierTypes.Service;
            Console.WriteLine(s2.ToString());
            #endregion

            #region Company Name Validation Test
            Test("CompanyName Validation Test");
            try
            {
                s1 = new Supplier(1, null, "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "wwwwww", SupplierTypes.Supply);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region City validation - too long
            Test("City Validation - Too Long");
            try
            {
                s1 = new Supplier(1, "myCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "12345678901234567890", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Constructor Uses Property Validation
            Test("Constructor Uses Property Validation");
            try
            {
                s3 = new Supplier(3, "MyCompany", "Mark", "CEO", string.Empty, "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region ID Validation -5
            Test("ID Validation -5");
            try
            {
                s1 = new Supplier(-5, "MyCompany-s1", "Mark", "CEO", "123 Anywhere Blvd.", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region ContactInfo Method Test
            Test("ContactInfo Method Test");
            s1 = new Supplier(1, "MyCompany", "Mark", "CEO", "123 Anywhere Blvd.", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            Console.WriteLine(s1.ContactInfo().ToString()); 
            #endregion

            #region GetHashCode Method Test
            Test("GetHashCode Method Test");
            Console.WriteLine(s1.GetHashCode().ToString()); 
            #endregion

            #region SupplierTypes enum
            Test("SupplierTypes Enum Test");
            Console.WriteLine(SupplierTypes.Product.ToString() + "\n" + SupplierTypes.Service.ToString() 
                + "\n" + SupplierTypes.Supply.ToString());
            #endregion

            #region PropertyAndValuesCollection Method Test
            Test("PropertyAndValuesColletion Method Test");
            ShowCollection(s1.PropertyAndValuesCollection());
            #endregion

            #region s1 == s2 Test
            Test("s1 == s2 Test");
            s2 = new Supplier(1, "MyCompany", "Mark", "CEO", "123 Anywhere Blvd.", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            Console.WriteLine((s1 == s2).ToString()); //true
            #endregion

            #region s1 == s2 Test
            Test("s1 != s2 Test");
            Console.WriteLine((s1 != s2).ToString());//false
            #endregion

            #region s3 == s1 (s3 is null)
            Test("s3 == s1  (s3 is null)");
            s3 = null;
            Console.WriteLine((s3 == s1).ToString());//should return false
            #endregion

            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: AppTypesException
        static void TestAppTypesException()
        {
            string testName = "AppTypesException";
            Begin(testName);
           
            Test("AppTypesException - ToString()");
            try
            {
                throw new Exception("Error");
            }
            catch (Exception ex)
            {
                AppTypesException ate = new AppTypesException(ex.Message, ex);
                Console.WriteLine(ate.GetType().Name + ":" + ex.Message + "\n" +
                                   ate.InnerException);

                Test("AppTypesException - Line Number");
                Console.WriteLine("Line Number: " + ate.LineNumber);
            }
            End(testName);
            Wait();
            
        }
        #endregion

        #region TEST CODE: Suppliers
        static void TestSuppliers()
        {
            string testName = "Suppliers";
            Begin(testName);

            #region Add Method and IEnumerable
            Test("Add Method and IEnumerable");
            Suppliers sups = new Suppliers();
            sups.Add(Suppliers.CreateSupplier(21, "MyCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply));
            sups.Add(Suppliers.CreateSupplier(22, "ThisCompany", "Bob", "Lackey", "1234 There", "Washington", "Maryland", "1234", "ASU", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Product));
            sups.Add(Suppliers.CreateSupplier(23, "phifzer", "Bob", "Lackey", "1234 There", "Here", "Markland", "1234", "BBB", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Service));
            sups.Add(Suppliers.CreateSupplier(24, "phisher", "Bob", "Lackey", "1234 There", "There", "Texas", "1234", "UAS", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Service));
            sups.Add(Suppliers.CreateSupplier(25, "phish", "Bob", "Lackey", "1234 There", "Anywhere", "Maryland", "1234", "ASU", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Product));
            sups.Add(Suppliers.CreateSupplier(26, "petunia", "Bob", "Lackey", "1234 There", "Anywher", "Maryland", "1234", "ASU", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Product));

            //OR Using the Static methods in teh DataAccess.Da class:
            //****if you use this the dupes test wont return and error since i have it looking for a specific object.****
            //sups.Add(new Supplier(DataAccess.DA.GetSupplier(21)));
            //sups.Add(new Supplier(DataAccess.DA.GetSupplier(22)));
            //sups.Add(new Supplier(DataAccess.DA.GetSupplier(23)));
            //sups.Add(new Supplier(DataAccess.DA.GetSupplier(24)));
            //sups.Add(new Supplier(DataAccess.DA.GetSupplier(25)));
            //sups.Add(new Supplier(DataAccess.DA.GetSupplier(26)));
            ShowCollection(sups.GetEnumerator());

            #endregion

            #region Count Method
            Test("Count Method");
            Console.WriteLine(sups.Count.ToString());
            #endregion

            #region Type Checking in Add Method
            Test("Type checking in Add method");
            try
            {
                sups.Add("String Instance");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region RemoveAt
            Test("RemoveAt");
            for (int i = 0; i < 3; i++) 
                sups.RemoveAt(0);
            ShowCollection(sups.GetEnumerator());
            #endregion

            #region Insert
            Test("Insert");
            sups.Insert(0, Suppliers.CreateSupplier(50001, "MyCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply));
            ShowCollection(sups.GetEnumerator());
            #endregion

            #region Insert Detects Duplicates
            Test("Insert Detect Duplicates");
            try
            {
                sups.Insert(2, Suppliers.CreateSupplier(26, "petunia", "Bob", "Lackey", "1234 There", "Anywher", "Maryland", "1234", "ASU", "1231231234",
                    "1231231234", "www.thiscompany.com", SupplierTypes.Product));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region InvalidIndex
            Test("Invalid Index");
            try
            {
                Console.WriteLine(sups[10].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Type Checking in Indexer
            Test("Type Checking in Indexer");
            try
            {
                sups[1] = "String Instance";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion

            #region Indexer Detects Duplicates
            Test("Indexer Detects Duplicates");
            try
            {
                sups[2] = Suppliers.CreateSupplier(26, "petunia", "Bob", "Lackey", "1234 There", "Anywher", "Maryland", "1234", "ASU", "1231231234",
                    "1231231234", "www.thiscompany.com", SupplierTypes.Product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region CopyTo
            Test("CopyTo");
            int x = sups.Count;
            Supplier[] myArray = new Supplier[x];
            sups.CopyTo(myArray);
            ShowCollection(myArray.GetEnumerator());
            #endregion

            #region GetTypeEnumerator
            Test("GetTypeEnumerator");
            ShowCollection(sups.GetTypeEnumerator(SupplierTypes.Product));           
            #endregion

            #region GetReverseEnumerator
            Test("GetReverseEnumerator");
            ShowCollection(sups.GetReverseEnumerator());
            #endregion

            #region Sort on TypeCompanyName
            Test("Sort on TypeCompanyName");
            sups.Sort(Supplier.GetSortByTypeCompanyName());
            ShowCollection(sups.GetEnumerator());
            #endregion

            #region Sort on CountryRegionCity
            Test("Sort on CountryRegionCity");
            sups.Sort(Supplier.GetSortByCountryRegionCity());
            foreach (Supplier s in sups)
                Console.WriteLine("ID: {0,5}\tCountry: {1}\tRegion: {2,10}\tCity: {3}", s.ID.ToString(), s.Country, s.Region, s.City);
            #endregion

            #region IComparable
            Test("IComparable");
            sups.Sort();
            ShowCollection(sups.GetEnumerator());
            #endregion

            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: Product
        static void TestProduct()
        {
            Product p1, p2;
            string testName = "Product";
            Begin(testName);

            #region Constructor and ToString Method
            Test("Constructor and ToString Method");
            p1 = new Product(1, "Widgets", 24, 2, "50", 23.20M, 15, 200, 10);
            p2 = new Product(2, "Wonkets", 26, 7, "250", 947.74M, 2, 1, 1);
            Console.WriteLine(p1.ToString());
            Console.WriteLine(p2.ToString());
            #endregion

            #region PropertyAndValuesCollection
            Test("PropertyAndValuesColletion Method Test (UnitPrice as Currency)");
            ShowCollection(p1.PropertyAndValuesCollection());
            #endregion

            #region Operator Overloads
            Test("Operator Overloads");
            Console.WriteLine((p1 < p2).ToString()); //true
            Console.WriteLine((p1 > p2).ToString()); //false
            Console.WriteLine((p1 <= p2).ToString()); //true
            Console.WriteLine((p1 >= p2).ToString()); //False
            #endregion

            #region Null Instance
            Test("Null Instance");
            Product p3 = null;
            Console.WriteLine((p3 < p1).ToString());
            #endregion

            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: Products
        static void TestProducts()
        {
            string testName = "Products";
            Begin(testName);

            #region Constructor, Add(), IEnumerable
            Test("Constructor, Add(), IEnumerable");
            Products products = new Products();
            for (int i = 21; i < 31; i++)
                products.Add(new Product(DataAccess.DA.Getproduct(i)));
            ShowCollection(products.GetEnumerator());
            #endregion

            #region CopyTo
            Test("Region");
            int x = products.Count;
            Product[] myArray = new Product[x];
            products.CopyTo(myArray, 0);
            ShowCollection(myArray.GetEnumerator());
            #endregion

            #region GetSortByCategoryID
            Test("GetSortByCategoryID");
            products.Sort(Product.GetSortByCategoryID());
            ShowCollection(products.GetEnumerator());
            #endregion

            #region IComparable
            Test("IComparable");
            products.Sort();
            ShowCollection(products.GetEnumerator());
            #endregion

            #region GetSortBySupplierID
            Test("GetSortBySupplierID");
            products.Sort(Product.GetSortBySupplierID());
            ShowCollection(products.GetEnumerator());
            #endregion


            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: GenericCollection
        static void TestGenericCollection()
        {
            string testName = "GenericCollection";
            Begin(testName);
            Supplier s1 = new Supplier(1, "MyCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            Supplier s2 = new Supplier();
            s2 = new Supplier();
            s2.ID = 2;
            s2.CompanyName = "M";
            s2.ContactName = "n";
            s2.ContactTitle = "a";
            s2.Address = "b";
            s2.City = "c";
            s2.Region = "d";
            s2.PostalCode = "123";
            s2.Country = "1";
            s2.Phone = "1";
            s2.Fax = "1";
            s2.Homepage = "wwwwww";
            s2.Type = SupplierTypes.Service;

            #region Constructor Add and IEnumerable
            Test("Constructor, Add and IEnumerable");
            GenericCollection<Supplier> genericSuppliers = new GenericCollection<Supplier>();
            genericSuppliers.Add(s1);
            genericSuppliers.Add(s2);
            ShowCollection(genericSuppliers.GetEnumerator());
            #endregion

            #region CopyTo Method
            Test("CopyTo Method");
            int x = genericSuppliers.Count;
            Supplier[] supplierArray = new Supplier[x];
            genericSuppliers.CopyTo(supplierArray,0);
            ShowCollection(supplierArray.GetEnumerator());
            #endregion

            #region Sort Array on TypeCompanyName
            Test("Sort Array on TypeCompanyName");
            Array.Sort(supplierArray, Supplier.GetSortByTypeCompanyName());
            ShowCollection(supplierArray.GetEnumerator());
            #endregion
            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: Explicit Conversion Operators
        static void TestExplicitConversionOperators()
        {
            string testName = "Explicit Conversion Operators";
            Begin(testName);
            Supplier s1 = new Supplier(1, "MyCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply);
            
            Product p1 = new Product(1, "Widgets", 24, 2, "50", 23.20M, 15, 200, 10);
            ShowCollection(s1.PropertyAndValuesCollection());
            Console.WriteLine(""); //this just puts a line space between the two.
            ShowCollection(p1.PropertyAndValuesCollection());

            #region Conversion Operators
            Test("Conversion Operators");
            DataAccess.SupplierStruct supplierStruct = (DataAccess.SupplierStruct) s1;
            Console.WriteLine("From supplierStruct:");
            Console.WriteLine("ID: {0}\tCompanyName: {1}", supplierStruct.ID.ToString(), supplierStruct.CompanyName);
            DataAccess.ProductStruct prodStruct = (DataAccess.ProductStruct)p1;
            Console.WriteLine("From prodStruct:");
            Console.WriteLine("ID: {0}\tProductName: {1}", prodStruct.ID.ToString(), prodStruct.ProductName);
            #endregion

            #region Convert Structure to object
            Test("Convert Structure to object");
            Supplier s2 = new Supplier(supplierStruct);
            ShowCollection(s2.PropertyAndValuesCollection());
            Console.WriteLine("");  //puts a space between the two.
            Product p2 = new Product(prodStruct);
            ShowCollection(p2.PropertyAndValuesCollection());
            #endregion

            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: Serialization
        static void TestSerialization()
        {
            string testName = "Serialization";
            Begin(testName);
            Suppliers sups = new Suppliers();
            sups.Add(Suppliers.CreateSupplier(21, "MyCompany", "Mark", "CEO", "1234 Here St. PO Box 123", "Dallas", "Texas", "12344", "USA", "555123123",
                    "1231231234", "www.MyCompany.com", SupplierTypes.Supply));
            sups.Add(Suppliers.CreateSupplier(22, "ThisCompany", "Bob", "Lackey", "1234 There", "Washington", "Maryland", "1234", "ASU", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Product));
            sups.Add(Suppliers.CreateSupplier(23, "phifzer", "Bob", "Lackey", "1234 There", "Here", "Markland", "1234", "BBB", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Service));
            sups.Add(Suppliers.CreateSupplier(24, "phisher", "Bob", "Lackey", "1234 There", "There", "Texas", "1234", "UAS", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Service));
            sups.Add(Suppliers.CreateSupplier(25, "phish", "Bob", "Lackey", "1234 There", "Anywhere", "Maryland", "1234", "ASU", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Product));
            sups.Add(Suppliers.CreateSupplier(26, "petunia", "Bob", "Lackey", "1234 There", "Anywher", "Maryland", "1234", "ASU", "1231231234",
                "1231231234", "www.thiscompany.com", SupplierTypes.Product));
            ShowCollection(sups.GetEnumerator());

            #region Serialize suppliers collection
            Test("Serialize suppliers collection");
            try
            {
                using (FileStream fs = new FileStream("SerializedSuppliers.xml", FileMode.Create,
                    FileAccess.Write, FileShare.None))
                {
                    SoapFormatter sf = new SoapFormatter();
                    sf.Serialize(fs, sups);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Serialization Complete");
            #endregion

            #region Restore the Suppliers
            Test("Restore the Suppliers");
            sups = null;
            try
            {
                using (FileStream fs = new FileStream("SerializedSuppliers.xml", FileMode.Open,
                    FileAccess.Read, FileShare.None))
                {
                    SoapFormatter sf = new SoapFormatter();
                    sups = (Suppliers)sf.Deserialize(fs);
                    //showCollection(sups.GetEnumerator());
                    //foreach (int i in sups)
                    //{
                    //    Console.WriteLine(i.ToString());
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            ShowCollection(sups.GetEnumerator());
            #endregion


            End(testName);
            Wait();
        }
        #endregion

        #region TEST CODE: Events
        static void TestEvents()
        {
            string testName = "Events";
            Begin(testName);
            Product p1 = new Product(DataAccess.DA.Getproduct(21));
            Product p2 = new Product(DataAccess.DA.Getproduct(22));
            Product p3 = new Product(DataAccess.DA.Getproduct(23));
            Products products = new Products();

            products.CollectionModified += new CollectionModifiedHandler(CollectionModifiedEvent);            

            #region Add Product p1
            Test("Add Product p1");
            products.Add(p1);            
            #endregion

            #region Add Product p2
            Test("Add Product p2");
            products.Add(p2);
            #endregion

            #region No Dupes
            Test("No Dupes");
            try
            {
                products.Add(p1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Replace Item 0 with p3
            Test("Replace Item 0 with p3");
            products[0] = p3;
            //ShowCollection(products.GetEnumerator());
            #endregion

            #region Remove product p2
            Test("Remove product p2");
            products.Remove(p2);
            #endregion

            #region Remove item not in collection p2
            Test("Remove item not in collection p2");
            products.Remove(p2);
            #endregion

            End(testName);
            Wait();
        }

        /// <summary>
        /// Event handler event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CollectionModifiedEvent(object sender, ModificationEventArgs e)
        {
            Console.WriteLine("MODIFICATION ATTEMPTED: " + e.ModificationEventStatus.ToString());
        }   

        #endregion

        #region TEST CODE: Attributes
        static void TestAttributes()
        {
            string testName = "Attributes";
            Begin(testName);

            Supplier s1 = new Supplier((DataAccess.DA.GetSupplier(21)));

            object[] myArray;
            myArray = s1.GetType().GetCustomAttributes(true);
            foreach (Attribute att in myArray)
            {
                if (att is DeveloperInfoAttribute)
                    Console.WriteLine("Name: " + ((DeveloperInfoAttribute)att).Name + " Date: " + ((DeveloperInfoAttribute)att).Date +
                        " Title: " + ((DeveloperInfoAttribute)att).Title + " TypeId: "+ ((DeveloperInfoAttribute)att).TypeId);
                else if (att is CustomDescriptionAttribute)
                    Console.WriteLine("Description: " + ((CustomDescriptionAttribute)att).Description + " TypeId: " + ((CustomDescriptionAttribute)att).TypeId);
            }

            End(testName);
            Wait();
        }
        #endregion
    }
    
}
