using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public class FromFundManagerModelMapper : IMapper<FundManagerModel, FundManager>
    {
        public FundManager Map(FundManagerModel model)
        {
            return new FundManager
            {
                Id = model.Id,
                Biography = model.Biography,
                Location = model.Location,
                ManagedSince = model.ManagedSince,
                Name = model.Name
            };
        }
    }
}
