﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slang.Parser.Sdf.Productions.IO;
using Virtlink.ATerms;

namespace Slang.Parser.Sdf.ParseTrees
{
    /// <summary>
    /// A parse tree imploder.
    /// </summary>
    public sealed class AsfixImploder : IParseNodeVisitor<ITerm>
    {
        private readonly TermFactory factory;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AsfixImploder"/> class.
        /// </summary>
        /// <param name="factory">The term factory to use.</param>
        public AsfixImploder(TermFactory factory)
        {
            #region Contract
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));
            #endregion

            this.factory = factory;
        }
        #endregion

        /// <summary>
        /// Implodes the specified parse tree.
        /// </summary>
        /// <param name="parseTree">The parse tree to implode.</param>
        /// <returns>The resulting AST.</returns>
        public ITerm Implode(IParseNode parseTree)
        {
            #region Contract
            if (parseTree == null)
                throw new ArgumentNullException(nameof(parseTree));
            #endregion

            return parseTree.Accept(this);
        }

        /// <inheritdoc />
		ITerm IParseNodeVisitor<ITerm>.VisitApplication(IApplicationParseNode node)
        {
            #region Contract
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            #endregion

            
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        ITerm IParseNodeVisitor<ITerm>.VisitAmbiguity(IAmbiguityParseNode node)
        {
            #region Contract
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            #endregion

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        ITerm IParseNodeVisitor<ITerm>.VisitProduction(IProductionParseNode node)
        {
            #region Contract
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            #endregion

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        ITerm IParseNodeVisitor<ITerm>.VisitParseNode(IParseNode node)
        {
            #region Contract
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            #endregion

            throw new NotSupportedException("Unknown parse node type.");
        }

        /// <inheritdoc />
        ITerm IParseNodeVisitor<ITerm>.VisitCycle(ICycleParseNode node)
        {
            #region Contract
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            #endregion

            throw new NotImplementedException();
        }
    }
}
