using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetPlayTypeName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null) { return value.ToString(); }

            var attribute = Attribute.GetCustomAttribute(field, typeof(PlayTypeEnumAttributes)) as PlayTypeEnumAttributes;

            return attribute != null ? attribute.Name : value.ToString();
        }
    }
}
