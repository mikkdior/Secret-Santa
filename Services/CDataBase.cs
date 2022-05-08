namespace Secret_Santa.Services;

public class CDataBase: DbContext
{
    public DbSet<CEmployee> Employees { get; set; }
    public DbSet<CGift> Gifts { get; set; }
    /// <summary>
    ///     Через конструктор передаем объект options родителю, 
    ///     а так же проверяем создана ли база. Если нет - создаем ее.
    /// </summary>
    public CDataBase(DbContextOptions<CDataBase> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
