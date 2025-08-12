using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Tablas
{
    public class T_Tarea
    {
        [AutoIncrement, PrimaryKey, NotNull]
        public int Id_Tarea { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Grupo { get; set; }

        public string Materia { get; set; }

        public string Evidencia { get; set; }
    }
}
