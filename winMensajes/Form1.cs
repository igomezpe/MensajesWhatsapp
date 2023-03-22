using Microsoft.VisualBasic.FileIO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tecla = OpenQA.Selenium.Keys;

namespace winMensajes
{
    public partial class Form1 : Form
    {
        string localPath = Directory.GetCurrentDirectory();
        IWebDriver driver;
        bool bandera = true;
        public Form1()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            configurar();
        }

        void configurar()
        {
            //Leemos el chromedriver que esta en la misma direccion del programa
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", localPath + "/chromedriver.exe");
            //Inicializamos ls opciones de chrome
            ChromeOptions optionsGoo = new ChromeOptions();
            //Permitimos la propiedad no-sandbox para evitar problemas en linux
            optionsGoo.AddArguments("--no-sandbox");
            //Deshabilitamos las notificaciones
            optionsGoo.AddArguments("--disable-notifications");
            //Guardamos la sesion en la carpeta chromeWA
            optionsGoo.AddArguments("--user-data-dir=" + localPath + "\\chromeWA");
            //Instanciamos un nuevo chromedriver
            driver = new ChromeDriver(optionsGoo);
            //Abrimos WA
            driver.Url = "https://web.whatsapp.com";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            configurar();

        }

        void enviarMensaje(string numeroTelf,string mensaje)
        {
            string numero = "51" + numeroTelf;


            driver.Url = "https://web.whatsapp.com/send/?phone=" + numero + "&text&type=phone_number&app_absent=0";
            //Declaramos el tiempo de espera

            while (true)
            {
                if (bandera)
                {

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    //Escribimos el mensaje
                    driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p")).SendKeys(mensaje);
                    //Esperamos 1 segundo

                    //Precionamos enter
                    driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]/p")).SendKeys(Tecla.Enter);
                }
            }


            driver.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bandera = true;
            enviarMensaje(txtNumeroTelf.Text,txtMensaje.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            var list = cn.ListarObjetosBloqueados();

            MessageBox.Show(list.Count().ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bandera = false;
            driver.Quit();
        }
    }
}
