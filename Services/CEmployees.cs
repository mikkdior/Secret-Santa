namespace Secret_Santa.Services;

public class CEmployees 
{
    private CDataBase _Db;
    public int EmpsCount { get => GetEmployees().Count(); }
    //---------------------------------------------------
    /// <summary>
    ///     Конструктор сервиса сотрудников.
    /// </summary>
    public CEmployees(CDataBase db) { _Db = db; }
    /// <summary>
    ///     Доступ к сотрудникам через сервис.
    /// </summary>
    public IEnumerable<CEmployee> GetEmployees() => _Db.Employees;
    /// <summary>
    ///     Развилка на добавление/удаление сотрудников.
    /// </summary>
    public void UpdateEmps(string act, int count = 1)
    {
        if (act == "add") AddRandomEmps(count);
        else LayoffRandomEmps(count);
    }
    /// <summary>
    ///     Добавляем сотрудников.
    /// </summary>
    private void AddRandomEmps(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var names = CSantaHelper.GenerateEmployeeNames();

            _Db.Employees.Add(new CEmployee
            {
                FirstName = names[0],
                LastName = names[1]
            });
        }
        
        _Db.SaveChanges();
    }
    /// <summary>
    ///     Сокращаем сотрудников.
    /// </summary>
    private void LayoffRandomEmps(int count = 1)
    {
        if (EmpsCount == 0) return;

        for (int i = 0; i < count; i++)
            _Db.Employees.Remove(CSantaHelper.ChooseRandom(_Db.Employees));

        _Db.SaveChanges();
    }
}