using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsExercise
{
    public static class EnumMapper
    {
        public static TEnum MapValueToEnum<TEnum, TValue>(TValue value) where TEnum : struct
        {
            TEnum result;
            
            if(!Enum.TryParse(value.ToString(), out result))
            {
                throw new Exception($"Value '{value}' is not part of '{result.ToString()} enum'");
            }

            return result;
        }
    }
}
