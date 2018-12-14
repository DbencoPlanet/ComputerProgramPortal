using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProgramPortal.Areas.Data.IService
{
    interface IPackageService
    {
        Task Create(Package model);
        Task<Package> Get(int? id);

        Task Edit(Package models);
        Task Delete(int? id);
        Task<List<Package>> PackageList(string searchString, string currentFilter, int? page);

        Task<List<StudentInfo>> Students(int? id);
        Task<int> StudentsCount(int? id);

        Task<Package> PackageDetails(int? id);


    }
}
