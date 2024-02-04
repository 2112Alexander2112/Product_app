using Product_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_App.Interface
{
    internal interface ICatalog
    {

        void LoadData();
        void SaveData();
        void AddCatalog(Catalog cat);
        void DeleteCatalog();
    }
}
