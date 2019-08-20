using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public abstract class BasePartnerScenario //: IPartnerScenario
    {
        /// <summary>
        /// Gets the scenario context.
        /// </summary>
        public ScenarioContext Context { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BasePartnerScenario"/> class.
        /// </summary>
        /// <param name="title">The scenario title.</param>
        /// <param name="context">The scenario context.</param>
        /// <param name="executionStrategy">The scenario execution strategy.</param>
        /// <param name="childScenarios">The child scenarios attached to the current scenario.</param>
        public BasePartnerScenario(string title, ScenarioContext context)
        {
            //if (string.IsNullOrWhiteSpace(title))
            //{
            //    throw new ArgumentException("title has to be set");
            //}

            //if (context == null)
            //{
            //    throw new ArgumentNullException("context");
            //}

            //this.Title = title;
            this.Context = context;

            //this.ExecutionStrategy = executionStrategy ?? new PromptExecutionStrategy();
            //this.Children = childScenarios;
        }

    }
}