using SQLite;
using BD.Modelo;
using System.IO;
using Android.Runtime;
using Xamarin.Forms;
using BD.Droid;
using System.Reflection;

[assembly: Dependency(typeof(ConexionA))]

namespace BD.Droid
{
    public class ConexionA : Conexion
    {

        public SQLiteAsyncConnection GetConnection()
        {
            var RutaDocs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var Ruta = Path.Combine(RutaDocs, "Tareas.db3");
            return new SQLiteAsyncConnection(Ruta);
        }

    }
}