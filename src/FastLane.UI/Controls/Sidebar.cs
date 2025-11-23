using System.Collections.Generic;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a sidebar component with a collection of menu items.
    /// </summary>
    public class Sidebar
    {
        public IList<string> MenuItems { get; }

        public Sidebar(IEnumerable<string> menuItems)
        {
            MenuItems = new List<string>(menuItems);
        }
    }
}
