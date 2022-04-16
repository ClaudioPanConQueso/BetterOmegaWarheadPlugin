using Exiled.API.Features;

namespace OmegaWarheadPlugin
{
    public class Plugin : Plugin<Config>
    {
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
            Exiled.Events.Handlers.Warhead.Starting += handler.OnWarheadStart;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Warhead.Starting -= handler.OnWarheadStart;
            handler = null;
        }
    }
}
