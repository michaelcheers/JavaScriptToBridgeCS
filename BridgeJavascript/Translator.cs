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

        public CSVariableDeclaration.VariableDeclarator Translate (VariableDeclarator value, CSClass csClassRef)
        {
            return new CSVariableDeclaration.VariableDeclarator
            {
                id = (CSIdentifier)Translate(value.Id, csClassRef),
                type = "object"
            };
        }

        public CSExpression Translate(Expression value, CSClass csClassRef)
        {
            if (value == null)
                return new CSLiteral("null");
            if (value is Literal)
            {
                var expressionLiteral = (Literal)value;
                return new CSLiteral(expressionLiteral.Value.ToJSString());
            }
            else if (value is BinaryExpression)
            {
                var expressionBinary = (BinaryExpression)value;
                return new CSBinaryExpression
                {
                    left = Translate(expressionBinary.Left, csClassRef),
                    right = Translate(expressionBinary.Right, csClassRef),
                    @operator = expressionBinary.Operator.ToOperatorString()
                };
            }
            else if (value is CallExpression)
            {
                var expressionCall = (CallExpression)value;
                CSExpression callee = Translate(expressionCall.Callee, csClassRef);
                CSExpression[] arguments = expressionCall.Arguments.ToList().ConvertAll(v => Translate(v, csClassRef)).ToArray();
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
                    return new CSIndexOperator
                    {
                        @object = Translate(expressionMember.Object, csClassRef),
                        property = Translate(expressionMember.Property, csClassRef)
                    };
                else
                    return new CSMemberExpression
                    {
                        @object = Translate(expressionMember.Object, csClassRef),
                        property = memberTable.ContainsKey(((Identifier)expressionMember.Property).Name) ? memberTable[((Identifier)expressionMember.Property).Name] : ((Identifier)expressionMember.Property).Name
                    };
            }
            else if (value is AssignmentExpression)
            {
                var expressionAssignment = (AssignmentExpression)value;
                var left = expressionAssignment.Left;
                var @operator = expressionAssignment.Operator;
                var right = expressionAssignment.Right;
                return new CSAssignmentExpression
                {
                    left = Translate(left, csClassRef),
                    right = Translate(right, csClassRef),
                    @operator = @operator.ToOperatorString()
                };
            }
            else if (value is UpdateExpression)
            {
                var expressionUpdate = (UpdateExpression)value;
                switch (expressionUpdate.Operator)
                {
                    case UnaryOperator.BitwiseNot:
                        break;
                    case UnaryOperator.Void:
                        break;
                    case UnaryOperator.TypeOf:
                        break;
                    case UnaryOperator.Increment:
                        return new CSUnaryExpression
                        {
                            @operator = "++",
                            valPos = expressionUpdate.Prefix ? CSUnaryExpression.Position.Right : CSUnaryExpression.Position.Left,
                            value = Translate(expressionUpdate.Argument, csClassRef)
                        };
                    case UnaryOperator.Decrement:
                        return new CSUnaryExpression
                        {
                            @operator = "--",
                            valPos = expressionUpdate.Prefix ? CSUnaryExpression.Position.Right : CSUnaryExpression.Position.Left,
                            value = Translate(expressionUpdate.Argument, csClassRef)
                        };
                    default:
                        break;
                }
            }
            else if (value is UnaryExpression)
            {
                var expressionUnary = (UnaryExpression)value;
                switch (expressionUnary.Operator)
                {
                    case UnaryOperator.LogicalNot:
                        return new CSUnaryExpression
                        {
                            @operator = "!",
                            value = Translate(expressionUnary.Argument, csClassRef),
                            valPos = CSUnaryExpression.Position.Right
                        };
                    case UnaryOperator.Plus:
                        return new CSCallExpression
                        {
                            arguments = new CSExpression[] { Translate(expressionUnary.Argument, csClassRef) },
                            callee = new CSMemberExpression
                            {
                                @object = new CSIdentifier
                                {
                                    name = "double"
                                },
                                property = "Parse"
                            }
                        };
                    case UnaryOperator.Minus:
                        return new CSUnaryExpression
                        {
                            @operator = "-",
                            value = Translate(expressionUnary.Argument, csClassRef),
                            valPos = CSUnaryExpression.Position.Right
                        };
                    case UnaryOperator.Delete:
                        return new CSCallExpression
                        {
                            arguments = new CSExpression[] { Translate(expressionUnary.Argument, csClassRef) },
                            callee = new CSMemberExpression
                            {
                                @object = new CSIdentifier { name = "Script" },
                                property = "Delete"
                            }
                        };
                }
            }
            else if (value is NewExpression)
            {
                var expressionNew = (NewExpression)value;
                return new CSNewExpression
                {
                    callee = Translate(expressionNew.Callee, csClassRef),
                    arguments = expressionNew.Arguments.ToList().ConvertAll(v => Translate(v, csClassRef)).ToArray()
                };
            }
            else if (value is ArrayExpression)
            {
                var expressionArray = (ArrayExpression)value;
                return new CSArrayExpression
                {
                    elements = expressionArray.Elements.ToList().ConvertAll(v => Translate(v, csClassRef)).ToArray()
                };
            }
            else if (value is LogicalExpression)
            {
                var expressionLogical = (LogicalExpression)value;
                return new CSBinaryExpression
                {
                    left = Translate(expressionLogical.Left, csClassRef),
                    right = Translate(expressionLogical.Right, csClassRef),
                    @operator = expressionLogical.Operator.ToOperatorString()
                };
            }
            throw new NotImplementedException();
        }

        public Dictionary<string, string> identiferTable = new Dictionary<string, string>
        {
            {"alert", "Global.Alert" },
            {"console", "Console" },
            {"Image", "ImageElement" },
            {"document", "Global.Document" },
            {"setTimeout", "Global.SetTimeout" }
        };

        public Dictionary<string, string> memberTable = new Dictionary<string, string>
        {
            {"log", "Log" },
            {"getElementById", "GetElementById" },
            {"random", "Random" },
            {"toString", "ToString" }
        };

        public CSStatement Translate (SyntaxNode value, CSClass csClassRef)
        {
            if (value is Statement)
                return Translate((Statement)value, csClassRef, true);
            else if (value is Expression)
                return new CSExpressionStatement(Translate((Expression)value, csClassRef));
            throw new NotImplementedException(value.GetType() + " is not stopped.");
        }

        public CSStatement Translate (Statement value, CSClass csClassRef, bool functionNested = false)
        {
            if (value is ExpressionStatement)
            {
                var statementExpression = (ExpressionStatement)value;
                return new CSExpressionStatement(Translate(statementExpression.Expression, csClassRef));
            }
            else if (value is ReturnStatement)
            {
                var statementReturn = (ReturnStatement)value;
                return new CSReturnStatement(Translate(statementReturn.Argument, csClassRef));
            }
            else if (value is IfStatement)
            {
                var statementIf = (IfStatement)value;
                IEnumerable<CSStatement> consequent = Translate(GetBlockStatement(statementIf.Consequent), csClassRef);
                CSExpression test = Translate(statementIf.Test, csClassRef);
                IEnumerable<CSStatement> alternate = null;
                if (statementIf.Alternate != null)
                    alternate = Translate(GetBlockStatement(statementIf.Alternate), csClassRef);
                return new CSIfStatement
                {
                    alternate = alternate,
                    consequent = consequent,
                    test = test
                };
            }
            else if (value is WhileStatement)
            {
                var statementWhile = (WhileStatement)value;
                IEnumerable<CSStatement> body = Translate(GetBlockStatement(statementWhile.Body), csClassRef);
                CSExpression test = Translate(statementWhile.Test, csClassRef);
                return new CSWhileStatement
                {
                    body = body,
                    @do = false,
                    test = test
                };
            }
            else if (value is ForStatement)
            {
                var statementFor = (ForStatement)value;
                var body = Translate(GetBlockStatement(statementFor.Body), csClassRef);
                var init = Translate(statementFor.Init, csClassRef);
                var test = Translate(statementFor.Test, csClassRef);
                var update = Translate(statementFor.Update, csClassRef);
                return new CSForStatement
                {
                    body = body,
                    init = init,
                    test = test,
                    update = update
                };
            }
            else if (value is DoWhileStatement)
            {
                var statementDoWhile = (DoWhileStatement)value;
                IEnumerable<CSStatement> body = Translate(GetBlockStatement(statementDoWhile.Body), csClassRef);
                CSExpression test = Translate(statementDoWhile.Test, csClassRef);
                return new CSWhileStatement
                {
                    body = body,
                    @do = true,
                    test = test
                };
            }
            else if (value is EmptyStatement)
                return new CSEmptyStatement(true);
            else if (value is VariableDeclaration)
            {
                var statementVariableDeclaration = (VariableDeclaration)value;
                var varDeclTrans = statementVariableDeclaration.Declarations.ToList().ConvertAll(v => new CSVariableDeclaration.VariableDeclarator
                {
                    id = new CSIdentifier { name = v.Id.Name },
                    type = "object"
                });

                var init = Translate(statementVariableDeclaration.Declarations.Last().Init, csClassRef);

                if (functionNested)
                    return new CSVariableDeclaration
                    {
                        setTo = init,
                        value = varDeclTrans
                    };
                else
                {
                    csClassRef.declarables.Add(new CSStaticVariable
                    {
                        value = statementVariableDeclaration.Declarations.ToList().ConvertAll(v => Translate(v, csClassRef)),
                        setTo = init
                    });
                    return new CSEmptyStatement(false);
                }
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
                declarables = new List<CSClass.Declarable>
                {
                    Init
                },
                attributes = new List<CSAttribute>
                {
                    new CSAttribute
                    {
                        arguments = new CSExpression[] {new CSLiteral("\"\"") },
                        callee = new CSIdentifier{name = "GlobalTarget" }
                    }
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
                    Program.declarables.Add(Translate(funcDecl, Program));
                }
                else
                    Init.function.blocks.Add(Translate(item, Program));
            }
            return NameSpace.ToString();
        }

        public CSFunctionDecl Translate(FunctionDeclaration func, CSClass csClassRef)
        {
            return new CSFunctionDecl
            {
                function = new CSFunction
                {
                    blocks = Translate((func.Body as BlockStatement).Body, csClassRef, true),
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
            string inForeach = "{\n\t";
            foreach (var item in value)
                inForeach += item.GenerateCS() + "\n";
            inForeach = inForeach.Remove(inForeach.Length - 1);
            inForeach += "\b\n}";
            return new TabString(inForeach).Value;
        }

        public List<CSStatement> Translate(IEnumerable<Statement> body, CSClass csClassRef, bool functionNested = false)
        {
            List<CSStatement> result = new List<CSStatement>();
            foreach (var item in body)
                result.Add(Translate(item, csClassRef, functionNested));
            return result;
        }
    }
}