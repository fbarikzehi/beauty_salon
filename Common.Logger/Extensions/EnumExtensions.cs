using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Common.Logger.Extensions
{
    public static class EnumExtensions<T>
    {
        public static string GetPersianName(T type)
        {
            FieldInfo fieldInfo = type.GetType().GetField(type.ToString());

            var displayAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            if (displayAttributes.Count() > 0 && displayAttributes[0].ResourceType != null)
                return LookupResource(displayAttributes[0].ResourceType, displayAttributes[0].Name);

            if (displayAttributes == null)
                return string.Empty;
            return displayAttributes.Length > 0 ? displayAttributes[0].Name : type.ToString();
        }

        public static IList<T> GetValues(System.Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)System.Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static IList<string> GetNames(System.Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(System.Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes.Count() > 0 && descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
        }

        private static string LookupResource(System.Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey;
        }

        private static T Parse(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }
    }
}
