using System.ComponentModel;
using System.Reflection;

namespace Modules.Utilities
{
    // Reference: https://stackoverflow.com/questions/15388072/how-to-add-extension-methods-to-enums
    public static class EnumExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="value">Enum variable</param>
        /// <returns>The [Description] attribute of an Enum value.</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute is not null ? attribute.Description : value.ToString();
        }
    }
}
