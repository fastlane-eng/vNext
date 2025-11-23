using System.Collections.Generic;
using FastLane.Core.Models;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a basic inventory grid to display a collection of SKUs.
    /// </summary>
    public class InventoryGrid
    {
        /// <summary>
        /// Gets the collection of SKU items displayed in the grid.
        /// </summary>
        public IList<Sku> Items { get; }

        public InventoryGrid(IEnumerable<Sku> items)
        {
            Items = new List<Sku>(items);
        }

        /// <summary>
        /// Adds a SKU to the grid.
        /// </summary>
        public void AddItem(Sku sku) => Items.Add(sku);

        /// <summary>
        /// Removes a SKU from the grid.
        /// </summary>
        public void RemoveItem(Sku sku) => Items.Remove(sku);
    }
}
