﻿
namespace TMRC_CSP.Logics
{
    using System.Security;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a secure mechanism for retrieving and store information. 
    /// </summary>
    public interface IVaultProvider
    {
        /// <summary>
        /// Gets the specified entity from the vault. 
        /// </summary>
        /// <param name="identifier">Identifier of the entity to be retrieved.</param>
        /// <returns>The value retrieved from the vault.</returns>
        Task<SecureString> GetAsync(string identifier);

        /// <summary>
        /// Stores the specified value in the vault.
        /// </summary>
        /// <param name="identifier">Identifier of the entity to be stored.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task StoreAsync(string identifier, SecureString value);
    }
}