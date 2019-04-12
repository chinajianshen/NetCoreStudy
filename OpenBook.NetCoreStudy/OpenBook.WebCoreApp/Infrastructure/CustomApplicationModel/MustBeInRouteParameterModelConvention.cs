using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomApplicationModel
{
    public class MustBeInRouteParameterModelConvention : Attribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.BindingInfo == null)
            {
                parameter.BindingInfo = new Microsoft.AspNetCore.Mvc.ModelBinding.BindingInfo();
            }
            parameter.BindingInfo.BindingSource = BindingSource.Path;
        }
    }
}
