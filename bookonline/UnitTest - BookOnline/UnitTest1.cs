using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bookonline;
using System.Collections.Generic;

namespace UnitTest___BookOnline
{
    [TestClass]
    public class UnitTest1
    {
        test obj;
        List<category> categories = new List<category>();
        List<book> books = new List<book>();

        [TestInitialize]
        public void TestInitialise()
        {
            obj = new test();

            category c1 = new category("Crime", 5);
            category c2 = new category("Romance", 0);
            category c3 = new category("Fantasy", 0);


            categories.Add(c1); categories.Add(c2); categories.Add(c3);

            books.Add(new book("Unsolved crimes", c1, 10.99m));
            books.Add(new book("A Little Love Story", c2, 2.40m));
            books.Add(new book("Heresy", c3, 6.80m));
            books.Add(new book("Jack the Ripper", c1, 16.00m));
            books.Add(new book("The Tolkien Years", c3, 22.90m));
        }
        [TestMethod]
        public void can_I_Call_AddCategory_Success()
        {
            category c1 = new category("Crime", 5);
            bool result = obj.AddCategory(c1);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void can_I_Call_AddBooks_Success()
        {
            category c1 = new category("Romance", 5);
            book b1 = new book("Unsolved crimes", c1, 10.99m);
            bool result = obj.AddBooks(b1);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void can_I_Call_ViewReceipt_Success()
        {         
            decimal taxPer=10;
            bool result = obj.ViewReceipt(taxPer);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void count_TotalCategories_Added()
        {
            Assert.AreEqual(3, categories.Count);
        }
        [TestMethod]
        public void count_TotalBooks_Purchased()
        {
            Assert.AreEqual(5, books.Count);
        }
        [TestMethod]
        public void Test_CrimeCategories_AreDicounted_By5Per()
        {
            category c1 = categories.Find(c => c.categoryname == "Crime");
            decimal discount = c1.discount;
            Assert.AreEqual(5, discount);

        }
        //Apply discount only for Crime Categ - Success
        [TestMethod]
        public void discountTest_Success()
        {
            category c1 = new category("Crime", 5);
            var bookss = new book("Unsolved crimes", c1, 10.99m);
            decimal calcdiscount = Convert.ToDecimal(10.99 * 5 / 100);
            decimal discount = obj.calculateDiscount(bookss);
            Assert.AreEqual(calcdiscount, discount);
        }
        [TestMethod]
        public void discountTest_Fail()
        {
            category c1 = new category("Romance", 5);
            var bookss = new book("A Little Love Story", c1, 10.99m);
            decimal discount = obj.calculateDiscount(bookss);
            Assert.AreEqual(0, discount);
        }

        [TestMethod]
        public void applyTax_Success()
        {
            decimal totalAmountAfterDiscount = 50, taxPercentage = 10;
            decimal tax = obj.calculateTax(totalAmountAfterDiscount, taxPercentage);
            Assert.AreEqual(5, tax);
        }        
       
    }
}
