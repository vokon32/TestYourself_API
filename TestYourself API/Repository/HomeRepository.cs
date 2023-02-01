using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestYourself_API.Dto;
using TestYourself_API.Helper;

namespace TestYourself_API.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ITestRepository _testRepository;

        public HomeRepository(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<HomeDto> GetHomeDto()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeDto();
            try
            {
                string url = IPInfoURl.URL;
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.Country = ipInfo.Country;
                if (homeViewModel != null)
                {
                    homeViewModel.Tests = await _testRepository.GetTestByCountry(homeViewModel.Country);
                }
                else
                {
                    homeViewModel.Tests = null;
                }
            }
            catch
            {
                homeViewModel.Tests = null;
            }
            return homeViewModel;
        }
    }
}
