using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

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
            if (sender.CheckPermission(Plugin.Singleton.Config.Permissions))
            {
                if (!Plugin.Singleton.handler.OmegaActivated)
                {
                    Plugin.Singleton.handler.OmegaWarhead();
                    response = "Omega Warhead activated.";
                    return false;
                }
                else
                {
                    response = "Omega Warhead is already activated.";
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