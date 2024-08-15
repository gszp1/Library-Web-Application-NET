using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IPublisherService : IGenericService<Publisher>
    {
        Task CreatePublisherAsync(PublisherDto dto);

        Task<List<AdminPublisherDto>> GetAllPublishersAsync();

        Task UpdatePublisherAsync(AdminPublisherDto dto);
    }

}
