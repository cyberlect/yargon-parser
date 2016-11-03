﻿using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using System.Reflection;
using Slang.Parser.Sdf.ParseTrees;
using Slang.Parser.Sdf.Productions.IO;
using Slang.Parsing;
using Virtlink.ATerms;
using Virtlink.ATerms.IO;

namespace Slang.Parser.Sdf
{
	[TestFixture]
	public class SdfParseTableReaderTests
    {
		[Test]
		public void ReadParseTableFromFile()
		{
            // Arrange
		    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Slang.Parser.Sdf.Parsetable1.tbl");
            Debug.Assert(stream != null);
            var termFactory = new TrivialTermFactory();
            var productionFormat = new TermProductionFormat(termFactory);
            var reader = new SdfParseTableReader(productionFormat);
            
            // Act/Assert
            Assert.That(() =>
            {
                var parseTable = reader.Read(new StreamReader(stream));
            }, Throws.Nothing);

            // Cleanup
            stream.Dispose();
		}

        [Test]
	    public void ParseString()
        {
            // Arrange
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Slang.Parser.Sdf.Parsetable1.tbl");
            var termFactory = new TrivialTermFactory();
            var productionFormat = new TermProductionFormat(termFactory);
            var reader = new SdfParseTableReader(productionFormat);
            var parseTable = reader.Read(new StreamReader(stream));            
            var parseTreeBuilder = new TrivialParseNodeFactory();
            var errorHandler = new FailingErrorHandler<SdfStateRef, Token<CodePoint>>();
            var parser = new SlangParser<SdfStateRef, Token<CodePoint>, IParseNode>(parseTable, parseTreeBuilder, errorHandler);

            // Act
            string input = "module Test entity X {}";
            var result = parser.Parse(new CharacterProvider(new StringReader(input)));

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Tree, Is.Null);

            // Cleanup
            stream.Dispose();
        }
    }
}
