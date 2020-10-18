using Library.Entities.Models.UsersProjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Contracts.UserProjectContracts
{
    public interface IUserProjectRepository : IRepositoryBase<UserProject>
    {
        Task<IEnumerable<UserProject>> GetAllProjectsForUser(string id);

        Task<IEnumerable<UserProject>> GetUserProject(Guid userProjectId);

        void CreateUserProject(UserProject userProject);

        void DeleteUserProject(UserProject userProject);

        void UpdateUserProject(UserProject userProject);

        void RemoveProjectAndUser(IEnumerable<UserProject> userProjects);
    }
}