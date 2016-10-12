﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Slang.Parsing;

namespace Slang.Parser.Tests
{
    /// <summary>
    /// A sort.
    /// </summary>
    public sealed class Sort : ISort
    {
        /// <summary>
        /// Gets the name of the sort.
        /// </summary>
        /// <value>The name.</value>
        /// <remarks>This is for debugging purposes only.</remarks>
        public string Name { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Sort"/> class.
        /// </summary>
        /// <param name="name">The name of the sort.</param>
        public Sort(string name)
        {
            #region Contract
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            #endregion

            this.Name = name;
        }
        #endregion

        /// <inheritdoc />
        public override bool Equals(object obj) => Object.ReferenceEquals(this, obj);

        /// <inheritdoc />
        public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

        /// <inheritdoc />
        public override string ToString() => this.Name;
    }
}
