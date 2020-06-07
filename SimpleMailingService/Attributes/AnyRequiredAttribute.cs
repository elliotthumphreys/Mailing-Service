using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleMailingService.Attributes
{
    public class AnyRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is IEnumerable<object> listValue))
                return false;

            return listValue.Any();
        }
    }
}
