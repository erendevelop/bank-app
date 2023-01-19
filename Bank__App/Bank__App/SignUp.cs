using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__App
{
    public partial class SignUp : Form
    {
        Random rand = new Random();
        int number;
        string resimler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\resimler.txt";
        string adlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\adlar.txt";
        string soyadlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\soyadlar.txt";
        string epostalar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\epostalar.txt";
        string telefonlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\telefonlar.txt";
        string bakiyeler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\bakiyeler.txt";
        string sifreler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\sifreler.txt";
        string harcamalar = "";
        public SignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length != 0 & textBox2.Text.Length != 0 & textBox3.Text.Length != 0 & textBox4.Text.Length != 0 & textBox5.Text.Length != 0)
            {
                if(Regex.IsMatch(textBox1.Text, @"^[a-zA-Z]+$") & Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$"))
                {
                    if (Regex.IsMatch(textBox3.Text, @"^[0-9]+$") & textBox3.Text.Length == 11 & Int32.Parse(textBox3.Text.ToString().Substring(0, 1)) == 0)
                    {
                        if (textBox4.Text.ToString().Contains("@gmail.com") & textBox4.Text.Length <= 40 & textBox4.Text.Length >= 12)
                        {
                            if(textBox5.Text.Length >= 5 & textBox5.Text.Length <= 11)
                            {
                                int gec = 0;
                                foreach (string line in System.IO.File.ReadLines(@epostalar))
                                {
                                    if(line.Equals(textBox4.Text)){
                                        MessageBox.Show("Bu epostaya bağlı bir hesap zaten var.");
                                        textBox4.BackColor = Color.Red;
                                        System.Threading.Thread.Sleep(100);
                                        textBox4.BackColor = Color.White;
                                        gec = 1;
                                        break;
                                    }
                                }
                                if(gec == 0)
                                {
                                    StreamWriter adwriter = new StreamWriter(@adlar, true);
                                    StreamWriter soyadwriter = new StreamWriter(@soyadlar, true);
                                    StreamWriter epostawriter = new StreamWriter(@epostalar, true);
                                    StreamWriter telefonwriter = new StreamWriter(@telefonlar, true);
                                    StreamWriter bakiyewriter = new StreamWriter(@bakiyeler, true);
                                    StreamWriter sifrewriter = new StreamWriter(@sifreler, true);
                                    StreamWriter resimwriter = new StreamWriter(@resimler, true);
                                    string harcama= $"C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\sonHarcamalar\\{textBox4.Text}.txt";
                                    StreamWriter harcamawriter = new StreamWriter(harcama, true);
                                    adwriter.Write(textBox1.Text + "\n");
                                    adwriter.Close();      
                                    soyadwriter.Write(textBox2.Text + "\n");
                                    soyadwriter.Close();
                                    telefonwriter.Write(textBox3.Text + "\n");
                                    telefonwriter.Close();
                                    epostawriter.Write(textBox4.Text + "\n");
                                    epostawriter.Close();
                                    sifrewriter.Write(textBox5.Text + "\n");
                                    sifrewriter.Close();
                                    number = rand.Next(2000, 10000) + 5000; 
                                    bakiyewriter.Write(number.ToString() + "\n");
                                    bakiyewriter.Close();
                                    resimwriter.Write("x" + "\n");
                                    resimwriter.Close();
                                    MessageBox.Show("Hesabınız oluşturulmuştur.");
                                    this.Close();
                                    new LogInScreen().Show();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Şifreniz çok uzun ya da kısa. Lütfen 4'ten uzun ve 12'den kısa bir şifre giriniz.");
                                textBox5.BackColor = Color.Red;
                                System.Threading.Thread.Sleep(100);
                                textBox5.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Böyle bir mail bulunamadı.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Telefon numarasını başında 0 olacak şekilde rakamlarla yazınız. örn: 0123 456 78 90");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen ad ve soyadınızı düzeltiniz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LogInScreen().Show();
        }
    }
}
