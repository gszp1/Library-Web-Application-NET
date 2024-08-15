using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IPublisherService : IGenericService<Publisher>
    {
        Task CreatePublisher(PublisherDto dto);

        Task<List<AdminPublisherDto>> GetAllPublishers();

        Task UpdatePublisher(AdminPublisherDto dto);
    }

}
