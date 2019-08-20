using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    /// <summary>
    /// Represents a partner scenario that demos one or more related partner center APIs.
    /// </summary>
    public interface IPartnerScenario
    {
        /// <summary>
        /// Gets the scenario title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the children scenarios of the current scenario.
        /// </summary>
        IReadOnlyList<IPartnerScenario> Children { get; }

        /// <summary>
        /// Gets the scenario context.
        /// </summary>
        IScenarioContext Context { get; }

        /// <summary>
        /// Runs the scenario.
        /// </summary>
        void Run();
    }
}
