using Product_App.Interface;
using Product_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_App.Services
{
    internal class ProductRepos : IProduct
    {
        private readonly FileServices _fileServices;
        private readonly string _filePath;

        public List<Product> _Product { get; set; }

        public ProductRepos()
        {
            _fileServices = new FileServices();
            _filePath = @"..\..\Data\production.txt";
            _Product = new List<Product>();
            loadData();
        }
        public void AddProduct(Product prod)
        {
            if (Exists(prod))
            {
                MessageBox.Show("Такий продукст вже існує!!");
            }
            else
            {
                _Product.Add(prod);
                saveData();
            }
        }
        public void DelProduct(Product prod)
        {
            _Product.Remove(prod);
            saveData();
        }

        public void ChangeProduct(Product prod)
        {
            var oldproduct = GetProduct(prod.Id);
            oldproduct.Name = prod.Name;
            oldproduct.DateTime1 = prod.DateTime1;
            oldproduct.DateTime2 = prod.DateTime2;
            oldproduct.Party = prod.Party;
            oldproduct.Count = prod.Count;
            oldproduct.IdCatalog = prod.IdCatalog;
            oldproduct.PhotoName = prod.PhotoName;
        }

        public void loadData()
        {
            string text = _fileServices.ReadFileContent(_filePath);
            string[] rows = text.Split('\n');
            foreach (var row in rows)
            {
                if (!string.IsNullOrWhiteSpace(row))
                {
                    string[] parts = row.Split('-');

                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(parts[0].Trim()),
                        IdCatalog = Convert.ToInt32(parts[1].Trim()),
                        Name = parts[2].Trim(),
                        DateTime1 = Convert.ToDateTime(parts[3].Trim()),
                        DateTime2 = Convert.ToDateTime(parts[4].Trim()),
                        Party = Convert.ToInt32(parts[5].Trim()),
                        Count = Convert.ToInt32(parts[6].Trim()),
                        PhotoName = parts[7].Trim(),
                    };
                    _Product.Add(product);
                }

            }
        }

        public void saveData()
        {
            string content = string.Empty;
            foreach (var product in _Product)
            {
                content += $"{product.Id} - {product.IdCatalog} - {product.Name} - {product.DateTime1} - {product.DateTime2} - {product.Party} - {product.Count} - {product.PhotoName}\n";
                _fileServices.WriteFileContent(_filePath, content);
            }
        }

        public List<Product> GetProductByCatalog(int catId)
        {
            return _Product.Where(product => product.IdCatalog == catId).ToList();
        }

        public Product GetProduct(int prodId)
        {
            return _Product.Where(product => product.Id == prodId).First();
        }


        public int GetLastId()
        {
            return _Product[_Product.Count - 1].Id;
        }

        private bool Exists(Product pro)
        {
            return _Product.Any(product => product.Id == pro.Id);
        }
       
    }
}
