using System;
using System.Text.RegularExpressions;

namespace TM.Desktop
{
    public static class RegexHelper
    {
        public static bool isEmpty(this string s)
        {
            if (s == string.Empty) return true;
            else return false;
        }
        public static bool isNumber(this string s)
        {
            try
            {
                if (isEmpty(s)) return false;
                return Regex.IsMatch(s, @"^[0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            catch (Exception) { return false; }
        }
        public static bool isDecimal(this string s)
        {
            try
            {
                if (isEmpty(s)) return false;
                return Regex.IsMatch(s, @"^[0-9]*\.?[0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            catch (Exception) { return false; }
        }
        public static bool isEmail(this string s)
        {
            try
            {
                if (isEmpty(s)) return false;
                return Regex.IsMatch(s, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            catch (Exception) { return false; }
        }
        public static bool isPhone(this string s)
        {
            try
            {
                if (isEmpty(s)) return false;
                return Regex.IsMatch(s, @"\d{9,15}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            catch (Exception) { return false; }
        }
        public static bool isMatchString(this string s, string x)
        {
            try
            {
                if (isEmpty(s) && isEmpty(x)) return false;
                return s == x;
            }
            catch (Exception) { return false; }
        }
        public static bool isBiger(this int one, int two)
        {
            return one > two;
        }
        public static bool isSmaller(this int one, int two)
        {
            return one < two;
        }
        public static bool isBiger(this decimal one, decimal two)
        {
            return one > two;
        }
        public static bool isSmaller(this decimal one, decimal two)
        {
            return one < two;
        }
        public static string RemoveWord(this string s)
        {
            try
            {
                return Regex.Replace(s, @"[a-zA-Z]", "");
            }
            catch (Exception) { return null; }
        }
        public static string RemoveWord(this string s, string chars)
        {
            try
            {
                s = s.Replace(chars, "");
                return s.RemoveWord();
            }
            catch (Exception) { return null; }
        }
        public static string RemoveWord(this string s, string[] chars)
        {
            try
            {
                foreach (var item in chars)
                    s = s.Replace(item, "");
                return s.RemoveWord();
            }
            catch (Exception) { return null; }
        }
        public static int RemoveWordToInt(this string s)
        {
            try
            {
                return int.Parse(Regex.Replace(s, @"[a-zA-Z]", ""));
            }
            catch (Exception) { return 0; }
        }
        public static string RemoveNumber(this string s)
        {
            try
            {
                return Regex.Replace(s, @"[\d]", "");
            }
            catch (Exception) { return null; }
        }
        public static string GetTimePattern(this string s)
        {
            try
            {
                //string value = "30.Jul.2019 This the line I want to match 15:04:09";
                string timepattern = @"time=(?:2[0-3]|[01]?[0-9])[:.][0-5]?[0-9][:.][0-5]?[0-9]";
                var timeRender = Regex.Match(s, timepattern);
                return timeRender.ToString();
            }
            catch (Exception) { return null; }
        }
        public static bool IsUrlValid(this string s)
        {
            try
            {
                string pattern = @"^(https?|ftp):\\/\\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\\.)*[a-zA-Z0-9-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\\/($|[a-zA-Z0-9.,?'\\\\+&%$#=~_-]+))*$";
                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var rs = regex.IsMatch(s);
                return rs;
            }
            catch (Exception) { return false; }
        }
        public static bool IsYoutubeUrl(this string s)
        {
            try
            {
                string pattern = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var rs = regex.Match(s);
                return regex.IsMatch(s); ;
            }
            catch (Exception) { return false; }
        }
        public static string youtubeGetIDVideos(this string s)
        {
            try
            {
                //string pattern = @"(youtu.*be.*)\/(watch\?v=|embed\/|v|shorts|)(.*?((?=[&#?])|$))";
                string pattern = @"(youtu.*be.*)\/(\@\w+|)\/(videos|shorts)|(youtu.*be.*)\/(watch\?v=|embed\/|v|shorts|)(.*?((?=[&#?])|$))";
                //string pattern = @"(youtu.*be.*)\/(\@\w+|)\/(videos|shorts)";
                var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var rs = regex.Match(s);
                if (rs.Success) return rs.Groups[2].Value +" "+ rs.Groups[6].Value;
                else return null;
            }
            catch (Exception) { return null; }
        }
    }
}