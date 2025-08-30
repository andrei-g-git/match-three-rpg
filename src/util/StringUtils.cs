using System.Text.RegularExpressions;

namespace Util;
public static class StringUtils{

    public static string SplitPascal(string s){
        return Regex.Replace(s, "(?<!^)([A-Z])", " $1");        
    }

}