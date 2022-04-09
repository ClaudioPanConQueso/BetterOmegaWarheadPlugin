using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using System;
using System.Linq;
using UnityEngine;

namespace OmegaWarheadPlugin.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Activate : ICommand
    {
        public string Command { get; } = "activateomega";

        public string[] Aliases { get; } = null;

        public string Description { get; } = "Activates the Omega Warhead.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            foreach (Room room in Room.List)
                room.Color = Color.cyan;
            string cassie = ("pitch_0.2 .g3 .g3 .g3 pitch_0.9 attention . attention . activating omega warhead . detonation in 3 minutes . please evacuate in helicopter of surface zone or in the breach shelter . please evacuate now . 170 seconds before destruction .g3 . .  .g3 . .  . .g3 . . . .g3 . . . .g3 . . . . .g3 . . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . . .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 120 seconds  .g3 . .  .g3 . .  .g3 . .  .g3 . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .  .g3 . . .   .g3 . . .   .g3 . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   .g3 . . .   1 minute .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . .g3 .  . 30 seconds . all checkpoint doors are open . please evacuate . 20 . 19 . 18 . 17 . 16 . 15 . 14 . 13 . 12 . 11 . 10 seconds 9 . 8 . 7 . 6 . 5 . 4 . 3 . 2 . 1 . pitch_0.7 0");
            Cassie.Message(cassie, false, false);
            Map.Broadcast(10, Plugin.Instance.Config.ActivatedMessage);
            Timing.CallDelayed(180, () =>
            {
                foreach (Player Sobrevivientes in Player.List.Where(plr => plr.CurrentRoom.Name == "EZ_Shelter"))
                {
                    Sobrevivientes.IsGodModeEnabled = true;
                    Timing.CallDelayed(2f, () =>
                    {
                        Sobrevivientes.IsGodModeEnabled = false;
                        Sobrevivientes.EnableEffect(EffectType.Flashed, 2);
                        Sobrevivientes.Position = new Vector3(-53, 988, -50);
                        Sobrevivientes.EnableEffect(EffectType.Visuals939);
                    });
                }
                foreach (Player Surface in Player.List.Where(plr => plr.Zone == ZoneType.Surface))
                    Surface.Kill("Omega Warhead");
                Warhead.Detonate();
            });
            Timing.CallDelayed(155, () =>
            {
                //Map.Broadcast(10, "<color=blue> HELICOPTERO DE RESCATE LLEGANDO A SURFACE </color>");
                //RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
            });
            foreach (Room room in Room.List)
                room.Color = Color.blue;
            response = string.Empty;
            return false;
        }
    }
}
