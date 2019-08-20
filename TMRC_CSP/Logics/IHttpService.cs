using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMRC_CSP.Logics
{
    public interface IHttpService
    {
        /// <summary>
        /// Sends a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="requestUri">The Uri where the request should be sent.</param>
        /// <param name="token">The access token value used to authorize the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<T> GetAsync<T>(Uri requestUri, string token);
    }
}
