using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFAppKolokvijum.DBContext
{
    public static class DataContext
    {
        public static string ConnectionString { get; set; } = "Data Source=.;Initial Catalog=Kontakti;Integrated Security=True;";
    }
}
