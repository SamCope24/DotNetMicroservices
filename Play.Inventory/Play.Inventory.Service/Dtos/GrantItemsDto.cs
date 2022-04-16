namespace Play.Inventory.Service.Dtos
{
    public record GrantItemsDto(Guid UserId, Guid CatalogItemId, int Quantity);
}