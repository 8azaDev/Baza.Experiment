using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Baza.Experiment.Models
{
    public class DateTimeBinder : IModelBinder
    {
        private readonly UserCultureInfo m_UserCulture;

        public DateTimeBinder(UserCultureInfo userCulture)
        {
            m_UserCulture = userCulture;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Check for missing data case 1: There was no <input ... /> element containing this data.
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            //bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            // Check for missing data case 2: There was an <input ... /> element but it was left blank.
            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                //if(valueProviderResult.FirstValue.IndexOf("T", StringComparison.CurrentCulture) < 0)
                //{
                //    var temp = DateTime.Parse(value);
                //    var a = TimeZoneInfo.Local.IsAmbiguousTime(temp);
                //    var tempa = temp.ToString("yyyy-MM-dd HH:mm:ss");
                //    value = tempa.Replace(" ", "T") + "+08:00";
                //}

                bindingContext.Result = ModelBindingResult.Success(DateTime.SpecifyKind(DateTime.Parse(valueProviderResult.FirstValue), DateTimeKind.Local));
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    exception,
                    bindingContext.ModelMetadata);
                return Task.CompletedTask;
            }
        }
    }
}
