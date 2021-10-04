using Fondital.Shared;
using Fondital.Shared.Dto;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRolesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserRole>> GetAll()
		{
            return await _unitOfWork.UserRoles.GetAllAsync();
		}
       
    }
}