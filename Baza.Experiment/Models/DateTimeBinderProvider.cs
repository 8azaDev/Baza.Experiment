using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Baza.Experiment.Models
{
    public class DateTimeBinderProvider : IModelBinderProvider
    {
        private readonly UserCultureInfo m_UserCulture;

        public DateTimeBinderProvider(UserCultureInfo userCulture)  
        {  
            m_UserCulture = userCulture;  
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (context.Metadata.UnderlyingOrModelType == typeof(DateTime))
            {
                return new DateTimeBinder(m_UserCulture);
            }
            return null;
        }
    }
}
