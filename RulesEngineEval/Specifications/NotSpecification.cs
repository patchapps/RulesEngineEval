namespace IJM.Specification
{
    /// <summary>
    /// Checks that a specification does not match a condition
    /// </summary>
    internal class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        #region Field(s)
        private ISpecification<TEntity> _specification;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Initialise a new instance of the NotSpecification class
        /// </summary>
        /// <param name="specification"></param>
        internal NotSpecification(ISpecification<TEntity> specification)
        {
            this._specification = specification;
        }
        #endregion

        #region Public Method(s)
        /// <summary>
        /// Is the specification satisified by the entity?
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsSatisfiedBy(TEntity entity)
        {
            return !(this._specification.IsSatisfiedBy(entity));
        }
        #endregion
    }
}
