using Exiled.API.Interfaces;
using System.ComponentModel;

namespace OmegaWarheadPlugin
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("If true, turns the alpha warhead into omega warhead.")]
        public bool ReplaceAlpha { get; set; } = false;
        //[Description("Broadcast that will appear when the rescue helicopter is coming")]
        //public string HelicopterMessage { get; set; } = "<color=blue>RESCUE HELICOPTER COMING TO SURFACE IN:</color> ";
        [Description("Broadcast that will appear when the omega warhead is activated.")]
        public string ActivatedMessage { get; set; } = "<b><color=red>OMEGA WARHEAD ACTIVATED.</color></b> \nPLEASE EVACUATE IN BREACH SHELTER.";
        [Description("Cassie message when omega warhead is stopped")]
        public string StopCassie { get; set; } = "pitch_0.9 Omega Warhead detonation stopped";
        [Description("Cassie message of omega warhead(Not recommended to modify this)")]
        public string Cassie { get; set; } = "pitch_0.2 .g3 .g3 .g3 pitch_0.9 attention . attention . activating omega warhead . detonation in 3 minutes . please evacuate in the breach shelter of entrance zone . please evacuate now . 170 seconds until destruction .g3 . .  .g3 . .  . .g3 . . .g3 . . .g3 . . . . .g3 . . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 120 seconds  .g3 . .  .g3 . .  .g3 . .  .g3 . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .   .g3 . . .   .g3 . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   1 minute .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . 30 seconds . all checkpoint doors are open . please evacuate . 20 . 19 . 18 . 17 . 16 . 15 . 14 . 13 . 12 . 11 . 10 seconds 9 . 8 . 7 . 6 . 5 . 4 . 3 . 2 . 1 . pitch_0.7 0";
        [Description("Time to make omega warhead explode after cassie message.")]
        public float TimeToExplodeAfterCassie = 1;
    }
}
