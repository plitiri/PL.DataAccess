using System;

namespace PL.DataAccess.TestApp.Models;

public class Sample01
{
    public DateTime? Now { get; set; }

    public override string ToString()
    {
        return $"Now: {Now}";
    }
}
