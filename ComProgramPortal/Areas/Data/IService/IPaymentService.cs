using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProgramPortal.Areas.Data.IService
{
    interface IPaymentService
    {
        Task Create(Payment model);
        Task<Payment> Get(int? id);

        Task Edit(Payment models);
        Task Delete(int? id);
        Task<List<Payment>> PaymentList(string searchString, string currentFilter, int? page);

        Task<List<StudentInfo>> Students(int? id);
        Task<int> StudentsCount(int? id);

        Task<Payment> PaymentDetails(int? id);


    }
}
