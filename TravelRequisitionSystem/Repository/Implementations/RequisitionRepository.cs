using MicroOrm.Dapper.Repositories;
using TravelRequisitionSystem.Data;
using TravelRequisitionSystem.Repository.Interfaces;

namespace TravelRequisitionSystem.Repository.Implementations
{
    public class RequisitionRepository : GenericRepository<TravelRequest>, IRequisitionRepository
    {
        public RequisitionRepository(DapperRepository<TravelRequest> repo) : base(repo)
        {

        }
    }
}
