using System.ComponentModel.DataAnnotations;

namespace GIP5_ScrumBoard.Enum
{
    public enum Status
    {
        [Display(Name ="To Do")]
        TODO,
        [Display(Name ="In progress")]
        InProgress,
        Done
    }
}
