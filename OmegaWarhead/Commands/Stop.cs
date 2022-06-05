using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
namespace OmegaWarheadPlugin.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Stop : ICommand
    {
        public string Command { get; } = "stopomega";
        public string[] Aliases { get; } = null;
        public string Description { get; } = "stops the Omega Warhead.";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission("omegawarhead"))
            {
                if (Plugin.Singleton.handler.OmegaActivated)
                {
                    Plugin.Singleton.handler.StopOmega();
                    response = "Omega Warhead stopped.";
                    return false;
                }
                else
                {
                    response = "Omega Warhead is already stopped.";
                    return false;
                }
            }
            else
            {
                response = "You need Warhead Events permissions to use this command";
                return true;
            }
        }
    }
}