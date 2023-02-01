using TestYourself_API.Dto;

namespace TestYourself_API.Repository
{
    public interface IHomeRepository
    {
        Task<HomeDto> GetHomeDto();
    }
}
