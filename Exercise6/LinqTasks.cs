using Exercise6.Models;

namespace Exercise6
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            #region Load depts

            List<Dept> depts =
            [
                new Dept
                {
                    Deptno = 1,
                    Dname = "Research",
                    Loc = "Warsaw"
                },
                new Dept
                {
                    Deptno = 2,
                    Dname = "Human Resources",
                    Loc = "New York"
                },
                new Dept
                {
                    Deptno = 3,
                    Dname = "IT",
                    Loc = "Los Angeles"
                }
            ];

            Depts = depts;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            List<Emp> emps =
            [
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10
            ];

            Emps = emps;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
            // Method syntax
            var methodSyntax =
                Emps
                    .Where(e => e.Job == "Backend programmer");
            
            // Query syntax
            var querySyntax =
                from e in Emps
                where e.Job.Equals("Backend programmer")
                select e;
            IEnumerable<Emp> result = querySyntax;     
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            // Method syntax
            var methodSyntax =
                Emps
                    .Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                    .OrderBy(emp => emp.Ename);
            
            // Query syntax
            var querySyntax =
                from e in Emps
                where e.Job.Equals("Frontend programmer") && e.Salary > 1000
                orderby e.Ename descending
                select e;
            
            IEnumerable<Emp> result = querySyntax;
            return result;
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            var methodSyntax = Emps
                .Max(emp => emp.Salary);

            var querySyntax =
                (from e in Emps
                    select e.Salary).Max();
                
            int result = querySyntax;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            var methodSyntax = Emps
                .Where(emp => emp.Salary == Emps
                    .Max(emp => emp.Salary))
                .Select(e => e);

            var querySyntax =
                from e in Emps
                where e.Salary == (from emp in Emps select emp.Salary).Max()
                select e;
            
            IEnumerable<Emp> result = querySyntax;
            return result;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            // Method syntax
            var methodSyntax = 
                Emps
                    .Select(e => new { Nazwisko = e.Ename,
                                            Praca = e.Job });
            
            // Query syntax
            var querySyntax =
                from e in Emps
                select new { Nazwisko = e.Ename, Praca = e.Job };
            
            IEnumerable<object> result = querySyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            var methodSyntax = Emps
                .Join(Depts, 
                    emp => emp.Deptno,
                    dept => dept.Deptno,
                    (emp, dept) => new
                    {
                        emp.Ename,
                        emp.Job,
                        dept.Dname
                    })
                .Select(e => new
                {
                    ename = e.Ename,
                    job = e.Job,
                    dname = e.Dname
                });
                
            
            var querySyntax =
                from e in Emps
                join d in Depts on e.Deptno equals d.Deptno
                select new { e.Ename, e.Job, d.Dname };

            IEnumerable<object> result = querySyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            var methodSyntax = Emps
                .GroupBy(emp => emp.Job)
                .Select(group => new
                {
                    Praca = group.Key,
                    LiczbaPracownikow = group.Count()
                });

            var querySyntax =
                from e in Emps
                group e by e.Job into g
                select new { Praca = g.Key, LiczbaPracownikow = g.Count() };

            IEnumerable<object> result = querySyntax;
            return result;
        }

        /// <summary>
        ///     Zwróć wartość "true" jeśli choć jeden
        ///     z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            var methodSyntax = Emps
                .Any(e => e.Job == "Backend programmer");

            var querySyntax = (from e in Emps
                where e.Job == "Backend programmer"
                select e).Count();
            bool result = querySyntax > 0 ? true:false ;
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            var methodSyntax = Emps
                .Where(e => e.Job == "Frontend programmer").MaxBy(e => e.HireDate);

            var querySyntax = (from e in Emps
                where e.Job == "Frontend programmer"
                orderby e.HireDate descending
                select e).FirstOrDefault();

            Emp result = querySyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            var methodSyntax = Emps
                .

            var querySyntax = (from e in Emps
                    select new { e.Ename, e.Job, e.HireDate }).Union(
                    from e in new[] { new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null } }
                    select e
        );

        IEnumerable<object> result = null;
            return result;
        }

        /// <summary>
        /// Wykorzystując LINQ pobierz pracowników podzielony na departamenty pamiętając, że:
        /// 1. Interesują nas tylko departamenty z liczbą pracowników powyżej 1
        /// 2. Chcemy zwrócić listę obiektów o następującej srukturze:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Wykorzystaj typy anonimowe
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            var querySyntax = from e in Emps
                group e by e.Deptno
                into gr
                where gr.Count() > 1
                select new
                {
                    name = (from d in Depts
                        where d.Deptno == gr.Key
                        select d.Dname),
                    empNum = gr.Count()
                };
                                

            IEnumerable<object> result = null;
            return result;
        }

        /// <summary>
        /// Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
        /// Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
        /// 
        /// Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
        /// Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {

            var querySyntax =
                from e in Emps
                join emp in Emps on e.Empno equals emp.Mgr
                orderby e.Ename
                select e;
                            
                        
            IEnumerable<Emp> result = querySyntax;
            return result;
        }

        /// <summary>
        /// Poniższa metoda powinna zwracać pojedyczną liczbę int.
        /// Na wejściu przyjmujemy listę liczb całkowitych.
        /// Spróbuj z pomocą LINQ'a odnaleźć tę liczbę, które występuja w tablicy int'ów nieparzystą liczbę razy.
        /// Zakładamy, że zawsze będzie jedna taka liczba.
        /// Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static int Task13(int[] arr)
        {
            int result = arr
                .GroupBy(n => n) 
                .Where(g => g.Count() % 2 != 0)
                .Select(g => g.Key)
                .Single();

            return result;
        }

        /// <summary>
        /// Zwróć tylko te departamenty, które mają 5 pracowników lub nie mają pracowników w ogóle.
        /// Posortuj rezultat po nazwie departament rosnąco.
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            IEnumerable<Dept> result = null;
            //result =
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        //Put your extension methods here
    }
}