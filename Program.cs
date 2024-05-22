using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Numerics;
using System.Collections.ObjectModel;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            static simitinfo getUrl(string url)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");
                simitinfo simitinfo = new simitinfo();
                using (IWebDriver driver = new ChromeDriver(options))
                {
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(30000);
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
                    var resumen = driver.FindElements(By.CssSelector("div#resumenEstadoCuenta"));
                    var tableinfo = driver.FindElements(By.CssSelector("table#multaTable"));
                    var table = driver.FindElement(By.Id("multaTable"));
                    List<string> list = OrganizeInfo(resumen);
                    List<string> resultable = OrganizeInfo(resumen);
                    simitinfo.Comparisons = list[1];
                    simitinfo.Fines = list[2];
                    simitinfo.PaidAgreements = list[3];
                    simitinfo.Total = list[4];
                    DateTime date = DateTime.Now;
                    simitinfo.Date= date;
                    //foreach(string dt  in resultable){
                    //    DetailslicensePlate detailslicense = new DetailslicensePlate();
                    //    detailslicense.Type = dt;
                    //}

                    // Obtén todas las filas del cuerpo de la tabla
                    //var rows = table.FindElements(By.CssSelector("tbody tr"));

                    //// Itera sobre las filas y extrae la información
                    //foreach (var row in rows)
                    //{
                    //    var cells = row.FindElements(By.CssSelector("td"));
                    //    foreach (var cell in cells)
                    //    {
                    //        Console.Write(cell.Text + "\t");
                    //    }
                    //    Console.WriteLine();
                    //}

                    driver.Quit();
                    return simitinfo;
                }

            }
            static List<string> OrganizeInfo(ReadOnlyCollection<IWebElement> Data)
            {
                List<string> list = new List<string>();
                foreach (var title in Data)
                {
                    list = title.Text.ToString().Split("\n").ToList();
                }
                return list;
            }

            string Url = "https://fcm.org.co/simit/#/estado-cuenta?numDocPlacaProp=";
            Console.WriteLine("Ingrese Placa a Consultar");
            string placa = Console.ReadLine();
            if (!string.IsNullOrEmpty(placa))
            {
                placa = placa.Trim().ToUpper().Replace("-", "");
                simitinfo simitinfo = getUrl(Url + placa);
                Console.Clear();
                if(simitinfo !=null)
                {

                    Console.WriteLine(simitinfo.Comparisons);
                    Console.WriteLine(simitinfo.Fines);
                    Console.WriteLine(simitinfo.PaidAgreements);
                    Console.WriteLine(simitinfo.Total);
                }
                else
                {
                    Console.WriteLine("No cuenta con información");
                }
            }
            

        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }


    public  class simitinfo{
        public string Comparisons   { get;set;}
        public string Fines         { get;set;}
        public string PaidAgreements {get;set;}
        public string Total         {get;set;}
        public DateTime Date { get;set;}
            
        public List<DetailslicensePlate> detailslicensePlates { get;set;}
    }
    public class DetailslicensePlate
    {
        public string Type { get; set; }
        public string Notification { get; set; }
        public string licensePlate { get; set; }
        public string Secretary { get; set; }
        public string Infraction { get; set; }
        public string Status { get; set; }
        public string Value { get; set; }
    }
}
