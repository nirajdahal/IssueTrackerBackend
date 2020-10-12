using Library.Entities.Models.Projects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        Task<IEnumerable<Project>> GetAllProjects();

        Task<Project> GetProject(Guid projectId);

        void CreateProject(Project project);
        
        void DeleteProject(Project project);

        void UpdateProject(Project project);

        //Task<IEnumerable<Project>> GetProjectByIds(IEnumerable<Guid> ids);
    }
}
