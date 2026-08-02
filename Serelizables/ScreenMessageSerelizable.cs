using System;
using System.ComponentModel;

using Exiled.API.Enums;
using Exiled.API.Features;

namespace SCP_600V.Serelizables
{
    public class ScreenMessageSerelizable
    {
        [Description("The message that will be displayed on the player's screen.")]
        public string Message { get; set; } = string.Empty;
        [Description("The duration in seconds that the message will be displayed on the player's screen.")]
        public float Duration { get; set; } = 5;
        [Description("The type of message to display.")]
        public MessageType MessageType { get; set; } = MessageType.Hint;
        /// <summary>
        /// Shows the message to the specified player.
        /// </summary>
        /// <param name="target">The player to display the message to.</param>
        /// <param name="handler">A function to process the message before displaying it.</param>
        public void Show(Player target, Func<string, string> handler = null)
        {
            if (target == null) return;
            string local = handler != null ? handler.Invoke(Message) : Message;
            if (string.IsNullOrEmpty(local)) return;
            switch (MessageType)
            {
                case MessageType.Hint:
                    target.ShowHint(local, Duration);
                    break;
                case MessageType.Broadcast:
                    target.Broadcast((ushort)Duration, local);
                    break;
                default:
                    target.ShowHint(local, Duration);
                    break;
            }
            Log.Debug($"[ScreenMessageSerelizable]: Showed message to {target.DisplayNickname}\nmessage: {local}\nduration: {Duration}\ntype: {MessageType}");
        }
    }
}