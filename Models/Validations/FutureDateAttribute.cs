using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Validations
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dueDate = (DateTime)value;
            return dueDate >= DateTime.Today;
        }
    }
}
