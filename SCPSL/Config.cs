using Exiled.API.Interfaces;
using System.ComponentModel;

namespace OmegaWarheadPlugin
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("If true, turns the alpha warhead into omega warhead.")]
        public bool ReplaceAlpha { get; set; } = false;

        [Description("Broadcast that will appear when the omega warhead is activated.")]
        public string ActivatedMessage { get; set; } = "<b><color=red>OMEGA WARHEAD ACTIVATED.</color></b> \nPLEASE EVACUATE IN BREACH SHELTER.";

        [Description("Broadcast that will appear when the omega warhead detonates.")]
        public string DetonatedMessage { get; set; } = "OMEGA WARHEAD DETONATED";
    }
}
