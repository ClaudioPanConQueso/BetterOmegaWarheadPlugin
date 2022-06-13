using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Respawning;
using System.Collections.Generic;
using UnityEngine;
using Map = Exiled.API.Features.Map;

namespace OmegaWarheadPlugin
{
    class EventHandlers
    {
        public bool OmegaActivated = false;
        public bool OmegaDetonated = false;
        public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        public List<Player> HelikopterSurvivors = new List<Player>();

        public void OnRestartingRound()
        {
            foreach (var coroutine in Coroutines)
                Timing.KillCoroutines(coroutine);
            HelikopterSurvivors.Clear();
            OmegaDetonated = false;
            OmegaActivated = false;
            Coroutines.Clear();
        }

        public void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (OmegaDetonated) ev.IsAllowed = false;
        }

        public void OnWarheadStart(StartingEventArgs ev)
        {
            if (Plugin.Singleton.Config.ReplaceAlpha && !OmegaActivated)
            {
                ev.IsAllowed = false;
                OmegaWarhead();
            }
            if (OmegaActivated || OmegaDetonated)
            {
                ev.IsAllowed = false;
            }
        }

        public void StopOmega()
        {
            OmegaDetonated = false;
            OmegaActivated = false;
            Cassie.Clear();
            HelikopterSurvivors.Clear();
            Cassie.Message(Plugin.Singleton.Config.StopCassie, false, false);
            foreach (var coroutine in Plugin.Singleton.handler.Coroutines)
                Timing.KillCoroutines(coroutine);
            foreach (Room room in Room.List)
                room.ResetColor();
        }

        public void OmegaWarhead()
        {
            OmegaActivated = true;
            foreach (Room room in Room.List)
                room.Color = Color.cyan;

            Cassie.Clear();
            Cassie.Message(Plugin.Singleton.Config.Cassie, false, false);
            Map.Broadcast(10, Plugin.Singleton.Config.ActivatedMessage);

            Coroutines.Add(Timing.CallDelayed(150, () =>
            {
                foreach (Door checkpoint in Door.List)
                {
                    if (checkpoint.Type == DoorType.CheckpointEntrance || checkpoint.Type == DoorType.CheckpointLczA || checkpoint.Type == DoorType.CheckpointLczB)
                    {
                        checkpoint.IsOpen = true;
                        checkpoint.Lock(69420, DoorLockType.Warhead);
                    }
                }
            }));

            Coroutines.Add(Timing.CallDelayed(179 + Plugin.Singleton.Config.TimeToExplodeAfterCassie, () =>
            {
                OmegaDetonated = true;
                foreach (Room room in Room.List)
                    room.Color = Color.blue;
                foreach (Player People in Player.List)
                    if (People.CurrentRoom.Type == RoomType.EzShelter)
                    {
                        People.IsGodModeEnabled = true;
                        Timing.CallDelayed(0.2f, () =>
                        {
                            People.IsGodModeEnabled = false;
                            People.EnableEffect(EffectType.Flashed, 2);
                            People.Position = new Vector3(-53, 988, -50);
                            People.EnableEffect(EffectType.Visuals939, 5);
                            Warhead.Detonate();
                            Warhead.Shake();
                        });
                    }
                    else if (!HelikopterSurvivors.Contains(People))
                    {
                        People.Kill("Omega Warhead");
                    }
                Timing.CallDelayed(4, () =>
                {
                    foreach (Player Helikopter in Player.List)
                        if (HelikopterSurvivors.Contains(Helikopter))
                        {
                            Helikopter.DisableAllEffects();
                            Helikopter.Scale = new Vector3(1, 1, 1);
                            Helikopter.Position = new Vector3(178, 1000, -59);
                            Timing.CallDelayed(2, HelikopterSurvivors.Clear);
                        }
                });
            }));

            Coroutines.Add(Timing.CallDelayed(158, () =>
            {
                for (int i = 10; i > 0; i--)
                    Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + i);
                Timing.CallDelayed(12, () =>
                {
                    Vector3 HelicopterZone = new Vector3(178, 993, -59);
                    foreach (Player player in Player.List)
                        if (Vector3.Distance(player.Position, HelicopterZone) <= 12)
                        {
                            player.Broadcast(4, Plugin.Singleton.Config.HelicopterEscape);
                            player.Position = new Vector3(293, 978, -52);
                            player.Scale = new Vector3(0, 0, 0);
                            player.EnableEffect(EffectType.Flashed, 12);
                            HelikopterSurvivors.Add(player);
                            Timing.CallDelayed(0.5f, () =>
                            {
                                player.EnableEffect(EffectType.Ensnared);
                            });
                        }
                });
                RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
            }));
        }
    }
}