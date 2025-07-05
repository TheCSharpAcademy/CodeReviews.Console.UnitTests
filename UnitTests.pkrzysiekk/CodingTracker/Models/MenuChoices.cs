using System.ComponentModel.DataAnnotations;
namespace CodingTracker.Models
{
    public enum MenuChoices
    {
        [Display(Name = "Display all the records")]
        DisplayRecords,
        [Display(Name = "Insert record into the Database")]
        InsertRecord,
        [Display(Name = "Remove record")]
        RemoveRecord,
        [Display(Name = "Edit record")]
        EditRecord,
        [Display(Name = "Exit Menu")]
        Exit
    }
}
