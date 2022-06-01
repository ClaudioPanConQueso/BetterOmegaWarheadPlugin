using CommandSystem;
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
            if (sender.CheckPermission(PlayerPermissions.WarheadEvents))
            {
                Plugin.Singleton.handler.StopOmega();
                response = "Omega Warhead stopped.";
                return false;
            }
            else
            {
                response = "You need Warhead Events permissions to use this command";
                return true;
            }
        }
    }
}
