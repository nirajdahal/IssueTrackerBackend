using Library.Contracts.ProjectManagerContracts;
using Library.Entities;
using Library.Entities.Models.UsersProjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class ProjectManagerRepository : RepositoryBase<ProjectManager>, IProjectManagerRepository
    {
        public ProjectManagerRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateProjectManager(ProjectManager ProjectManager)
        {
            Create(ProjectManager);
        }

        public void DeleteProjectManager(ProjectManager ProjectManager)
        {
            Delete(ProjectManager);
        }

        public async Task<IEnumerable<ProjectManager>> GetAllProjectsForManager(string id)
        {
            var ProjectsTypes = await FindByCondition(x => x.Id.Equals(id)).ToListAsync();
            return ProjectsTypes;
        }

        public async Task<IEnumerable<ProjectManager>> GetProjectManager(Guid ProjectId)
        {
            var ProjectType = await FindByCondition(x => x.ProjectId.Equals(ProjectId))
                .Include(x => x.ApplicationUser)
                .ToListAsync();
            return ProjectType;
        }

        public async Task<IEnumerable<ProjectManager>> GetProjectManagers()
        {
            var ProjectType = await FindAll()
                .Include(x => x.ApplicationUser)
                .ToListAsync();
            return ProjectType;
        }

        public void UpdateProjectManager(ProjectManager ProjectManager)
        {
            Update(ProjectManager);
        }

        public void RemoveProjectAndManager(IEnumerable<ProjectManager> ProjectManagers)
        {
            _context.Set<ProjectManager>().RemoveRange(ProjectManagers);
        }
    }
}