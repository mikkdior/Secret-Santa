namespace Secret_Santa.Models.DBTables;

public class CGift
{
    public int Id { get; set; }
    public int IdEmpFrom { get; set; }
    public int IdEmpTo { get; set; }
    public string Name { get; set; }
}