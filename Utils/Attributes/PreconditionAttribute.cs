using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Gradient.Utils.Attributes
{
    [PSerializable]
    public class PreconditionAttribute : OnMethodBoundaryAspect
    {
        private string _errorMessage;
        private string _expression;
        private string _parameterName;

        public PreconditionAttribute(string parameterName, string expression, string? errorMessage = null)
        {
            _parameterName = parameterName;
            _expression = expression;
            _errorMessage = errorMessage ?? $"Parameter preconditions not met: {expression}";
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var method = args.Method as MethodInfo;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var parameters = method.GetParameters();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (parameter.Name != _parameterName) continue; // Only validate the specified parameter

                var value = args.Arguments[i];
                var parameterType = parameter.ParameterType;
                var lambda = DynamicExpressionParser.ParseLambda(new[] { Expression.Parameter(parameterType, parameter.Name) }, typeof(bool), _expression);
                var compiledLambda = lambda.Compile();
                var invokePrecondition = compiledLambda.DynamicInvoke(value);
#pragma warning disable CS8605 // Unboxing a possibly null value.
                if (!(bool)invokePrecondition)
                {
                    throw new ArgumentException(_errorMessage, parameter.Name);
                }
#pragma warning restore CS8605 // Unboxing a possibly null value.
            }
        }
    }
}