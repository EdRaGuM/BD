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
    public partial class V_ConsultarEvidencia : ContentPage
    {
        public ObservableCollection<T_Tarea> Items { get; set; }
        string EvidenciaCon;

        public V_ConsultarEvidencia()
        {
            InitializeComponent();
            BindingContext = this;

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var tareaSeleccionada = (T_Tarea)e.Item;
            await Navigation.PushAsync(new V_InformacionEvidencia(tareaSeleccionada));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            var db = new SQLiteConnection(ruta);
            db.CreateTable<T_Tarea>();

            var datos = db.Table<T_Tarea>().ToList();
            Items = new ObservableCollection<T_Tarea>(datos);
            MyListView.ItemsSource = Items;


            if (datos.Count == 0)
            {
                DisplayAlert("Aviso", "No hay registros en la base de datos.", "OK");
                return;
            }

            Items = new ObservableCollection<T_Tarea>(datos);
            MyListView.ItemsSource = Items;
        }


        private void txtEvidencia_TextChanged(object sender, TextChangedEventArgs e)
        {

            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            var db = new SQLiteConnection(Ruta);
            db.CreateTable<T_Tarea>();

            string texto = txtEvidencia.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(texto))
            {
                var todas = db.Table<T_Tarea>().ToList();
                Items = new ObservableCollection<T_Tarea>(todas);
                MyListView.ItemsSource = Items;
                return;
            }

            IEnumerable<T_Tarea> resul = SELECT_WHERE(db, texto);

            Items = new ObservableCollection<T_Tarea>(resul);
            MyListView.ItemsSource = Items;
        }

        public IEnumerable<T_Tarea> SELECT_WHERE(SQLiteConnection db, string EvidenciaIng)
        {
            string consul = "SELECT * FROM T_Tarea WHERE Titulo LIKE ?";
            return db.Query<T_Tarea>(consul, $"%{EvidenciaIng}%");
        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Menu());
        }
    }
}
