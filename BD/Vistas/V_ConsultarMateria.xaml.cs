using BD.Tablas;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_ConsultarMateria : ContentPage
    {
        public ObservableCollection<T_Materia> Items { get; set; }
        string MateriaCon;

        public V_ConsultarMateria()
        {
            InitializeComponent();
            BindingContext = this;
            CargarMaterias();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var materiaSeleccionada = (T_Materia)e.Item;
            await Navigation.PushAsync(new V_InformacionMateria(materiaSeleccionada));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            var db = new SQLiteConnection(ruta);
            db.CreateTable<T_Materia>();

            var datos = db.Table<T_Materia>().ToList();
            Items = new ObservableCollection<T_Materia>(datos);
            MyListView.ItemsSource = Items;

            if (datos.Count == 0)
            {
                DisplayAlert("Aviso", "No hay registros en la base de datos.", "OK");
                return;
            }

            Items = new ObservableCollection<T_Materia>(datos);
            MyListView.ItemsSource = Items;
        }

        private async void CargarMaterias()
        {
            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            var db = new SQLiteAsyncConnection(ruta);
            var materias = await db.Table<T_Materia>().ToListAsync();
            Items = new ObservableCollection<T_Materia>(materias);
            MyListView.ItemsSource = Items;
        }

        private void txtMateria_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            var db = new SQLiteConnection(Ruta);
            db.CreateTable<T_Materia>();

            string texto = txtMateria.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(texto))
            {
                var todas = db.Table<T_Materia>().ToList();
                Items = new ObservableCollection<T_Materia>(todas);
                MyListView.ItemsSource = Items;
                return;
            }

            IEnumerable<T_Materia> resul = SELECT_WHERE(db, texto);

            Items = new ObservableCollection<T_Materia>(resul);
            MyListView.ItemsSource = Items;
        }

        public IEnumerable<T_Materia> SELECT_WHERE(SQLiteConnection db, string MateriaIng)
        {
            string consul = "SELECT * FROM T_Materia WHERE Titulo LIKE ?";
            return db.Query<T_Materia>(consul, $"%{MateriaIng}%");
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Menu());
        }
    }
}
