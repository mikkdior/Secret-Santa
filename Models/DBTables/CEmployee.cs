namespace Secret_Santa.Models.DBTables;

public class CEmployee: IEmployee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public void Work() { }
}
