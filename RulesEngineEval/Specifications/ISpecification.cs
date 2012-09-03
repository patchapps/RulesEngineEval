using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IJM.Specification
{
    /// <summary>
    /// Defines a interface for implementing a Specification Pattern 
    /// </summary>
    public interface ISpecification<TEntity>
    {
        #region Method Declarations
        /// <summary>
        /// Is the specification satisified by the entity?
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsSatisfiedBy(TEntity entity);
        #endregion
    }
}
