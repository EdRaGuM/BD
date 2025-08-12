using SQLite;

namespace BD.Modelo
{
    public interface Conexion
    {
        SQLiteAsyncConnection GetConnection();
    }
}
