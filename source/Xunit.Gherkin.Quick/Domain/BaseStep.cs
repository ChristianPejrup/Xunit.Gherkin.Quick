using System;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Xunit.Gherkin.Quick.Domain
{
    internal abstract class BaseStep
    {
        internal StepStatus Status { get; set; }
        protected string Keyword { get; }
        protected string Text { get; }
        protected StepMethod Method { get; }
        protected Exception Exception { get; set; }

        protected BaseStep(string keyWord, string text, StepMethod stepMethod)
        {
            Status = StepStatus.ReadyToRun;

            Keyword = keyWord;
            Text = text;
            Method = stepMethod;
        }

        internal abstract Task Execute();

        internal void ReportStatus(ITestOutputHelper output)
        {
            output.WriteLine($"{Keyword} {Text}: {Status}");

            if(Exception != null)
            {
                output.WriteLine(Exception.ToString());
            }
        }
    }
}
