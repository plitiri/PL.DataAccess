namespace PL.DataAccess.TestApp.Models;

public class MyTable
{
    public string? Name { get; set; }
    public decimal? Age { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}
