using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Repositories;

namespace FundsLibrary.InterviewTest.Service.Controllers
{
    public class FundManagerController : ApiController
    {
        private readonly IFundManagerRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerMemoryDb())
        {}

        public FundManagerController(IFundManagerRepository injectedRepository)
        {
            _repository = injectedRepository;
        }

        public async Task<IEnumerable<FundManager>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/FundManager/79c74c79-f993-454e-a7d4-53791f17f179
        public async Task<FundManager> Get(Guid id)
        {
            return await _repository.GetBy(id);
        }

        public async Task<Guid> Post(FundManager fundManager)
        {
            return await _repository.Create(fundManager);
        }

        public void Put(FundManager fundManager)
        {
            _repository.Update(fundManager.Id, fundManager);
        }
    }
}
