using System;
using System.Collections.Generic;
using System.IO;

namespace ThjonustukerfiWebAPI.Config.EnvironmentVariables
{
    public static class EnvironmentFileManager
    {
        /// <summary>Loads up the environment variable file and returns a dictionary with environment values</summary>
        /// <returns>Dictionary of string and string</returns>
        public static Dictionary<string, string> LoadEvironmentFile()
        {
            string envFile = "Config/EnvironmentVariables/.env";

            if (File.Exists(envFile)) { return ParseEnvFile(envFile); }
            else { return new Dictionary<string, string>(); }
        }

        /// <summary>Takes a environment file (as string) and parses it to a dictionary, the environment file should be setup as key-value pairs.</summary>
        private static Dictionary<string, string> ParseEnvFile(string envFile)
        {
            var dic = new Dictionary<string, string>();

            var envData = File.ReadAllLines(envFile);
            for (var i = 0; i < envData.Length; i++)
            {
                var setting = envData[i];
                var sidx = setting.IndexOf("=");
                if (sidx >= 0)
                {
                    var skey = setting.Substring(0, sidx);
                    var svalue = setting.Substring(sidx+1);
                    if (!dic.ContainsKey(skey))
                    {
                        dic.Add(skey, svalue);
                    }
                }
            }

            return dic;
        }
    }
}