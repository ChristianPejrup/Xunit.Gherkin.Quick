using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Xunit.Gherkin.Quick.Domain
{
    internal class Scenario
    {
        private BaseStep previousStep;
        internal List<BaseStep> Steps { get; set; }

        internal void Execute()
        {
            foreach (var step in Steps)
            {
                ExecuteStep(step);
            }
        }

        internal void ReportStatus(ITestOutputHelper output)
        {
            foreach (var step in Steps)
            {
                step.ReportStatus(output);
            }
        }

        //TODO: move logic to BaseStep
        private void ExecuteStep(BaseStep step)
        {
            if(previousStep != null && previousStep.Status != StepStatus.Passed)
            {
                step.Status = StepStatus.Skipped;
                return;
            }

            try
            {
                step.Execute();
                step.Status = StepStatus.Passed;
            }
            catch(Exception exception)
            {
                //TODO: error handling
                step.Status = StepStatus.Failed;
            }

            previousStep = step;
        }
    }
}
