namespace Secret_Santa.Models;

public static class CSantaHelper
{
    /// <summary>
    ///     Выбор рандомного элемента из сиквенции
    /// </summary>
    public static T ChooseRandom<T>(IEnumerable<T> coll)
    {
        int random_index = new Random().Next(coll.Count());
        return coll.ToArray()[random_index];
    }
    /// <summary>
    ///     Перемешиваем сиквенцию
    /// </summary>
    public static IEnumerable<int> ShakeIndexes(IEnumerable<int> indexes)
        => indexes.OrderBy(item => new Random().Next()).ToList();

    /// <summary>
    ///     Генерация рандомного имени для сотрудника
    /// </summary>
    public static string[] GenerateEmployeeNames() => new[]
        {
            FirstNamesColl[new Random().Next(FirstNamesColl.Length)].Trim(),
            LastNamesColl[new Random().Next(LastNamesColl.Length)].Trim()
        };

    /// <summary>
    ///     Генерация рандомного имени для подарка
    /// </summary>
    public static string GenerateGiftName() => GiftsColl[new Random().Next(GiftsColl.Length)].Trim();

    private static string[] FirstNamesColl = { "Harry","Ross","Bruce","Cook","Carolyn","Morgan",
                        "Albert","Walter","Randy","Reed","Larry","Barnes","Lois","Will","Jesse","Chris",
                        "Ernest","Rogers","Henry","Samuel","Theresa","Peter","Michelle","Perry","Frank",
                        "Shirley", "Jeremy","Rachel","Edward","Christopher","Harry","Thomas","Joe","Sara","Mike",
                        "Bailey","Roger","John", "Marie","Tom","Anthony","Evan","Julie","Hall",
                        "Paula","Phillip","Dorothy","Mart","Annie","Hernandez","Alice","Buzz","Andy" };

    private static string[] LastNamesColl = { "Ruth","Jackson","Jacqueline","Torres", "Josephson","Nelson", "Wandayk", "Thomas", 
                        "Debrand","Allen","Geraldson","Harrison","Raymond","Carter","Carlson","Sanchez","Ralphson","Clarkson",
                        "Jeanson","Alexanderer","Stephen","Roberts","Ericson","Long","Dartson","Scott","Tresand","Diaz" };

    private static string[] GiftsColl = { "Wine bottle","Book","Steel Water Bottle","Gift Card", "French Press","Essential Oils", 
                        "Perfume", "Self-Care Set", "Wallet","Hot Sauce","Warm Gloves","Box Of Chocolates","Headphones",
                        "Christmas Sweater","Coffee Mugs","Succulents","Luxe Coffee","Scarves", "Laptop Sleeves","Sunglasses",
                        "Socks","Netflix Subscription","Gift Basket","Bonsai Tree","Bluetooth Speaker","Backpack","Smartwatch","Windchime" };
}
