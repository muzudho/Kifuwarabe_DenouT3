namespace Grayscale.Kifuwarakaku.Entities.Configuration
{
    using System.IO;

    /// <summary>
    /// Resource. ファイルについて。
    /// </summary>
    public class ResFile : IResFile
    {
        /// <summary>
        /// ファイル名。
        /// 拡張子は .log 固定。ファイル削除の目印にします。
        /// </summary>
        public string Name { get; private set; }

        public static IResFile AsData(string fullName)
        {
            return new ResFile(fullName);
        }
        public static IResFile AsLog(string logDirectory, string basename)
        {
            return new ResFile(Path.Combine(logDirectory, $"[{EntitiesLayer.Unique}]{basename}"));
        }

        ResFile(string name)
        {
            this.Name = name;
        }
    }
}
