
namespace ThjonustukerfiWebAPI.Providers.SMS
{
    public class SMSUtils
    {
        public static string ReplaceIcelandicLetters(string message)
        {
            var srcChars = new char[] {'á', 'é', 'ú', 'í', 'ý', 'ð', 'ö', 'ó', 'þ', 'Á', 'É', 'Ú', 'Í', 'Ý', 'Ð', 'Ö', 'Ó', 'Þ'};
            var dstChars = new char[] {'a', 'e', 'u', 'i', 'y', 'd', 'o', 'o', 't', 'A', 'E', 'U', 'I', 'Y', 'D', 'O', 'O', 'T'};

            var srcStr = new string[] {"æ", "Æ"};
            var dstStr = new string[] { "ae", "AE" };

            string s = message;

            for (int i = 0; i < srcChars.Length; i++)
            {
                s = s.Replace(srcChars[i], dstChars[i]);
            }

            for (int i = 0; i < srcStr.Length; i++)
            {
                s = s.Replace(srcStr[i], dstStr[i]);
            }

            return s;
        }
    }

}
