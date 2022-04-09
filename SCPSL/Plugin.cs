using Exiled.API.Features;

namespace OmegaWarheadPlugin
{
    public class Plugin : Plugin<Config>
    {
        private static readonly Plugin Singleton = new Plugin();
        public static Plugin Instance => Singleton;

        private PlayerE player;

        public override void OnEnabled()
        {
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
        }

        public void RegisterEvents()
        {
            player = new PlayerE();
            Exiled.Events.Handlers.Warhead.Starting += player.OnWarheadStart;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Warhead.Starting -= player.OnWarheadStart;
            player = null;
        }
    }
}
