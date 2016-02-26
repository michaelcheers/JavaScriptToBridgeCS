using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint.Parser;
using Jint.Parser.Ast;

namespace BridgeJavascript
{
    using CSharp_Generator;
    public class Translator
    {
        public Translator()
        {

        }

        public CSExpression Translate(Expression value)
        {
            if (value is Literal)
            {
                var expressionLiteral = (Literal)value;
                return new CSLiteral(expressionLiteral.Raw.Replace('\'', '\"'));
            }
            else if (value is BinaryExpression)
            {
                var expressionBinary = (BinaryExpression)value;
                return new CSBinaryExpression
                {
                    left = Translate(expressionBinary.Left),
                    right = Translate(expressionBinary.Right),
                    @operator = expressionBinary.Operator.ToOperatorString()
                };
            }
            else if (value is CallExpression)
            {
                var expressionCall = (CallExpression)value;
                CSExpression callee = Translate(expressionCall.Callee);
                CSExpression[] arguments = expressionCall.Arguments.ToList().ConvertAll(v => Translate(v)).ToArray();
                return new CSCallExpression
                {
                    callee = callee,
                    arguments = arguments
                };
            }
            else if (value is Identifier)
            {
                var expressionIdentifier = (Identifier)value;
                var name = expressionIdentifier.Name;
                string newName;
                if (identiferTable.TryGetValue(name, out newName)) name = newName;
                return new CSIdentifier
                {
                    name = name
                };
            }
            else if (value is MemberExpression)
            {
                var expressionMember = (MemberExpression)value;
                if (expressionMember.Computed)
                    throw new NotImplementedException();
                else
                    return new CSMemberExpression
                    {
                        @object = Translate(expressionMember.Object),
                        property = memberTable.ContainsKey(((Identifier)expressionMember.Property).Name) ? memberTable[((Identifier)expressionMember.Property).Name] : ((Identifier)expressionMember.Property).Name
                    };
            };
            throw new NotImplementedException();
        }

        public Dictionary<string, string> identiferTable = new Dictionary<string, string>
        {
            {"alert", "Global.Alert" },
            {"console", "Console" }
        };

        public Dictionary<string, string> memberTable = new Dictionary<string, string>
        {
            {"log", "Log" }
        };

        public CSStatement Translate(Statement value, bool functionNested = false)
        {
            if (value is ExpressionStatement)
            {
                var statementExpression = (ExpressionStatement)value;
                return new CSExpressionStatement(Translate(statementExpression.Expression));
            }
            else if (value is ReturnStatement)
            {
                var statementReturn = (ReturnStatement)value;
                return new CSReturnStatement(Translate(statementReturn.Argument));
            }
            else if (value is IfStatement)
            {
                var statementIf = (IfStatement)value;
                IEnumerable<CSStatement> consequent = Translate(GetBlockStatement(statementIf.Consequent));
                CSExpression test = Translate(statementIf.Test);
                IEnumerable<CSStatement> alternate = null;
                if (statementIf.Alternate != null)
                    alternate = Translate(GetBlockStatement(statementIf.Alternate));
                return new CSIfStatement
                {
                    alternate = alternate,
                    consequent = consequent,
                    test = test
                };
            }
            else if (value is VariableDeclaration)
            {
                var statementVariableDeclaration = (VariableDeclaration)value;
                var varDeclTrans = statementVariableDeclaration.Declarations.ToList().ConvertAll(v => new CSVariableDeclaration.VariableDeclarator
                {
                    id = new CSIdentifier { name = v.Id.Name },
                    type = "object"
                });
                var init = Translate(statementVariableDeclaration.Declarations.Last().Init);

                return new CSVariableDeclaration
                {
                    setTo = init,
                    value = varDeclTrans,
                    @static = !functionNested
                };
            }
            throw new NotSupportedException();
        }

        public static List<Statement> GetBlockStatement (Statement value)
        {
            if (value is BlockStatement)
                return ((BlockStatement)value).Body.ToList();
            else
                return new List<Statement> { value };
        }

        public string TranslateCode(string code)
        {
            JavaScriptParser parser = new JavaScriptParser();
            var parsed = parser.Parse(code);
            var Init = new CSFunctionDecl
            {
                attributes = new CSAttribute[]
                        {
                            new CSAttribute
                            {
                                callee = new CSLiteral("Init"),
                                arguments = new CSExpression[0]
                            }
                        },
                name = "Main",
                function = new CSFunction
                {
                    blocks = new List<CSStatement>()
                },
                keyWords = CSFunctionDecl.FuncKeywords.Static,
                returnType = "void"
            };
            var Program = new CSClass
            {
                functions = new List<CSFunctionDecl>
                {
                    Init
                },
                name = "Program",
                keyWords = CSFunctionDecl.FuncKeywords.Static
            };
            var NameSpace = new CSNameSpace
            {
                name = "NameSpace",
                elements = new List<CSElement>
                {
                    Program
                }
            };
            foreach (var item in parsed.Body)
            {
                if (item is FunctionDeclaration)
                {
                    var funcDecl = (FunctionDeclaration)item;
                    Program.functions.Add(Translate(funcDecl));
                }
                else
                    Init.function.blocks.Add(Translate(item));
            }
            return NameSpace.ToString();
        }

        public CSFunctionDecl Translate(FunctionDeclaration func)
        {
            return new CSFunctionDecl
            {
                function = new CSFunction
                {
                    blocks = Translate((func.Body as BlockStatement).Body, true),
                    attributes = new List<CSAttribute>(),
                    parameters = func.Parameters.ToList().ConvertAll(v => new CSFunction.Parameter(v.Name, "object"))
                },
                attributes = new CSAttribute[0],
                keyWords = CSFunctionDecl.FuncKeywords.Static,
                name = func.Id.Name,
                returnType = func.Body.As<BlockStatement>().Body.ToList().Exists(v => v is ReturnStatement) ? "object" : "void"
            };
        }

        public static string ToBlockFunction (IEnumerable<CSStatement> value)
        {
            string inForeach = "{\n";
            foreach (var item in value)
                inForeach += item.GenerateCS() + "\n";
            inForeach += "}";
            return inForeach;
        }

        public List<CSStatement> Translate(IEnumerable<Statement> body, bool functionNested = false)
        {
            List<CSStatement> result = new List<CSStatement>();
            foreach (var item in body)
                result.Add(Translate(item, functionNested));
            return result;
        }
    }
}