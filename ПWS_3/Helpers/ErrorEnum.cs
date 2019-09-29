using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ПWS_3.Helpers
{
    public enum ErrorEnum
    {
        [Description("Неверно заданы параметры")]
        InCorrectParams,
        [Description("Проблема при создании сущности")]
        CreateEntityError,
        [Description("Студентов нет")]
        StudentsNotExist,
        [Description("Такого студента не существуют")]
        StudentNotExist,
        [Description("Ошибка при удалении студента")]
        StudentDeleteError
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum @enum)
        {
            FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return @enum.ToString();
        }
    }
}