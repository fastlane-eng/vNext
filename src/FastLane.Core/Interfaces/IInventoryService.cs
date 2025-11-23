using System.Collections.Generic;
using System.Threading.Tasks;
using FastLane.Core.Models;

namespace FastLane.Core.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Sku>> GetAllSkusAsync();
        Task<Sku?> GetSkuByIdAsync(int id);
        Task UpdateInventoryAsync(int skuId, int quantityDelta);
    }
}
