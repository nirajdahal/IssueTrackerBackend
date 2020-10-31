using Library.Contracts;
using Library.Entities;
using Library.Entities.Models.Projects;
using Library.Entities.Models.UsersProjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext _context) : base(_context)
        {
        }

        public void CreateProject(Project project)
        {
            Create(project);
        }

        public void DeleteProject(Project project)
        {
            Delete(project);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            var projects = await FindAll().OrderBy(p => p.Title)
                .Include(x => x.Ticket)
                .Include(x => x.ProjectManagers)
                .Include(x => x.UsersProjects)
                .ToListAsync();
            return projects;
        }

        public async Task<Project> GetProject(Guid projectId)
        {
            var projects = await FindByCondition(p => p.Id.Equals(projectId))
                .Include(p => p.Ticket)
                .Include(x => x.UsersProjects)
                .Include(x => x.ProjectManagers)
                .SingleOrDefaultAsync();
            return projects;
        }

        public void UpdateProject(Project project)
        {
            Update(project);
        }
    }
}