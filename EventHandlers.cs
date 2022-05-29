using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;
using Map = Exiled.API.Features.Map;

namespace OmegaWarheadPlugin
{
    class EventHandlers
    {
        public void OnWarheadStart(StartingEventArgs ev)
        {
            if (Plugin.Singleton.Config.ReplaceAlpha)
            {
                ev.IsAllowed = false;
                OmegaWarhead();
            }
        }
        public void OmegaWarhead()
        {
            Round.IsLocked = true;

            foreach (Room room in Room.List)
                room.Color = Color.cyan;

            Cassie.Message(Plugin.Singleton.Config.Cassie, false, false);
            Map.Broadcast(10, Plugin.Singleton.Config.ActivatedMessage);

            Timing.CallDelayed(150, () =>
            {
                foreach (Door checkpoint in Door.List)
                {
                    if (checkpoint.Type == DoorType.CheckpointEntrance || checkpoint.Type == DoorType.CheckpointLczA || checkpoint.Type == DoorType.CheckpointLczB)
                    {
                        checkpoint.IsOpen = true;
                        checkpoint.Lock(69420, DoorLockType.Warhead);
                    }
                }
            });

            Timing.CallDelayed(179 + Plugin.Singleton.Config.TimeToExplodeAfterCassie, () =>
            {
                Round.IsLocked = false;
                foreach (Player People in Player.List)
                {
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
                    else
                    {
                        People.Kill("Omega Warhead");
                    }
                }

                foreach (Room room in Room.List)
                    room.Color = Color.blue;
            });

            //TO-DO
            /*Timing.CallDelayed(155, () =>
            {
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "10");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "9");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "8");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "7");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "6");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "5");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "4");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "3");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "2");
                Map.Broadcast(1, Plugin.Singleton.Config.HelicopterMessage + "1");
                RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
            });*/
        }
    }
}
