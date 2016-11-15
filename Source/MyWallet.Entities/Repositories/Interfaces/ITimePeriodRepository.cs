using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Entities.DataAccessModels;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface ITimePeriodRepository
    {
        /// <summary>
        /// Adds single time period
        /// </summary>
        /// <param name="period">New period</param>
        /// <returns>Added time period</returns>
        Task<TimePeriod> AddTimePeriod(TimePeriod period);

        /// <summary>
        /// Returns single time period
        /// </summary>
        /// <param name="id">Guid of time period</param>
        /// <returns>Time period by id</returns>
        Task<TimePeriod> GetSingleTimePeriod(Guid id);

        /// <summary>
        /// Returns all time periods
        /// </summary>
        /// <returns>All time periods</returns>
        Task<TimePeriod[]> GetAllTimePeriods();
    }
}