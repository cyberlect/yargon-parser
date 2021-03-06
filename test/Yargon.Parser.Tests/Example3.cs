﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Yargon.Parsing;

namespace Yargon.Parser
{
    /// <summary>
    /// An example that has a grammar with a reject production.
    /// </summary>
    [TestFixture]
    public sealed class Example3 : ExampleBase
    {
        [Test]
        public void Test()
        {
            // Arrange
            // Sorts
            var S = new Sort("S");
            var E = new Sort("E");
            var I = new Sort("I");
            var T = new Sort("T");
            // Token types
            var ths = new TokenType("ths");
            var eof = new TokenType("$");
            var tokens = new[] {ths, eof};
            // Reductions
            var r0 = new Reduction(S, 2);                   // S → E $
            var r1 = new Reduction(E, 1);                   // E → I
            var r2 = new Reduction(E, 1);                   // E → T
            var r3 = new Reduction(I, 1);                   // I → ths
            var r4 = new Reduction(T, 1);                   // T → ths
            var r5 = new Reduction(I, 1, rejects: true);    // I → ths {reject}
            // States
            var s0 = new State("s0");
            var s1 = new State("s1");
            var s2 = new State("s2");
            var s3 = new State("s3");
            var s4 = new State("s4");
            var s5 = new State("s5");

            // Parse table
            var startState = s0;
            var shifts = new Dictionary<Tuple<State, ITokenType>, State>()
            {
                [Tuple.Create(s0, (ITokenType)ths)] = s1,

                [Tuple.Create(s4, (ITokenType)eof)] = s5,
            };
            var gotos = new Dictionary<Tuple<State, ISort>, State>()
            {
                [Tuple.Create(s0, (ISort)I)] = s2,
                [Tuple.Create(s0, (ISort)T)] = s3,
                [Tuple.Create(s0, (ISort)E)] = s4,
            };
            var reductions = new Dictionary<State, IReadOnlyCollection<Reduction>>()
            {
                [s1] = new[] { r3, r4, r5 },
                [s2] = new[] { r1 },
                [s3] = new[] { r2 },
                [s5] = new[] { r0 },
            };
            var parseTable = new ParseTable(startState, shifts, gotos, Expand(reductions, tokens));
            var parseTreeBuilder = new ParseTreeBuilder();
            var parser = new YargonParser<State, TypedToken<String>, IParseTree>(parseTable, parseTreeBuilder, new FailingErrorHandler<State, TypedToken<String>>());

            // Input: 1-(2-3)$
            var input = Collect(new[]
            {
                Tuple.Create("this", ths),
                Tuple.Create("$",    eof),
            });

            // Act
            var result = parser.Parse(TokenProvider.From(input));

            // Assert: E(T("this"))
            Assert.That(result.Success, Is.True);
            Assert.That(StripLocations(result.Tree), Is.EqualTo(Node(E,Node(T, Token(ths, "this")))));
        }
        
    }
}
