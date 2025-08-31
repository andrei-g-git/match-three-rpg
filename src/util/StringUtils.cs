using System;
using System.Text.RegularExpressions;

namespace Util;
public static class StringUtils{

    public static string SplitPascal(string s){
        return Regex.Replace(s, "(?<!^)([A-Z])", " $1");        
    }

    public static Enum ConvertToEnum(string value, Type enumType){ //this needs proofing and exception handling
        if(Enum.TryParse(enumType, value, out object stringToEnum)){
            return stringToEnum as Enum;
        }
        return default;
    }
}