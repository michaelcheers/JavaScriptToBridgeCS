using Microsoft.VisualStudio.TestTools.UnitTesting;
using BridgeJavascript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint.Parser.Ast;

namespace BridgeJavascript.Tests
{
    [TestClass]
    public class TranslatorTests
    {
        [TestMethod]
        public void GetBlockStatementTest()
        {
            Assert.IsTrue(Translator.GetBlockStatement(new EmptyStatement())[0] is EmptyStatement);
            CollectionAssert.AllItemsAreInstancesOfType(Translator.GetBlockStatement(new BlockStatement { Body = new Statement[] { new EmptyStatement(), new EmptyStatement() } }), typeof(EmptyStatement));
        }
    }
}