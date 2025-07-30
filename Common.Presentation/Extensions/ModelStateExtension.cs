using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Common.Presentation.Extensions
{
    public static class ModelStateExtension
    {
        public static string[] GetModelErrors(this ModelStateDictionary modelState)
        {
            string[] messages = modelState.Where(a => a.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
            return messages;
        }

        public static string GetModelFirstError(this ModelStateDictionary modelState)
        {
            string messages = modelState.FirstOrDefault(a => a.Value.Errors.Count > 0).Value.Errors.FirstOrDefault(x=>!string.IsNullOrWhiteSpace(x.ErrorMessage)).ErrorMessage;
            return messages;
        }

        public static string ToHtmlString(this string[] errors)
        {
            string finalMessage = string.Empty;
            foreach (var e in errors)
                finalMessage += $"<p style=\"text-align: right;\">{e}</p>";

            finalMessage += "";

            return finalMessage;
        }
    }
}
