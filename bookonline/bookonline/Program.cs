using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookonline
{
    public class category
    {
        public string categoryname { get; set; }
        public decimal discount { get; set; }
        public category(string categ, decimal dis)
        {
            categoryname = categ;
            discount = dis;
        }
        public virtual ICollection<book> books { set; get; }   
    }

    public class book
    {
        public string name { get; set; }
        public category categoryname { get; set; }
        public decimal totalcost { get; set; }

        public book(string bookname, category bookcateg, decimal cost)
        {
            name = bookname;
            categoryname = bookcateg;
            totalcost = cost;
        }
    }

    public class test
    {
        List<category> categories = new List<category>();
        List<book> books = new List<book>();

        public bool AddCategory(category newcateg)
        {
            try
            {
                categories.Add(newcateg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public void ViewAllCategories()
        {
            foreach (category categ in categories)
            {
                Console.WriteLine(categ.categoryname);
                Console.ReadLine();
            }
        }
        
        public bool AddBooks(book newbook)
        {
            try
            {
                books.Add(newbook);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
          
        }

        public bool ViewReceipt(decimal taxPercentage)
        {
            try
            {
                decimal totalDiscountedAmount = 0;
                decimal amount = 0;
                decimal discountedAmount = 0;
                decimal totalCost = 0;
                decimal totalAmount = 0;
                decimal finalAmount = 0;
                decimal tax = 0;
                decimal totalAmountAfterDiscount = 0;

                Console.WriteLine("Book Name           | Category   | Total Cost     | Discount        | Amount");
                Console.WriteLine("-----------------------------------------------------------------------------");
                foreach (book bookin in books)
                {
                    discountedAmount = calculateDiscount(bookin);
                    amount = 0;

                    amount = bookin.totalcost - discountedAmount;
                    Console.WriteLine("{0,-23}{1,-16}{2,-15}{3,-15}{4,-20}", bookin.name, bookin.categoryname.categoryname, Math.Round(bookin.totalcost, 2), Math.Round(discountedAmount, 2), Math.Round(amount, 2));


                    totalCost += bookin.totalcost;
                    totalAmount += amount;
                    totalDiscountedAmount += discountedAmount;
                }
                totalAmountAfterDiscount = totalCost - totalDiscountedAmount;
                tax = calculateTax(totalAmountAfterDiscount, taxPercentage);
                finalAmount = totalAmountAfterDiscount + tax;
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("{0,-39}{1,-15}{2,-15}{3,-20}", "Sub Total", Math.Round(totalCost, 2), Math.Round(totalDiscountedAmount, 2), Math.Round(totalAmountAfterDiscount, 2));
                Console.WriteLine("Tax                                                                  " + Math.Round(tax, 2));
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("TOTAL (Includes Tax)                                                 " + Math.Round(finalAmount, 2));                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public decimal calculateDiscount(book thisbook)
        {
            decimal discount = 0;
            if (thisbook.categoryname.categoryname.ToLower().ToString() == "crime")
            {
                discount = thisbook.totalcost * thisbook.categoryname.discount / 100;
            }
            return discount;

        }
        public decimal calculateTax(decimal totalAmountAfterDiscount, decimal taxPercentage)
        {
            decimal tax = 0;
            tax = totalAmountAfterDiscount * taxPercentage / 100;
            return tax;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            category c1 = new category("Crime", 5);
            category c2 = new category("Romance", 0);
            category c3 = new category("Fantasy", 0);

            test t1 = new test();
            //t1.AddCategory(c1);
            //t1.AddCategory(c2);
            //t1.AddCategory(c3);
            //t1.ViewAllCategories();

            t1.AddBooks(new book("Unsolved crimes", c1, 10.99m));
            t1.AddBooks(new book("A Little Love Story", c2, 2.40m));
            t1.AddBooks(new book("Heresy", c3, 6.80m));
            t1.AddBooks(new book("Jack the Ripper", c1, 16.00m));
            t1.AddBooks(new book("The Tolkien Years", c3, 22.90m));

            t1.ViewReceipt(10);
            Console.ReadLine();
        }
    }
}
