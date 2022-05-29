using CommandSystem;
using System;
namespace OmegaWarheadPlugin.Commands
{
    public class Stop : ICommand
    {
        public string Command { get; } = "stopomega";
        public string[] Aliases { get; } = null;
        public string Description { get; } = "stops the Omega Warhead.";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // TO-DO
            if (sender.CheckPermission(PlayerPermissions.WarheadEvents))
            {
                //Cassie.Message("Omega Warhead detonation cancelled");
                response = "Omega Warhead stopped.";
                return false;
            }
            else
            {
                response = "You need Warhead Events permissions to use this commands";
                return true;
            }
        }
    }
}
