using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Battleship_SolitaireUI.Extensions
{
    public static class EnumHelper
    {
        /// <summary>
        /// Get the description of a enum
        /// </summary>
        /// <param name="enumValue">the enum value</param>
        /// <returns>The description of the enum</returns>
        public static string GetDescriptionOfEnumValue(Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttributes.Length > 0) //return first description if existing else return name of value
            {
                return descriptionAttributes.FirstOrDefault().Description;
            }
            else
            {
                return enumValue.ToString();
            }
        }
    }
}
