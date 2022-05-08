namespace Secret_Santa.Services;

public class CGifts
{
    private CDataBase _Db;
    private CEmployees EmpsModel;
    private CTickatus Tickatus;
    public int GiftsCount { get => GetGifts().Count(); }
    //---------------------------------------------------

    /// <summary>
    ///     Конструктор. Заполняем поля.
    /// </summary>
    public CGifts(CDataBase db, CEmployees emps_model, CTickatus tickatus) 
    {
        _Db = db;
        EmpsModel = emps_model;
        Tickatus = tickatus;
        CheckGifts();
    }
    /// <summary>
    ///     Доступ к подаркам через сервис.
    /// </summary>
    public IEnumerable<CGift> GetGifts() => _Db.Gifts;

    /// <summary>
    ///     При обновлении страницы проверяем, не остался ли кто без подарка,
    ///     При выявлении проблем с отсутствием подарка или его уникальностью - переназначаем билеты.
    /// </summary>
    private void CheckGifts()
    {
        var gifts = GetGifts();

        if (Tickatus.GetTickets(EmpsModel.GetEmployees(), gifts).Count() < EmpsModel.EmpsCount)
        {
            SetRandomGifts();
            return;
        }

        int gifts_count = gifts.Count();
        bool from_is_unique = gifts_count == gifts.DistinctBy(g => g.IdEmpFrom).Count();
        bool to_is_unique = gifts_count == gifts.DistinctBy(g => g.IdEmpFrom).Count();

        if (!from_is_unique || !to_is_unique) SetRandomGifts();
    }

    /// <summary>
    ///     Развилка на добавление/удаление подарков.
    /// </summary>
    public void UpdateGifts(string act, int count = 1)
    {
        if (act == "add") AddRandomGifts(count);
        else RemoveRandomGifts(count);
        
        SetRandomGifts();
        _Db.SaveChanges();
    }

    /// <summary>
    ///     Добавление подарков.
    /// </summary>
    private void AddRandomGifts(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            _Db.Gifts.Add(new CGift()
            {
                IdEmpFrom = 1, // filler
                IdEmpTo = 2, // filler
                Name = CSantaHelper.GenerateGiftName()
            });
        }
        _Db.SaveChanges();
    }
    /// <summary>
    ///     Удаление подарков.
    /// </summary>
    private void RemoveRandomGifts(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            _Db.Gifts.Remove(CSantaHelper.ChooseRandom(GetGifts()));
            _Db.SaveChanges();
        }
    }

    /// <summary>
    ///     Проверяем кол-во подарков на соответствие с кол-вом сотрудников,
    ///     если есть несовпадения - добавляем необх./убираем лишнее.
    /// </summary>
    private void ControlGiftsCount()
    {
        if (GiftsCount == EmpsModel.EmpsCount) return; // It`s OK

        if (GiftsCount > EmpsModel.EmpsCount)
            RemoveRandomGifts(GiftsCount - EmpsModel.EmpsCount);
        else
            AddRandomGifts(EmpsModel.EmpsCount - GiftsCount);
    }

    /// <summary>
    ///     Назначаем подаркам поля сотрудников, которые будут дарить их.
    /// </summary>
    private void SetRandomGifts()
    {
        ControlGiftsCount();
        var indexes = CSantaHelper.ShakeIndexes(EmpsModel.GetEmployees().Select(e => e.Id)).ToArray();
        int counter = 0;
        int len = indexes.Count();

        foreach (var gift in _Db.Gifts)
        {
            gift.IdEmpFrom = indexes[counter];
            counter++;
            if (counter == len) 
                gift.IdEmpTo = indexes[0];
            else 
                gift.IdEmpTo = indexes[counter];
        }
        _Db.SaveChanges();
    }
}
