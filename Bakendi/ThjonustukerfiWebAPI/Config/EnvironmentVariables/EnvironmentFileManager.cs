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
            else
            {
                string SMTP_USERNAME = Environment.GetEnvironmentVariable("SMTP_USERNAME");
                string SMTP_PASSWORD = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
                string SMTP_SERVER = Environment.GetEnvironmentVariable("SMTP_SERVER");
                string SMTP_PORT = Environment.GetEnvironmentVariable("SMTP_PORT");

                var path = "Config/EnvironmentVariables";

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, ".env")))
                {
                    outputFile.WriteLine($"SMTP_USERNAME={SMTP_USERNAME}");
                    outputFile.WriteLine($"SMTP_PASSWORD={SMTP_PASSWORD}");
                    outputFile.WriteLine($"SMTP_SERVER={SMTP_SERVER}");
                    outputFile.WriteLine($"SMTP_PORT={SMTP_PORT}");
                }

                if (File.Exists(envFile)) { return ParseEnvFile(envFile); }
                else { return new Dictionary<string, string>(); }
            }
        }

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