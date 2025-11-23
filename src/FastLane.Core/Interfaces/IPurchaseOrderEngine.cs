using System.Collections.Generic;
using System.Threading.Tasks;
using FastLane.Core.Models;

namespace FastLane.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for the Purchase Order engine.
    /// </summary>
    public interface IPurchaseOrderEngine
    {
        /// <summary>
        /// Creates a new purchase order given a supplier and line items.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="lines">The purchase order lines.</param>
        /// <returns>The newly created purchase order.</returns>
        Task<PurchaseOrder> CreatePurchaseOrderAsync(int supplierId, IEnumerable<PurchaseOrder.PurchaseOrderLine> lines);

        /// <summary>
        /// Retrieves a purchase order by its identifier.
        /// </summary>
        /// <param name="purchaseOrderId">The purchase order identifier.</param>
        /// <returns>The purchase order if found; otherwise null.</returns>
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int purchaseOrderId);
    }
}
