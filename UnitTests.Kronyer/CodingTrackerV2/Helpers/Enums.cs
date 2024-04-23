using System.ComponentModel.DataAnnotations;

namespace CodingTrackerV2.Helpers;

internal class Enums
{
    public enum MenuChoices
    {
        [Display(Name = "Add Record")]
        AddRecord,

        [Display(Name = "View Records")]
        ViewRecords,

        [Display(Name = "Delete Record")]
        DeleteRecord,

        [Display(Name = "Update Record")]
        UpdateRecord,

        Quit
    }
}
