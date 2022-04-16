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
            if(Plugin.Singleton.Config.ReplaceAlpha)
            {
                ev.IsAllowed = false;
                OmegaWarhead();
            }
        }
        public void OmegaWarhead()
        {
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
            var handle = Timing.CallDelayed(180, () =>
            {
                foreach (Player Sobrevivientes in Player.List)
                {
                    if (Sobrevivientes.CurrentRoom.Type == RoomType.EzShelter)
                    {
                        Sobrevivientes.IsGodModeEnabled = true;
                        Timing.CallDelayed(Plugin.Singleton.Config.TimeToExplodeAfterCassie - 0.2f, () =>
                        {
                            Sobrevivientes.IsGodModeEnabled = false;
                            Sobrevivientes.EnableEffect(EffectType.Flashed, 2);
                            Sobrevivientes.Position = new Vector3(-53, 988, -50);
                            Sobrevivientes.EnableEffect(EffectType.Visuals939, 5);
                        });
                    }
                    Timing.CallDelayed(Plugin.Singleton.Config.TimeToExplodeAfterCassie, Warhead.Detonate);
                }
                foreach (Player Muertos in Player.List)
                    if (Muertos.CurrentRoom.Type != RoomType.EzShelter)
                    {
                        Muertos.Kill("Omega Warhead");
                    }
                foreach (Room room in Room.List)
                    room.Color = Color.blue;
            });
            Timing.CallDelayed(155, () =>
            {
                //Map.Broadcast(10, "<color=blue> RESCUE HELICOPTER COMING TO SURFACE </color>");
                //RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);
            });
        }
    }
}
