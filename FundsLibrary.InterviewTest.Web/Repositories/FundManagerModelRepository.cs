using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;
using System.Web;
using System.Configuration;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IFundManagerModelRepository
    {
        Task<IEnumerable<FundManagerModel>> GetAll();
        Task<FundManagerModel> Get(Guid id);
        Task Edit(FundManagerModel model);
        Task<Guid> Create(FundManagerModel model);
    }

    public class FundManagerModelRepository : IFundManagerModelRepository
    {
        /// <summary>
        /// Service URL moved to web.config file
        /// </summary>
        private string _serviceAppUrl = ConfigurationManager.AppSettings["serviceUrl"];

        private readonly IHttpClientWrapper _client;
        private readonly IMapper<FundManager, FundManagerModel> _toModelMapper;
        private IMapper<FundManagerModel, FundManager> _fromModelMapper;

        public FundManagerModelRepository(
            IHttpClientWrapper client = null,
            IMapper<FundManager, FundManagerModel> toModelMapper = null)
        {
            _client = client ?? new HttpClientWrapper(_serviceAppUrl);
            _toModelMapper = toModelMapper ?? new ToFundManagerModelMapper();
            _fromModelMapper = _fromModelMapper ?? new FromFundManagerModelMapper();
        }

        /// <summary>
        /// Gets all Fund Managers on the platform
        /// </summary>
        /// <returns>IEnumerable of Fund Managers on the platform</returns>
        public async Task<IEnumerable<FundManagerModel>> GetAll()
        {
            var managers = await _client.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/");

            if (managers == null)
            {
                return null;
            }
            else
            {
                return managers.Select(s => _toModelMapper.Map(s));
            }
            
        }

        /// <summary>
        /// Retrieves a FundManager by their ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single Fund Manager</returns>
        public async Task<FundManagerModel> Get(Guid id)
        {
            var manager = await _client.GetAndReadFromContentGetAsync<FundManager>("api/FundManager/" + id);

            if (manager == null)
            {
                return null;
            }
            else
            {
                return _toModelMapper.Map(manager);
            }
        }

        /// <summary>
        /// Creates a fund manager using submitted form data
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The newly created Fund Manager's ID</returns>
        public async Task<Guid> Create(FundManagerModel model)
        {
            var fundManager = _fromModelMapper.Map(model);

            return await _client.CreateManagerAsync<Guid>("api/FundManager/", fundManager);
        }

        /// <summary>
        /// Edits the details of a fund manager using submitted form data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Edit(FundManagerModel model)
        {
            var fundManager = _fromModelMapper.Map(model);

            await _client.EditManagerAsync("api/FundManager/" + fundManager.Id, fundManager);
        }
    }
}
