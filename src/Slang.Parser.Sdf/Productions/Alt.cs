﻿using System;
using System.IO;
using Slang.Parsing;

namespace Slang.Parser.Sdf.Productions
{
	public sealed class Alt : ISort, IEquatable<Alt>
	{
		public ISymbol Left
		{ get; }

		public ISymbol Right
		{ get; }

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Alt"/> class.
		/// </summary>
		public Alt(ISymbol left, ISymbol right)
		{
			#region Contract
			if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (right == null)
                throw new ArgumentNullException(nameof(right));
			#endregion

			this.Left = left;
			this.Right = right;
		}
        #endregion

        #region Equality
        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as Alt);

        /// <inheritdoc />
        public bool Equals(Alt other)
        {
            if (Object.ReferenceEquals(other, null) ||      // When 'other' is null
                other.GetType() != this.GetType())          // or of a different type
                return false;                               // they are not equal.
            return Object.Equals(this.Left, other.Left)
                && Object.Equals(this.Right, other.Right);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 29 + this.Left.GetHashCode();
                hash = hash * 29 + this.Right.GetHashCode();
            }
            return hash;
        }
        #endregion

        /// <inheritdoc />
        public override string ToString()
		{
			return $"{this.Left} | {this.Right}";
		}
	}
}
