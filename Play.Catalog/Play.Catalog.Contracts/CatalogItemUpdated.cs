namespace Play.Catalog.Contracts
{
    public record CatalogItemUpdated(Guid ItemId, string Name, string Description);
}