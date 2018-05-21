using System;
using System.Reflection;

namespace Xunit.Gherkin.Quick.Domain
{
    public class StepMethod
    {
        public MethodInfo Method { get; set; }
        public BaseStepDefinitionAttribute Definition  { get; set; }

        public StepMethod(MethodInfo method)
        {
            Method = method;
            Definition = method.GetCustomAttribute<BaseStepDefinitionAttribute>();
        }

        public bool Match(string keyword, string text)
        {
            return Definition.MatchesStep(keyword, text);
        }

        public bool Match(string text)
        {
            var matchResult = Definition.MatchRegex(text);

            return String.Equals(matchResult.Value, text.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
