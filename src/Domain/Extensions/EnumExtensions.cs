using System.ComponentModel.DataAnnotations;

namespace Domain.Extensions;

public static class EnumExtensions
{
    public static string ToStringOrDisplayName(this Enum value) {
        var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();

        if (memberInfo is not null) {
            var displayNameAttribute = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

            if (displayNameAttribute is not null) return displayNameAttribute.Name!;
        }

        return value.ToString();
    }
}
