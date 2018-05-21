using System;
using System.Threading.Tasks;

namespace Xunit.Gherkin.Quick.Domain
{
    internal class Step : BaseStep
    {
        protected Step(string keyWord, string test, System.Reflection.MethodInfo method, BaseStepDefinitionAttribute baseStepDefinitionAttribute)
            : base(keyWord, test, method, baseStepDefinitionAttribute)
        {
        }

        internal override Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
