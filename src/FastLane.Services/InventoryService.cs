using System.Collections.Generic;
using System.Threading.Tasks;
using FastLane.Core.Interfaces;
using FastLane.Core.Models;
using FastLane.Data.Repositories;

namespace FastLane.Services
{
    /// <summary>
    /// Provides inventory-related operations backed by the data repository.
    /// </summary>
    public class InventoryService : IInventoryService
    {
        private readonly InventoryRepository _repository;

        public InventoryService(InventoryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Sku>> GetAllSkusAsync()
        {
            return _repository.GetAllSkusAsync();
        }

        public Task<Sku> GetSkuByIdAsync(int id)
        {
            return _repository.GetSkuByIdAsync(id);
        }

        public Task UpdateInventoryAsync(int skuId, int quantityDelta)
        {
            return _repository.UpdateInventoryAsync(skuId, quantityDelta);
        }
    }
}
