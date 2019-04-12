using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomModelValiates
{
    public class ClassicMovieAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            
            throw new NotImplementedException();
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

       
    }
}
