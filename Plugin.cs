using Exiled.API.Features;
using System;

namespace OmegaWarheadPlugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "ClaudioPanConQueso";
        public override string Name { get; } = "OmegaWarhead";
        public override string Prefix { get; } = "OmegaWarhead";
        public override Version Version { get; } = new Version(1, 0, 4);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 1);

        public static Plugin Singleton;
        internal EventHandlers handler;

        public void RegisterEvents()
        {
            handler = new EventHandlers();
            Exiled.Events.Handlers.Warhead.Starting += handler.OnWarheadStart;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Warhead.Starting -= handler.OnWarheadStart;
            handler = null;
        }
    }
}
