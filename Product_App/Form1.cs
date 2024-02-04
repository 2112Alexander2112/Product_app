using Product_App.Models;
using Product_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Product_App
{
    public partial class Form1 : Form
    {
        private readonly CatalogRepos _CalRep;
        private readonly ProductRepos _productRepo;

        private void DisplayCatalog()
        {   //1
            comboBox1.DataSource = null;
            comboBox1.DataSource = _CalRep.GetAllCatalog();
            comboBox1.DisplayMember = "Name";
            //2
            comboBox2.DataSource = null;
            comboBox2.DataSource = _CalRep.GetAllCatalog();
            comboBox2.DisplayMember = "Name";

        }
        public Form1()
        {
            InitializeComponent();
            _CalRep = new CatalogRepos();
            _productRepo = new ProductRepos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayCatalog();
        }

        //Додавання категорії
        private void Add_button_Click(object sender, EventArgs e)
        {
            string catalogName = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(catalogName))
            {
                MessageBox.Show("Ви не вказали назву нового департаменту!!");
                textBox1.Focus();
            }
            else
            {
                var catalog = new Catalog()
                {
                    Id = _CalRep.GetLastId() + 1,
                    Name = catalogName,
                };
                _CalRep.AddCatalog(catalog);
                textBox1.Clear();
               DisplayCatalog();
            }
        }
        
        // Очищення категорії
        private void Del_Button_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        // Завантаження знімку (Непрацює)
        private void Down_Button_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.SafeFileName;
                string initFilePath = openFileDialog.FileName;
                string desFilePath = @"..\..\Img\" + initFilePath;

                if (File.Exists(desFilePath))
                {
                    MessageBox.Show("Такий файл вже існує!");
                }
                else
                {
                    File.Copy(initFilePath, desFilePath);
                    pictureBox1.Image = Image.FromFile(desFilePath);
                }
            }
        }
        
        // Вивід продукції за категорією
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                int selectedId = (comboBox1.SelectedItem as Catalog).Id;
                listBox1.DataSource = _productRepo.GetProductByCatalog(selectedId);
                listBox1.DisplayMember = "Name";
            }
        }

        // Заповнення даних о продукції
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selProduct = listBox1.SelectedItem as Product;
            string photoPath = @"..\..\Img\" + selProduct.PhotoName;
            textBox2.Text = selProduct.Name;
            dateTimePicker1.Value = selProduct.DateTime1;
            dateTimePicker2.Value = selProduct.DateTime2;
            numericUpDown1.Value = selProduct.Party;
            numericUpDown2.Value = selProduct.Count;
            pictureBox1.Image = Image.FromFile(photoPath);
        }

        // Додавання продукції у каталог
        private void button1_Click(object sender, EventArgs e)
        {
            string productName = textBox2.Text.Trim();
            var date1 = dateTimePicker1.Value;
            var date2 = dateTimePicker2.Value;
            var party = numericUpDown1.Value;
            var count = numericUpDown2.Value;
            if (string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Ошибка!!");
                textBox1.Focus();
            }
            else
            {
                int selectedId = (comboBox2.SelectedItem as Catalog).Id;
                var product = new Product()
                {
                    Id = _productRepo.GetLastId() + 1,
                    IdCatalog = selectedId,
                    Name = productName,
                    DateTime1 = date1,
                    DateTime2 = date2,
                    Party = Convert.ToInt32(party),
                    Count = Convert.ToInt32(count),
                    PhotoName = "question.png"
                };
                _productRepo.AddProduct(product);
                DisplayCatalog();
            }
        }

        // Очистка полів продукції
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            dateTimePicker1.MinDate = DateTime.MinValue;
            dateTimePicker2.MinDate = DateTime.MinValue;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }
        // Редагування продукції
        private void button3_Click(object sender, EventArgs e)
        {
            var selProduct = listBox1.SelectedItem as Product;
            string productName = textBox2.Text.Trim();
            var date1 = dateTimePicker1.Value;
            var date2 = dateTimePicker2.Value;
            var party = numericUpDown1.Value;
            var count = numericUpDown2.Value;
            int selectedId = (comboBox1.SelectedItem as Catalog).Id;

            var product = new Product()
            {
                Id = selProduct.Id,
                IdCatalog = selectedId,
                Name = productName,
                DateTime1 = date1,
                DateTime2 = date2,
                Party = Convert.ToInt32(party),
                Count = Convert.ToInt32(count),
                PhotoName = ProductName,
            };
            _productRepo.ChangeProduct(product);
            DisplayCatalog();
        }

        // Видалення продукції
        private void button4_Click(object sender, EventArgs e)
        {
            var selProduct = listBox1.SelectedItem as Product;
            _productRepo.DelProduct(selProduct);
            DisplayCatalog();
        }
    }
}
