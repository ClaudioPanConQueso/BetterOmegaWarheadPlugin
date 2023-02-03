using Exiled.API.Features;
using System;

namespace BetterOmegaWarhead
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "ClaudioPanConQueso";
        public override string Name { get; } = "BetterOmegaWarhead";
        public override string Prefix { get; } = "BetterOmegaWarhead";
        public override Version Version { get; } = new Version(1, 0, 9);
        public override Version RequiredExiledVersion { get; } = new Version(6, 0, 0);

        public static Plugin Singleton;
        internal EventHandlers handler;

        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Singleton = null;
            UnregisterEvents();
            base.OnDisabled();
        }
        public void RegisterEvents()
        {
            handler = new EventHandlers();
            Exiled.Events.Handlers.Server.RestartingRound += handler.OnRestartingRound;
            Exiled.Events.Handlers.Warhead.Starting += handler.OnWarheadStart;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= handler.OnRestartingRound;
            Exiled.Events.Handlers.Warhead.Starting -= handler.OnWarheadStart;
            handler = null;
        }
    }
}
