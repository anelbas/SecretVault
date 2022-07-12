using Newtonsoft.Json.Linq;
using SecretVaultAPI.Utils;

namespace SecretVaultAPI
{
        public class ConfigSettings : IConfigSettings
        {
            private string _key1;
            private string _key2;
            public ConfigSettings()
            {
                Init();
            }
            public void Init()
            {
                var secretValues = JObject.Parse(SecretsManager.GetSecret());
                if (secretValues != null)
                {
                    _key1 = secretValues["key1"].ToString();
                    _key2 = secretValues["key2"].ToString();
                }
            }
            public string Key1
            {
                get
                {
                    return _key1;
                }
                set
                {
                    _key1 = value;
                }
            }
            public string Key2
            {
                get
                {
                    return _key2;
                }
                set
                {
                    _key2 = value;
                }
            }
        }
        public interface IConfigSettings
        {
            string Key1
            {
                get;
                set;
            }
            string Key2
            {
                get;
                set;
            }
        }
    }