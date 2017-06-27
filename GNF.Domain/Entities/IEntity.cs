namespace GNF.Domain.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 是否是无需持久化实体对象
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();
    }

    public interface IEntity : IEntity<int>
    {

    }
}
