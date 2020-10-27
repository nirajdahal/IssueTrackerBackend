using Library.Entities.Models.UsersProjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Contracts.ProjectManagerContracts
{
    public interface IProjectManagerRepository : IRepositoryBase<ProjectManager>
    {
        Task<IEnumerable<ProjectManager>> GetAllProjectsForManager(string id);

        Task<IEnumerable<ProjectManager>> GetProjectManager(Guid ProjectId);

        void CreateProjectManager(ProjectManager ProjectManager);

        void DeleteProjectManager(ProjectManager ProjectManager);

        void UpdateProjectManager(ProjectManager ProjectManager);

        void RemoveProjectAndManager(IEnumerable<ProjectManager> ProjectManagers);

        Task<IEnumerable<ProjectManager>> GetProjectManagers();
    }
}