using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastLane.Core.Interfaces;
using FastLane.Core.Models;
using FastLane.Data.Repositories;

namespace FastLane.Services
{
    public class PurchaseOrderEngine : IPurchaseOrderEngine
    {
        private readonly PurchaseOrderRepository _repository;
        private readonly IInventoryService _inventoryService;

        public PurchaseOrderEngine(PurchaseOrderRepository repository, IInventoryService inventoryService)
        {
            _repository = repository;
            _inventoryService = inventoryService;
        }

        public async Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));

            // Insert purchase order and retrieve it
            int poId = await _repository.InsertPurchaseOrderAsync(purchaseOrder);
            return await _repository.GetPurchaseOrderByIdAsync(poId);
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByIdAsync(int id)
        {
            return await _repository.GetPurchaseOrderByIdAsync(id);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync()
        {
            return await _repository.GetAllPurchaseOrdersAsync();
        }
    }
}
