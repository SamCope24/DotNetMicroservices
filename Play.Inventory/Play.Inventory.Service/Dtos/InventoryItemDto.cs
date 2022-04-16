namespace Play.Inventory.Service.Dtos
{
    public record InventoryItemDto(Guid CatalogItemId, int Quantity, DateTimeOffset AcquiredDate);
}