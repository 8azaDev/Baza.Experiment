using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Baza.Experiment.Models
{
    public class DateTimeRequest
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }
    }

    public class DateTimeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)  
            {  
                throw new ArgumentNullException(nameof(bindingContext));  
            }  
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);  
            if (string.IsNullOrEmpty(valueProviderResult.FirstValue))  
            {  
                return null;  
            }  
            DateTime datetime;  
            if (DateTime.TryParse(valueProviderResult.FirstValue, null, DateTimeStyles.AdjustToUniversal, out datetime))  
            {  
                bindingContext.Result = ModelBindingResult.Success(TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.Local).ToUniversalTime());  
            }  
            else  
            {  
                // TODO: [Enhancement] Could be implemented in better way.  
                bindingContext.ModelState.TryAddModelError(  
                    bindingContext.ModelName,  
                    bindingContext.ModelMetadata  
                    .ModelBindingMessageProvider.AttemptedValueIsInvalidAccessor(  
                        valueProviderResult.ToString(), nameof(DateTime)));  
            }  
            return Task.CompletedTask;  
        }
    }

    public class DateTimeBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)  
            {  
                throw new ArgumentNullException(nameof(context));  
            }  
            if (context.Metadata.UnderlyingOrModelType == typeof(DateTime))  
            {  
                return new DateTimeBinder();  
            }  
            return null;
        }
    }
}
