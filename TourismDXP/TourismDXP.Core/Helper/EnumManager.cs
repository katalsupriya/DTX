using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TourismDXP.Core.Helper
{
    public class EnumManager
    {
        /// <summary>
        /// convert enum list into list of SelectListItem
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static List<SelectListItem> GetSelectLisItemsByEnum<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(@enum => new
                {
                    enumValue = GetValue(@enum),
                    order = GetDisplayOrder(@enum),
                    enumText = GetEnumDescription(@enum)
                })
                .OrderBy(x => x.order)
                .Select(x => new SelectListItem
                {
                    Text = x.enumText,
                    Value = x.enumValue
                }
            ).ToList();
        }

        /// <summary>
        /// get the display order what we have provided for each enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        private static int GetDisplayOrder<T>(T @enum)
        {
            return @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>(false)?.Order ?? 0;
        }

        /// <summary>
        /// Get enum value
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetValue<T>(T @enum)
        {
            return ((int)Enum.Parse(typeof(T), @enum.ToString())).ToString();
        }

        /// <summary>
        /// get the display name of the enums
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName<T>(T @enum)
        {
            return @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>(false)?.Name ?? @enum.ToString();
        }

        /// <summary>
        /// Get the enum description based on enum value
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(T @enum)
        {
            var fieldInfo = @enum.GetType().GetField(@enum.ToString());
            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : @enum.ToString();
        }
    }
}
