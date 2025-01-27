using System.ComponentModel.DataAnnotations;


namespace IntraApi.Domain.Enums
{
    public enum WorkPeriodType
    {
        [Display(Name = "Daily")]
        Daily = 1,
        [Display(Name = "Weekly")]
        Weekly = 2,
        [Display(Name = "Monthly")]
        Monthly = 3
    }
}
