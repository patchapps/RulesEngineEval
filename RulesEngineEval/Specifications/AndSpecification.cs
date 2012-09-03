namespace IJM.Specification
{
    /// <summary>
    /// Joins two Specifications in an And condition
    /// </summary>
    internal class AndSpecification<TEntity> : ISpecification<TEntity>
    {
        #region Field(s)
        private ISpecification<TEntity> _specificationOne;
        private ISpecification<TEntity> _specificationTwo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Initialise a new instance of the AndSpecification class
        /// </summary>
        /// <param name="specificationOne"></param>
        /// <param name="specificationTwo"></param>
        internal AndSpecification(ISpecification<TEntity> specificationOne, ISpecification<TEntity> specificationTwo)
        {
            this._specificationOne = specificationOne;
            this._specificationTwo = specificationTwo;
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
            return (this._specificationOne.IsSatisfiedBy(entity) && this._specificationTwo.IsSatisfiedBy(entity));
        }
        #endregion
    }
}
