using Library.Contracts.UserProjectContracts;
using Library.Entities;
using Library.Entities.Models.UsersProjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class UserProjectRepository : RepositoryBase<UserProject>, IUserProjectRepository
    {
        public UserProjectRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateUserProject(UserProject userProject)
        {
            Create(userProject);
        }

        public void DeleteUserProject(UserProject userProject)
        {
            Delete(userProject);
        }

        public async Task<IEnumerable<UserProject>> GetAllProjectsForUser(string id)
        {
            var ProjectsTypes = await FindByCondition(x => x.Id.Equals(id))
                .ToListAsync();
            return ProjectsTypes;
        }

        public async Task<IEnumerable<UserProject>> GetUserProject(Guid ProjectId)
        {
            var ProjectType = await FindByCondition(x => x.ProjectId.Equals(ProjectId))
                .Include(x => x.ApplicationUser)
                .ToListAsync();
            return ProjectType;
        }

        public void UpdateUserProject(UserProject userProject)
        {
            Update(userProject);
        }

        public void RemoveProjectAndUser(IEnumerable<UserProject> userProjects)
        {
            _context.Set<UserProject>().RemoveRange(userProjects);
        }
    }
}