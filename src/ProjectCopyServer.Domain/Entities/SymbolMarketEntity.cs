using Volo.Abp.Domain.Entities;

namespace ProjectCopyServer.Entities;

public abstract class SymbolMarketEntity<TKey> : Entity, IEntity<TKey>
{
    /// <inheritdoc/>
    public virtual TKey Id { get; set; }

    protected SymbolMarketEntity()
    {
    }

    protected SymbolMarketEntity(TKey id)
    {
        Id = id;
    }

    public override object[] GetKeys()
    {
        return new object[] { Id };
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}