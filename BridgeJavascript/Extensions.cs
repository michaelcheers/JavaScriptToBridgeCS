using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript
{
    static class Extensions
    {
        public static string ToJSONString<T>(this Dictionary<string, T> value, Converter<T, string> toString)
        {
            string result = "{\n\t";
            foreach (var item in value)
            {
                result += item.Key + " = " + toString(item.Value) + ",\n";
            }
            result = result.Substring(0, result.Length - 2);
            result += "\n\b}";
            return result;
        }

        public static string ToOperatorString (this Jint.Parser.Ast.LogicalOperator value)
        {
            switch (value)
            {
                case Jint.Parser.Ast.LogicalOperator.LogicalAnd:
                    return "&&";
                case Jint.Parser.Ast.LogicalOperator.LogicalOr:
                    return "||";
            }
            throw new NotImplementedException();
        }
        public static string ToJSString (this object value)
        {
            if (value is string)
                return "\"" + value + "\"";
            else if (value is bool)
                return value.ToString().ToLower();
            else
                return value.ToString();
        }
        public static string ToOperatorString (this Jint.Parser.Ast.AssignmentOperator value)
        {
            switch (value)
            {
                case Jint.Parser.Ast.AssignmentOperator.Assign:
                    return "=";
                case Jint.Parser.Ast.AssignmentOperator.PlusAssign:
                    return "+=";
                case Jint.Parser.Ast.AssignmentOperator.MinusAssign:
                    return "-=";
                case Jint.Parser.Ast.AssignmentOperator.TimesAssign:
                    return "*=";
                case Jint.Parser.Ast.AssignmentOperator.DivideAssign:
                    return "/=";
                case Jint.Parser.Ast.AssignmentOperator.ModuloAssign:
                    return "%=";
                case Jint.Parser.Ast.AssignmentOperator.BitwiseAndAssign:
                    return "&=";
                case Jint.Parser.Ast.AssignmentOperator.BitwiseOrAssign:
                    return "|=";
                case Jint.Parser.Ast.AssignmentOperator.BitwiseXOrAssign:
                    return "^=";
                case Jint.Parser.Ast.AssignmentOperator.LeftShiftAssign:
                    return "<<=";
                case Jint.Parser.Ast.AssignmentOperator.RightShiftAssign:
                    return ">>=";
                case Jint.Parser.Ast.AssignmentOperator.UnsignedRightShiftAssign:
                    break;
                default:
                    break;
            }
            throw new NotImplementedException();
        }
        public static string ToOperatorString (this Jint.Parser.Ast.BinaryOperator value)
        {
            switch (value)
            {
                case Jint.Parser.Ast.BinaryOperator.Plus:
                    return "+";
                case Jint.Parser.Ast.BinaryOperator.Minus:
                    return "-";
                case Jint.Parser.Ast.BinaryOperator.Times:
                    return "*";
                case Jint.Parser.Ast.BinaryOperator.Divide:
                    return "/";
                case Jint.Parser.Ast.BinaryOperator.Modulo:
                    return "%";
                case Jint.Parser.Ast.BinaryOperator.Equal:
                    throw new ArgumentException("Equal not possible.");
                case Jint.Parser.Ast.BinaryOperator.NotEqual:
                    throw new ArgumentException("Not-Equal not possible.");
                case Jint.Parser.Ast.BinaryOperator.Greater:
                    return ">";
                case Jint.Parser.Ast.BinaryOperator.GreaterOrEqual:
                    return ">=";
                case Jint.Parser.Ast.BinaryOperator.Less:
                    return "<";
                case Jint.Parser.Ast.BinaryOperator.LessOrEqual:
                    return "<=";
                case Jint.Parser.Ast.BinaryOperator.StrictlyEqual:
                    return "==";
                case Jint.Parser.Ast.BinaryOperator.StricltyNotEqual:
                    return "!=";
                case Jint.Parser.Ast.BinaryOperator.BitwiseAnd:
                    return "&";
                case Jint.Parser.Ast.BinaryOperator.BitwiseOr:
                    return "|";
                case Jint.Parser.Ast.BinaryOperator.BitwiseXOr:
                    return "^";
                case Jint.Parser.Ast.BinaryOperator.LeftShift:
                    return "<<";
                case Jint.Parser.Ast.BinaryOperator.RightShift:
                    return ">>";
                case Jint.Parser.Ast.BinaryOperator.UnsignedRightShift:
                    break;
                case Jint.Parser.Ast.BinaryOperator.InstanceOf:
                    break;
                case Jint.Parser.Ast.BinaryOperator.In:
                    break;
                default:
                    break;
            }
            throw new ArgumentException("Inpossible operator: " + value);
        }
    }
}
