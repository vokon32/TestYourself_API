using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestYourself_API.Models;

namespace TestYourself_API.Repository
{
    public interface ITestRepository
    {
        Task<ICollection<Test>> GetTestsAsync();
        Task<Test> GetTestAsync(int id);
        bool TestExist(int id);

    }
}
