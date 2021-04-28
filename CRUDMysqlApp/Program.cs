using System;
using System.Linq;

namespace CRUDMysqlApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CrudDemo app = new CrudDemo("localhost", "biblioteca", "root", "");
            app.TestConnection(); // Funcionando
            app.CreateData(); // Funcionando
            app.ReadData();//Funcionando
            app.UpdateData(7); // change idproduct based on your data // Funcionando

            app.ReadData(); //Funcionando
            app.DeleteData(8); // change idproduct based on your data //Funcionando
            app.ReadData();

            app.BulkData();//Funcionando
        }
    }
}
