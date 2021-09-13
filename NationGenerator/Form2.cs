using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Keys = OpenQA.Selenium.Keys;
using RestSharp;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualBasic;
using System.IO;
using System.Linq;

//#GabboBestDev lol
namespace NationGenerator
{
    public partial class Form2 : Form
    {
        public static Random rand = new Random();
        bool Generating = false;
        public Form2(Form1 form1)
        {
            InitializeComponent();
        }
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        public static int GetRandomNumber(int min, int cap)
        {
            return rand.Next(min, cap);
        }

        public string[] GetRandomProxy()
        {
            try
            {
                TextBox textboxona = new TextBox();
                textboxona.Text = File.ReadAllLines("proxies.txt").ToString();
                return Strings.Split(textboxona.Lines[GetRandomNumber(0, textboxona.Lines.Length)], ":");
            }
            catch (Exception)
            {
            }
            return new string[] { };
        }

        void GetEmail()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            IWebDriver driver = new ChromeDriver(service);
            driver.Navigate().GoToUrl("https://temp-mail.org/en/");
            Thread.Sleep(7000);
            driver.FindElement(By.Id("click-to-copy")).Click();
            driver.Close();
        }

        void Gen()
        {
            while(!Generating)
            {
                try
                {
                    GetEmail();
                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;
                    IWebDriver driver = new ChromeDriver(service);
                    driver.Navigate().GoToUrl("https://www.discord.com/register");
                    string username = "";
                    string password = "";
                    if (metroRadioButton8.Checked)
                    {
                        if (guna2TextBox8.Text == "" || guna2TextBox8.Text == " ")
                        {
                            username = "Nation Generator";
                        }
                        else
                        {
                            username = guna2TextBox8.Text;
                        }
                    }
                    else if(metroRadioButton7.Checked)
                    {
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                        var stringChars = new char[8];
                        var random = new Random();

                        for (int i = 0; i < 4; i++)
                        {
                            stringChars[i] = chars[random.Next(chars.Length)];
                        }

                        var finalString = new String(stringChars);
                        username = finalString + " | Nation Generator";
                    }
                    if (guna2TextBox4.Text == "")
                    {
                        password = "N4t10nGenOnT0p";
                    }
                    else
                    {
                        password = guna2TextBox4.Text;
                    }
                    driver.FindElement(By.Name("email")).SendKeys(Keys.Control + "v");
                    driver.FindElement(By.Name("username")).SendKeys(username);
                    driver.FindElement(By.Name("password")).SendKeys(password);
                    driver.FindElement(By.ClassName("css-gvi9bl-control")).Click();
                    Actions action = new Actions(driver);
                    action.SendKeys("12");
                    action.SendKeys(Keys.Enter);
                    action.SendKeys("9");
                    action.SendKeys(Keys.Enter);
                    action.SendKeys("1990");
                    action.SendKeys(Keys.Enter);
                    action.Perform();
                    try
                    {
                        driver.FindElement(By.ClassName("input-3ITkQf")).Click();
                    }
                    catch (Exception)
                    {
                    }
                    driver.FindElement(By.ClassName("button-3k0cO7")).Click();
                    if(File.Exists(Environment.CurrentDirectory + "/accounts.txt"))
                    {
                        File.WriteAllText(Environment.CurrentDirectory + "/accounts.txt", Clipboard.GetText() + ":" + guna2TextBox8.Text + ":" + guna2TextBox4.Text);
                    }
                    else
                    {
                        File.Create(Environment.CurrentDirectory + "accounts.txt");
                        File.WriteAllText(Environment.CurrentDirectory + "/accounts.txt", Clipboard.GetText() + ":" + guna2TextBox8.Text + ":" + guna2TextBox4.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void guna2TrackBar2_Scroll_1(object sender, ScrollEventArgs e)
        {
            metroLabel6.Text = $"Threads ({guna2TrackBar2.Value})";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < guna2TrackBar2.Value; i++)
                {
                    Thread thread = new Thread(new ThreadStart(Gen));
                    thread.Start();
                }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
                Generating = false;
                new Thread(() => new Thread(Gen)).Abort();
        }
    }
}
