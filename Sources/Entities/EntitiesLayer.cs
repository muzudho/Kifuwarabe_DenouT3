using System;
using Grayscale.Kifuwarakaku.Entities.Configuration;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Logging;

namespace Grayscale.Kifuwarakaku.Entities
{
    public class EntitiesLayer
    {
        private static readonly Guid unique = Guid.NewGuid();
        public static Guid Unique { get { return unique; } }

        public static void Implement(IEngineConf engineConf)
        {
            SpecifyFiles.Init(engineConf);
            Logger.Init(engineConf);
            Util_KifuTreeLogWriter.Init(engineConf);
        }
    }
}
