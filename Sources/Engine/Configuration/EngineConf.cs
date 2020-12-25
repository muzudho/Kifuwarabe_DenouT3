using System.Configuration;
using System.IO;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Nett;

namespace Grayscale.Kifuwarakaku.Engine.Configuration
{
    public class EngineConf : IEngineConf
    {
        public string GetEngine(string key)
        {
            return this.EngineToml.Get<TomlTable>("Engine").Get<string>(key);
        }

        public string GetResourceFullPath(string key)
        {
            return Path.Combine(this.ProfilePath, this.EngineToml.Get<TomlTable>("Resources").Get<string>(key));
        }

        /// <summary>
        /// フルパスにしない方が使いやすい？
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetLogBasename(string key)
        {
            return this.EngineToml.Get<TomlTable>("Logs").Get<string>(key);
        }

        public string LogDirectory
        {
            get
            {
                if (this.logDirectory_ == null)
                {
                    this.logDirectory_ = Path.Combine(this.ProfilePath, this.EngineToml.Get<TomlTable>("Resources").Get<string>("LogDirectory"));
                }
                return this.logDirectory_;
            }
        }
        string logDirectory_;

        TomlTable EngineToml
        {
            get
            {
                if (this.engineToml_ == null)
                {
                    this.engineToml_ = Toml.ReadFile(Path.Combine(this.ProfilePath, "Engine.toml"));
                }
                return this.engineToml_;
            }
        }
        TomlTable engineToml_;

        string ProfilePath
        {
            get
            {
                if (this.profilePath_ == null)
                {
                    this.profilePath_ = ConfigurationManager.AppSettings["Profile"];
                }
                return this.profilePath_;
            }
        }
        string profilePath_;
    }
}
