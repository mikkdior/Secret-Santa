namespace Secret_Santa.Services;

public class CTickatus
{
    /// <summary>
    ///     Основная выборка из БД на создание билетов санты.
    /// </summary>
    public IEnumerable<CSantaTicketVM> GetTickets(IEnumerable<CEmployee> emps, IEnumerable<CGift> gifts)
        => (from g in gifts
           join e_from in emps on g.IdEmpFrom equals e_from.Id
           join e_to in emps on g.IdEmpTo equals e_to.Id
           select new CSantaTicketVM(e_from, e_to, g.Name)).OrderBy(t => t.GiftName);
}
