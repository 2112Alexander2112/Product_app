using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Product_App.Interface;
using Product_App.Models;

namespace Product_App.Services
{
    public class CatalogRepos : ICatalog
    {
        private readonly FileServices _fileServices;
        private readonly string _filePath;

        public List<Catalog> _Catalog { get; set; }

        public CatalogRepos()
        {
            _fileServices = new FileServices();
            _filePath = @"..\..\Data\catalog.txt";
            _Catalog = new List<Catalog>();
            LoadData();
           
        }

        private bool Exists(Catalog cat)
        {
            return _Catalog.Any(catalog => catalog.Id == cat.Id);
        }

        public void LoadData()
        {
            string text = _fileServices.ReadFileContent(_filePath);
            string[] rows = text.Split('\n');
            foreach (var row in rows)
            {
                if (!string.IsNullOrWhiteSpace(row))
                {
                    string[] parts = row.Split('-');
                    Catalog catalog = new Catalog()
                    {
                        Id = Convert.ToInt32(parts[0].Trim()),
                        Name = parts[1].Trim()
                    };
                    _Catalog.Add(catalog);
                }

            }
        }

        public void SaveData()
        {
            string content = string.Empty;
            foreach (var catalog in _Catalog)
            {
                content += $"{catalog.Id} - {catalog.Name}\n";
                _fileServices.WriteFileContent(_filePath, content);
            }
        }

        public void AddCatalog(Catalog cat)
        {
            if (Exists(cat))
            {
                MessageBox.Show("Такий департамент вже існує!!");
            }
            else
            {
                _Catalog.Add(cat);
                SaveData();
            }
        }

        public void DeleteCatalog()
        {
            throw new NotImplementedException();
        }

        public List<Catalog> GetAllCatalog()
        {
            return _Catalog;
        }

        public int GetLastId()
        {
            return _Catalog[_Catalog.Count - 1].Id;
        }
    }
}
