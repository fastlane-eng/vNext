using System;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a simple popup dialog with a title and message.
    /// </summary>
    public class PopupDialog
    {
        public string Title { get; }
        public string Message { get; }

        public PopupDialog(string title, string message)
        {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// Displays the dialog. In this skeleton this writes to the console.
        /// </summary>
        public void Show()
        {
            Console.WriteLine($"[{Title}] {Message}");
        }
    }
}
