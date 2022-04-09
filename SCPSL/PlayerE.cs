using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Linq;
using UnityEngine;
using Map = Exiled.API.Features.Map;

namespace OmegaWarheadPlugin
{
    class PlayerE
    {

        public void OnWarheadStart(StartingEventArgs ev)
        {
            if(Plugin.Instance.Config.ReplaceAlpha)
            {
                ev.IsAllowed = false;
                foreach (Room room in Room.List)
                    room.Color = Color.cyan;
                string cassie = ("pitch_0.2 .g3 .g3 .g3 pitch_0.9 attention . attention . activating omega warhead . detonation in 3 minutes . please evacuate in helicopter of surface zone or in the breach shelter . please evacuate now . 170 seconds before destruction .g3 . .  .g3 . .  . .g3 . . .g3 . . .g3 . . . . .g3 . . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 120 seconds  .g3 . .  .g3 . .  .g3 . .  .g3 . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .   .g3 . . .   .g3 . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   1 minute .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . 30 seconds . all checkpoint doors are open . please evacuate . 20 . 19 . 18 . 17 . 16 . 15 . 14 . 13 . 12 . 11 . 10 seconds 9 . 8 . 7 . 6 . 5 . 4 . 3 . 2 . 1 . pitch_0.7 0");
                Cassie.Message(cassie, false, false);
                Map.Broadcast(10, Plugin.Instance.Config.ActivatedMessage);
                foreach (Room room in Room.List)
                    room.Color = Color.cyan;
                Timing.CallDelayed(180, () =>
                {
                    foreach (Player Sobrevivientes in Player.List.Where(plr => plr.CurrentRoom.Name == "EZ_Shelter"))
                    {
                        Sobrevivientes.IsGodModeEnabled = true;
                        Timing.CallDelayed(1f, () =>
                        {
                            Sobrevivientes.IsGodModeEnabled = false;
                            Sobrevivientes.EnableEffect(EffectType.Flashed, 2);
                            Sobrevivientes.Position = new Vector3(-53, 988, -50);
                            Sobrevivientes.EnableEffect(EffectType.Visuals939, 5);
                        });
                        Timing.CallDelayed(1.5f, () =>
                        {
                            Timing.CallDelayed(0.2f, Warhead.Detonate);
                        });
                    }
                    foreach (Player Surface in Player.List.Where(plr => plr.Zone == ZoneType.Surface))
                        Surface.Kill("Omega Warhead");
                    foreach (Player Muertos in Player.List.Where(plr => plr.CurrentRoom.Name != "EZ_Shelter"))
                        Muertos.Kill("Omega Warhead");
                });
                Timing.CallDelayed(155, () =>
                {
                    //Map.Broadcast(10, "<color=blue> RESCUE HELICOPTER COMING TO SURFACE </color>");
                    //RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
                });
                foreach (Room room in Room.List)
                    room.Color = Color.blue;
            }
        }
    }
}
