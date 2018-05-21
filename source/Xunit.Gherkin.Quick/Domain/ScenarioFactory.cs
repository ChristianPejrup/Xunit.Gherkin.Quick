using Gherkin.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Gherkin.Quick.Domain
{
    internal class ScenarioFactory
    {
        internal Scenario Get(string scenarioName, Type scenarioClass)
        {
            var scenarioDefinition = GetScenarioDefinition(scenarioName, scenarioClass);

            var steps = GetSteps(scenarioDefinition, scenarioClass);

            return new Scenario();
        }

        private ScenarioDefinition GetScenarioDefinition(string scenarioName, Type scenarioClass)
        {
            var gherkinDocument = ScenarioDiscoverer.GetGherkinDocumentByType(scenarioClass);

            var parsedScenario = gherkinDocument.Feature.Children.FirstOrDefault(scenario => scenario.Name == scenarioName);
            if (parsedScenario == null)
                throw new InvalidOperationException($"Cannot find scenario `{scenarioName}`.");

            return parsedScenario;
        }

        private List<BaseStep> GetSteps(ScenarioDefinition definition, Type scenarioClass)
        {
            var steps = new List<BaseStep>();

            var stepMethods = GetStepMethods(scenarioClass);

            foreach (var step in definition.Steps)
            {
                var matchingMethod = stepMethods.FirstOrDefault(m => m.Match(step.Keyword, step.Text));

                if(matchingMethod != null)
                    throw new InvalidOperationException($"Cannot find scenario step `{step.Keyword}{step.Text}` for scenario `{definition.Name}`.");

                if(!matchingMethod.Match(step.Text))
                    throw new InvalidOperationException($"Step method partially matched but not selected. Step `{step.Text.Trim()}`, Method pattern `{matchingMethod.Definition.Pattern}`.");
            }

            throw new NotImplementedException();
        }

        private List<StepMethod> GetStepMethods(Type scenarioClass)
        {
            return scenarioClass.GetTypeInfo().GetMethods()
                .Where(m => m.IsDefined(typeof(BaseStepDefinitionAttribute)))
                .Select(m => new StepMethod(m))
                .ToList();
        }
    }
}
