using System;

namespace Lawang.Coding_Tracker;

public class CodingGoals
{
    public int Id { get; set; }
    public string Time_to_complete { get; set; } = "";
    public string Avg_Time_To_Code { get; set;} = "";
    public int Days_left { get; set; }
}
