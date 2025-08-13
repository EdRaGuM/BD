using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Tablas
{
    public class T_Materia
    {
        [AutoIncrement, PrimaryKey, NotNull]
        public int Id_Materia { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Tipo { get; set; }

        public DateTime FechaRegistro { get; set; }


    }
}
