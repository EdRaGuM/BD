using System.Numerics;
using SQLite;

namespace BD.Tablas
{
    public class T_Usuario
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id_Usuario { get; set; }

        [NotNull]
        public string Nombre { get; set; }

        [NotNull]
        public string Ap_Paterno { get; set; }

        [NotNull]
        public string Ap_Materno { get; set; }

        [NotNull]
        public string Telefono { get; set; }

        [NotNull, Unique]
        public string Correo { get; set; }

        [NotNull]
        public string Genero { get; set; }

        [NotNull]
        public string Contrasena { get; set; }
    }
}
