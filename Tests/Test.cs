using I2P_Project.Classes;
using I2P_Project.Classes.UserSystem;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;

namespace I2P_Project.Tests
{
    class Test
    {
        private Admin admin;

        public Test()
        {
            SDM.LMS.ClearDB();
            admin = new Admin("admin");
        }
        
        public void Test1()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Librarian lb = new Librarian("lb");

            admin.ModifyLibrarian(lb.PersonID, "lb", "lb", "lb", 2);
            
            lb.AddAV("b", "b", 0, 2);
            DocClass b = new DocClass("b");
            
            st.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(b.Quantity == 1);
            Debug.Assert(SDM.LMS.GetUserBooks(st.PersonID).Count == 1);
        }

        public void Test2()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Librarian lb = new Librarian("lb");
            
            DocClass A = new DocClass("A");
            
            st.CheckOut(A.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(A.ID) == null);
            Debug.Assert(SDM.LMS.GetUserBooks(st.PersonID).Count == 0);
        }

        public void Test3()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("ft", "ft", "ft", "ft", "ft", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Faculty ft = new Faculty("ft");
            Librarian lb = new Librarian("lb");
            
            lb.AddBook("b", "B", "B", 0, "B", "B", 0, false, 1);
            DocClass b = new DocClass("b");
            
            ft.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(ft.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(b.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(ft.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.OverdueTime(ft.PersonID, b.ID) / 7 == 4);
        }

        public void Test4()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("ft", "ft", "ft", "ft", "ft", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Faculty ft = new Faculty("ft");
            Librarian lb = new Librarian("lb");
            
            lb.AddBook("b", "B", "B", 0, "B", "B", 0, true, 1);
            DocClass b = new DocClass("b");
            
            st.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(ft.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(b.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(st.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.OverdueTime(ft.PersonID, b.ID) / 7 == 2);
        }

        public void Test5()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("st1", "st1", "st1", "st1", "st1", false);
            SDM.LMS.RegisterUser("st2", "st2", "st2", "st2", "st2", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Student st1 = new Student("st1");
            Student st2 = new Student("st2");
            Librarian lb = new Librarian("lb");

            lb.AddAV("a", "a", 0, 2);
            DocClass a = new DocClass("a");
            
            st.CheckOut(a.ID);
            st1.CheckOut(a.ID);
            st2.CheckOut(a.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st1.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st2.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(a.ID) != null);
            Debug.Assert(a.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(st.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.GetUserBooks(st1.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.GetUserBooks(st2.PersonID).Count == 0);
        }

        public void Test6()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Librarian lb = new Librarian("lb");
            
            lb.AddAV("b", "B", 0, 2);
            DocClass b = new DocClass("b");
            
            st.CheckOut(b.ID);
            st.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(b.Quantity == 1);
            Debug.Assert(SDM.LMS.GetUserBooks(st.PersonID).Count == 1);
        }

        public void Test7()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("p1", "p1", "p1", "p1", "p1", false);
            SDM.LMS.RegisterUser("p2", "p2", "p2", "p2", "p2", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student p1 = new Student("p1");
            Student p2 = new Student("p2");
            Librarian lb = new Librarian("lb");
            
            lb.AddAV("b1", "B", 0, 2);
            DocClass b1 = new DocClass("b1");
            
            p1.CheckOut(b1.ID);
            p2.CheckOut(b1.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(p1.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(p2.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b1.ID) != null);
            Debug.Assert(b1.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(p1.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.GetUserBooks(p2.PersonID).Count == 1);
        }

        public void Test8()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("f", "f", "f", "f", "f", false);
            SDM.LMS.RegisterUser("s", "s", "s", "s", "s", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Faculty f = new Faculty("f");
            Student s = new Student("s");
            Librarian lb = new Librarian("lb");
            
            lb.AddBook("b", "B", "B", 0, "B", "B", 0, false, 1);
            DocClass b = new DocClass("b");
            
            s.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(f.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(s.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(b.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(s.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.OverdueTime(s.PersonID, b.ID) / 7 == 3);
        }

        public void Test9()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("f", "f", "f", "f", "f", false);
            SDM.LMS.RegisterUser("s", "s", "s", "s", "s", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Faculty f = new Faculty("f");
            Student s = new Student("s");
            Librarian lb = new Librarian("lb");
            
            lb.AddBook("b", "B", "B", 0, "B", "B", 0, true, 1);
            DocClass b = new DocClass("b");
            
            s.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(f.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(s.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(b.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(s.PersonID).Count == 1);
            Debug.Assert(SDM.LMS.OverdueTime(s.PersonID, b.ID) / 7 == 2);
        }

        public void Test10()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("st", "st", "st", "st", "st", false);
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Student st = new Student("st");
            Librarian lb = new Librarian("lb");
            
            lb.AddBook("b", "B", "B", 0, "B", "B", 0, false, 1);
            lb.AddBook("a", "A", "A", 0, "A", "A", 0, false, 0);
            DocClass b = new DocClass("b");
            DocClass a = new DocClass("a");
            
            st.CheckOut(a.ID);
            st.CheckOut(b.ID);

            Debug.Assert(SDM.LMS.GetUser(lb.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(st.PersonID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(a.ID) != null);
            Debug.Assert(b.Quantity == 0);
            Debug.Assert(a.Quantity == 0);
            Debug.Assert(SDM.LMS.GetUserBooks(st.PersonID).Count == 1);
        }

        public void Test11()
        {
            SDM.LMS.ClearDB();
            
            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Librarian lb = new Librarian("lb");

            lb.AddBook
                (
                    "Introduction to Algorithms",
                    "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest and Clifford Stein",
                    "MIT Press",
                    2009,
                    "Third Edition",
                    "Alghorithm techniques and design",
                    1800,
                    false,
                    3
                );
            lb.AddBook
                (
                    "Design Patterns: Elements of Reusable Object-Oriented Software",
                    "Erich Gamma, Ralph Johnson, John Vlissides, Richard Helm",
                    "Addison-Wesley Professional",
                    2003,
                    "First Edition",
                    "Programm patterns, how to programm well w/o headache",
                    2000,
                    true,
                    2
                );
            lb.AddBook
                (
                    "The Mythical Man-month",
                    "Brooks,Jr., Frederick P",
                    "Addison-Wesley Longman Publishing Co., Inc.",
                    1995,
                    "Second edition",
                    "How to do everything and live better",
                    800,
                    false,
                    1
                );
            lb.AddAV("Null References: The Billion Dollar Mistake", "Tony Hoare", 400, 1);
            lb.AddAV("Information Entropy", "Claude Shannon", 700, 1);
            DocClass b1 = new DocClass("Introduction to Algorithms");
            DocClass b2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");
            DocClass b3 = new DocClass("The Mythical Man-month");
            DocClass av1 = new DocClass("Null References: The Billion Dollar Mistake");
            DocClass av2 = new DocClass("Information Entropy");

            lb.RegisterUser("Sergey Afonso", "Sergey Afonso", "Sergey Afonso", "Via Margutta, 3", "30001", false);
            lb.RegisterUser("Nadia Teixeira", "Nadia Teixeira", "Nadia Teixeira", "Via Sacra, 13", "30002", false);
            lb.RegisterUser("Elvira Espindola", "Elvira Espindola", "Elvira Espindola", "Via del Corso, 22", "30003", false);
            Student p1 = new Student("Sergey Afonso");
            Student p2 = new Student("Nadia Teixeira");
            Student p3 = new Student("Elvira Espindola");

            lb.ModifyUser(p1.PersonID, p1.Name, p1.Adress, p1.PhoneNumber, p1.UserType + 1);

            Debug.Assert(SDM.LMS.GetDoc(b1.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b2.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(b3.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(av1.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(av2.ID) != null);

            Debug.Assert(b1.Quantity == 3);
            Debug.Assert(b2.Quantity == 2);
            Debug.Assert(b3.Quantity == 1);
            Debug.Assert(av1.Quantity == 1);
            Debug.Assert(av2.Quantity == 1);

            Debug.Assert(SDM.LMS.GetUser(p1.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(p2.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(p3.PersonID) != null);
        }

        public void Test12()
        {
            Test11();
            
            Librarian lb = new Librarian("lb");
            Student p2 = new Student("Nadia Teixeira");

            DocClass b1 = new DocClass("Introduction to Algorithms");
            DocClass b3 = new DocClass("The Mythical Man-month");
            
            lb.ModifyAV(b1.ID, b1.Title, b1.Autors, b1.Price, b1.Quantity - 2);
            lb.ModifyAV(b3.ID, b3.Title, b3.Autors, b3.Price, b3.Quantity - 1);
            lb.DeleteUser(p2.PersonID);

            Debug.Assert(SDM.LMS.GetUser(p2.PersonID) == null);
            Debug.Assert(b1.Quantity == 1);
            Debug.Assert(b3.Quantity == 0);
        }

        public void Test13()
        {
            Test11();
            
            Librarian lb = new Librarian("lb");
            Faculty p1 = new Faculty("Sergey Afonso");
            Student p3 = new Student("Elvira Espindola");

            lb.ShowUserCard(p1.PersonID);
            lb.ShowUserCard(p3.PersonID);
            
            // TODO Под вопросом
            List<CheckedOut> CheckedOutInfo = new List<CheckedOut>();
            Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 1, CheckedOutInfo));
            Debug.Assert(SDM.LMS.CheckUserInfo("Elvira Espindola", "Via del Corso, 22", "30003", 0, CheckedOutInfo));
        }

        public void Test14()
        {
            Test12();

            Librarian lb = new Librarian("lb");
            Student p2 = new Student("Nadia Teixeira");
            Student p3 = new Student("Elvira Espindola");

            lb.ShowUserCard(p2.PersonID);
            lb.ShowUserCard(p3.PersonID);

            List<CheckedOut> CheckedOutInfo = new List<CheckedOut>();
            Debug.Assert(SDM.LMS.CheckUserInfo("Elvira Espindola", "Via del Corso, 22", "30003", 0, CheckedOutInfo));
            Debug.Assert(SDM.LMS.GetUser(p2.PersonID) == null);
        }

        public void Test15()
        {
            Test12();
            
            Student p2 = new Student("Nadia Teixeira");

            DocClass b1 = new DocClass("Introduction to Algorithms");

            p2.CheckOut(b1.ID);

            Debug.Assert(SDM.LMS.GetUser(p2.PersonID) == null);
        }

        public void Test16()
        {
            Test11();

            Librarian lb = new Librarian("lb");
            Faculty p1 = new Faculty("Sergey Afonso");
            Student p3 = new Student("Elvira Espindola");

            DocClass b1 = new DocClass("Introduction to Algorithms");
            DocClass b2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");

            p1.CheckOut(b1.ID);
            p3.CheckOut(b1.ID);
            p1.CheckOut(b2.ID);

            lb.ShowUserCard(p1.PersonID);
            lb.ShowUserCard(p3.PersonID);

            List<CheckedOut> CheckedOutInfo = new List<CheckedOut>
            {
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(21).Day, DocumentCheckedOut = "Introduction to Algorithms" }
            };
            Debug.Assert(SDM.LMS.CheckUserInfo("Elvira Espindola", "Via del Corso, 22", "30003", 0, CheckedOutInfo));

            CheckedOutInfo = new List<CheckedOut>
            {
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(28).Day, DocumentCheckedOut = "Introduction to Algorithms" },
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(28).Day, DocumentCheckedOut = "Design Patterns: Elements of Reusable Object-Oriented Software" }
            };
            Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 1, CheckedOutInfo));
        }

        public void Test17()
        {
            Test11();

            Librarian lb = new Librarian("lb");
            Faculty p1 = new Faculty("Sergey Afonso");
            Student p2 = new Student("Nadia Teixeira");

            DocClass b1 = new DocClass("Introduction to Algorithms");
            DocClass b2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");
            DocClass b3 = new DocClass("The Mythical Man-month");
            DocClass av1 = new DocClass("Null References: The Billion Dollar Mistake");
            DocClass av2 = new DocClass("Information Entropy");

            p1.CheckOut(b1.ID);
            p1.CheckOut(b2.ID);
            p1.CheckOut(b3.ID);
            p1.CheckOut(av1.ID);
            p2.CheckOut(b1.ID);
            p2.CheckOut(b2.ID);
            p2.CheckOut(av2.ID);
            
            lb.ShowUserCard(p1.PersonID);
            lb.ShowUserCard(p2.PersonID);

            List<CheckedOut> CheckedOutInfo = new List<CheckedOut>
            {
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(21).Day, DocumentCheckedOut = "Introduction to Algorithms" },
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(14).Day, DocumentCheckedOut = "Design Patterns: Elements of Reusable Object-Oriented Software" },
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(14).Day, DocumentCheckedOut = "Information Entropy" }
            };
            Debug.Assert(SDM.LMS.CheckUserInfo("Nadia Teixeira", "Via Sacra, 13", "30002", 0, CheckedOutInfo));

            CheckedOutInfo = new List<CheckedOut>
            {
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(28).Day, DocumentCheckedOut = "Introduction to Algorithms" },
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(28).Day, DocumentCheckedOut = "Design Patterns: Elements of Reusable Object-Oriented Software" },
                new CheckedOut { CheckOutTime = DateTime.Now.AddDays(14).Day, DocumentCheckedOut = "Null References: The Billion Dollar Mistake" }
            };
            Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 1, CheckedOutInfo));
        }

        public void Test18()
        {
            Test11();

            Librarian lb = new Librarian("lb");
            Faculty p1 = new Faculty("Sergey Afonso");
            Student p2 = new Student("Nadia Teixeira");

            DocClass b1 = new DocClass("Introduction to Algorithms");
            DocClass b2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");
            DocClass av1 = new DocClass("Null References: The Billion Dollar Mistake");

            p1.CheckOut(b1.ID, new int[] { 09, 02, 2018 });
            p1.CheckOut(b2.ID, new int[] { 02, 02, 2018 });
            p2.CheckOut(b1.ID, new int[] { 05, 02, 2018 });
            p2.CheckOut(av1.ID, new int[] { 17, 02, 2018 });
            
            lb.ShowOverdue(p1.PersonID);
            lb.ShowOverdue(p2.PersonID);
            
            DateTime timeToBack = new DateTime(2018, 02, 05).AddDays(21);
            List<OverdueInfo> overdueInfos = new List<OverdueInfo>
            {
                new OverdueInfo { Overdue = (int)DateTime.Now.Subtract(timeToBack).TotalDays, DocumentChekedOut = "Introduction to Algorithms" },
                new OverdueInfo { Overdue = (int)DateTime.Now.Subtract(timeToBack).TotalDays, DocumentChekedOut = "Null References: The Billion Dollar Mistake" }
            };
            Debug.Assert(SDM.LMS.CheckUserInfo("Nadia Teixeira", "Via Sacra, 13", "30002", 0, overdueInfos));

            timeToBack = new DateTime(2018, 02, 02).AddDays(28);
            overdueInfos = new List<OverdueInfo>()
            {
                new OverdueInfo { Overdue = (int)DateTime.Now.Subtract(timeToBack).TotalDays, DocumentChekedOut = "Introduction to Algorithms" }
            };
            Debug.Assert(SDM.LMS.CheckUserInfo("Sergey Afonso", "Via Margutta, 3", "30001", 1, overdueInfos));
        }

        public void Test19()
        {
            Test11();
            Environment.Exit(0);
        }

        public void Initial()
        {
            SDM.LMS.ClearDB();

            SDM.LMS.RegisterUser("lb", "lb", "lb", "lb", "lb", true);
            Librarian lb = new Librarian("lb");

            lb.AddBook
                (
                    "Introduction to Algorithms",
                    "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest and Clifford Stein",
                    "MIT Press",
                    2009,
                    "Third Edition",
                    "Alghorithm techniques and design",
                    5000,
                    false,
                    3
                );
            lb.AddBook
                (
                    "Design Patterns: Elements of Reusable Object-Oriented Software",
                    "Erich Gamma, Ralph Johnson, John Vlissides, Richard Helm",
                    "Addison-Wesley Professional",
                    2003,
                    "First Edition",
                    "Programm patterns, how to programm well w/o headache",
                    1700,
                    true,
                    3
                );
            lb.AddAV("Null References: The Billion Dollar Mistake", "Tony Hoare", 700, 2);
            DocClass d1 = new DocClass("Introduction to Algorithms");
            DocClass d2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");
            DocClass d3 = new DocClass("Null References: The Billion Dollar Mistake");
            
            lb.RegisterUser("p1", "p1", "p1", "Via Margutta, 3", "30001", false);
            lb.RegisterUser("p2", "p2", "p2", "Via Sacra, 13", "30002", false);
            lb.RegisterUser("p3", "p3", "p3", "Via del Corso, 22", "30003", false);
            lb.RegisterUser("s", "s", "s", "s", "s", false);
            lb.RegisterUser("v", "v", "v", "v", "v", false);
            Student p1 = new Student("p1");
            Student p2 = new Student("p2");
            Student p3 = new Student("p3");
            Student s = new Student("s");
            Student v = new Student("v");

            lb.ModifyUser(p1.PersonID, p1.Name, p1.Adress, p1.PhoneNumber, 4);
            lb.ModifyUser(p2.PersonID, p2.Name, p2.Adress, p2.PhoneNumber, 4);
            lb.ModifyUser(p3.PersonID, p3.Name, p3.Adress, p3.PhoneNumber, 4);
            lb.ModifyUser(v.PersonID, v.Name, v.Adress, v.PhoneNumber, 3);

            Debug.Assert(SDM.LMS.GetDoc(d1.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(d2.ID) != null);
            Debug.Assert(SDM.LMS.GetDoc(d3.ID) != null);

            Debug.Assert(d1.Quantity == 3);
            Debug.Assert(d2.Quantity == 3);
            Debug.Assert(d3.Quantity == 2);

            Debug.Assert(SDM.LMS.GetUser(p1.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(p2.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(p3.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(s.PersonID) != null);
            Debug.Assert(SDM.LMS.GetUser(v.PersonID) != null);
        }

        public void Test20()
        {
            Initial();

            Faculty p1 = new Faculty("p1");

            DocClass d1 = new DocClass("Introduction to Algorithms");
            DocClass d2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");

            p1.CheckOut(d1.ID, new int[] { 07, 03, 2018 });
            p1.CheckOut(d2.ID, new int[] { 07, 03, 2018 });
            p1.ReturnDoc(d2.ID);

            Debug.Assert(SDM.LMS.GetUserFineForDoc(p1.PersonID, d1.ID) == 100);
        }

        public void Test21()
        {
            Initial();
            int[] timeCheat = { 07, 03, 2018 };

            Faculty p1 = new Faculty("p1");
            Student s = new Student("s");
            VisitingProfessor v = new VisitingProfessor("v");

            DocClass d1 = new DocClass("Introduction to Algorithms");
            DocClass d2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");

            p1.CheckOut(d1.ID, timeCheat);
            p1.CheckOut(d2.ID, timeCheat);
            s.CheckOut(d1.ID, timeCheat);
            s.CheckOut(d2.ID, timeCheat);
            v.CheckOut(d1.ID, timeCheat);
            v.CheckOut(d2.ID, timeCheat);

            Debug.Assert(SDM.LMS.OverdueTime(p1.PersonID, d1.ID) == 1);
            Debug.Assert(SDM.LMS.OverdueTime(p1.PersonID, d2.ID) == 1);
            Debug.Assert(SDM.LMS.GetUserFineForDoc(p1.PersonID, d1.ID) == 100);
            Debug.Assert(SDM.LMS.GetUserFineForDoc(p1.PersonID, d2.ID) == 100);

            Debug.Assert(SDM.LMS.OverdueTime(s.PersonID, d1.ID) == 8);
            Debug.Assert(SDM.LMS.OverdueTime(s.PersonID, d2.ID) == 15);
            Debug.Assert(SDM.LMS.GetUserFineForDoc(s.PersonID, d1.ID) == 800);
            Debug.Assert(SDM.LMS.GetUserFineForDoc(s.PersonID, d2.ID) == 1500);

            Debug.Assert(SDM.LMS.OverdueTime(v.PersonID, d1.ID) == 22);
            Debug.Assert(SDM.LMS.OverdueTime(v.PersonID, d2.ID) == 22);
            Debug.Assert(SDM.LMS.GetUserFineForDoc(v.PersonID, d1.ID) == 2200);
            Debug.Assert(SDM.LMS.GetUserFineForDoc(v.PersonID, d2.ID) == 1700);
        }

        public void Test22()
        {
            Initial();
            int[] timeCheat = { 02, 04, 2018 };
            
            Faculty p1 = new Faculty("p1");
            Student s = new Student("s");
            VisitingProfessor v = new VisitingProfessor("v");

            DocClass d1 = new DocClass("Introduction to Algorithms");
            DocClass d2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");

            p1.CheckOut(d1.ID, timeCheat);
            s.CheckOut(d2.ID, timeCheat);
            v.CheckOut(d2.ID, timeCheat);

            p1.RenewDoc(d1.ID, timeCheat);
            s.RenewDoc(d2.ID, timeCheat);
            v.RenewDoc(d2.ID, timeCheat);
            
            Debug.Assert(SDM.LMS.GetCheckout(p1.PersonID, d1.ID).TimeToBack.Day == 30);
            Debug.Assert(SDM.LMS.GetCheckout(s.PersonID, d2.ID).TimeToBack.Day == 16);
            Debug.Assert(SDM.LMS.GetCheckout(v.PersonID, d2.ID).TimeToBack.Day == 9);
        }

        public void Test23()
        {
            Initial();
            int[] timeCheat = { 31, 03, 2018 };

            Librarian lb = new Librarian("lb");
            Faculty p1 = new Faculty("p1");
            Student s = new Student("s");
            VisitingProfessor v = new VisitingProfessor("v");

            DocClass d1 = new DocClass("Introduction to Algorithms");
            DocClass d2 = new DocClass("Design Patterns: Elements of Reusable Object-Oriented Software");

            p1.CheckOut(d1.ID, timeCheat);
            s.CheckOut(d2.ID, timeCheat);
            v.CheckOut(d2.ID, timeCheat);
            lb.OutstandingRequest(d2.ID);

            timeCheat = new int[] { 02, 04, 2018 };
            p1.RenewDoc(d1.ID,timeCheat);
            s.RenewDoc(d2.ID);
            v.RenewDoc(d2.ID);

            Debug.Assert(SDM.LMS.GetCheckout(p1.PersonID, d1.ID).TimeToBack.Day == 30);
            Debug.Assert(SDM.LMS.GetCheckout(s.PersonID, d2.ID).TimeToBack.Day == DateTime.Now.Day);
            Debug.Assert(SDM.LMS.GetCheckout(v.PersonID, d2.ID).TimeToBack.Day == DateTime.Now.Day);
        }


        public void Test25()
        {
            Initial();
            int[] dateCheat = { 02, 04, 2018 };

            Faculty p1 = new Faculty("p1");
            Student s = new Student("s");
            VisitingProfessor v = new VisitingProfessor("v");

            DocClass d3 = new DocClass("Null References: The Billion Dollar Mistake");

            p1.CheckOut(d3.ID, dateCheat);
            s.CheckOut(d3.ID, dateCheat);
            v.CheckOut(d3.ID, dateCheat);

            PriorityQueue<int> pq = SDM.LMS.LoadPQ(d3.ID);
            Debug.Assert(pq.Pop() == v.PersonID);
        }

        public void Test26()
        {
            Initial();
            int[] dateCheat = { 02, 04, 2018 };

            Student p1 = new Student("p1");
            Student p2 = new Student("p2");
            Student p3 = new Student("p3");
            Student s = new Student("s");
            Student v = new Student("v");

            DocClass d3 = new DocClass("Null References: The Billion Dollar Mistake");

            p1.CheckOut(d3.ID,dateCheat);
            p2.CheckOut(d3.ID, dateCheat);
            s.CheckOut(d3.ID, dateCheat);
            v.CheckOut(d3.ID, dateCheat);
            p3.CheckOut(d3.ID, dateCheat);

            PriorityQueue<int> pq = SDM.LMS.LoadPQ(d3.ID);
            Debug.Assert(pq.Pop() == s.PersonID);
            Debug.Assert(pq.Pop() == v.PersonID);
            Debug.Assert(pq.Pop() == p3.PersonID);
        }

        public void Test27()
        {
            Test26();
            
            Librarian lb = new Librarian("lb");

            DocClass d3 = new DocClass("Null References: The Billion Dollar Mistake");
            
            lb.OutstandingRequest(d3.ID);

            Debug.Assert(SDM.LMS.ExistQueueForDoc(d3.ID));
        }

        public void Test28()
        {
            Test26();
            
            Faculty p2 = new Faculty("p2");
            Student p3 = new Student("p3");
            Student s = new Student("s");
            Student v = new Student("v");

            DocClass d3 = new DocClass("Null References: The Billion Dollar Mistake");

            p2.ReturnDoc(d3.ID);

            PriorityQueue<int> pq = SDM.LMS.LoadPQ(d3.ID);
            Debug.Assert(pq.Pop() == s.PersonID);
            Debug.Assert(pq.Pop() == v.PersonID);
            Debug.Assert(pq.Pop() == p3.PersonID);
            List<CheckedOut> checkedOuts = SDM.LMS.GetCheckoutsList("p2");
            Debug.Assert(checkedOuts.Capacity == 0);
        }

        public void Test29()
        {
            Test26();
            int[] dateCheat = { 02, 04, 2018 };

            Faculty p1 = new Faculty("p1");
            Faculty p2 = new Faculty("p2");
            Student p3 = new Student("p3");
            Student s = new Student("s");
            Student v = new Student("v");

            DocClass d3 = new DocClass("Null References: The Billion Dollar Mistake");
            
            p1.RenewDoc(d3.ID,dateCheat);

            List<CheckedOut> checkedOuts = SDM.LMS.GetCheckoutsList("p1");
            Debug.Assert(checkedOuts.First().CheckOutTime == 16);
            Debug.Assert(checkedOuts.First().DocumentCheckedOut == "d3");
            PriorityQueue<int> pq = SDM.LMS.LoadPQ(d3.ID);
            Debug.Assert(pq.Pop() == s.PersonID);
            Debug.Assert(pq.Pop() == v.PersonID);
            Debug.Assert(pq.Pop() == p3.PersonID);
        }

        public void Test30()
        {
            Initial();
            
            Faculty p1 = new Faculty("p1");
            VisitingProfessor v = new VisitingProfessor("v");

            DocClass d1 = new DocClass("Introduction to Algorithms");

            p1.CheckOut(d1.ID, new int[] { 26, 03, 2018 });
            p1.RenewDoc(d1.ID, new int[] { 29, 03, 2018 });
            v.CheckOut(d1.ID, new int[] { 29, 03, 2018 });
            v.RenewDoc(d1.ID, new int[] { 30, 03, 2018 });

            List<CheckedOut> checkedOuts = SDM.LMS.GetCheckoutsList("p1");
            Debug.Assert(checkedOuts.First().CheckOutTime == 26);
            Debug.Assert(checkedOuts.First().DocumentCheckedOut == "d1");
            checkedOuts = SDM.LMS.GetCheckoutsList("v");
            Debug.Assert(checkedOuts.First().CheckOutTime == 6);
            Debug.Assert(checkedOuts.First().DocumentCheckedOut == "d1");
        }
    }
}