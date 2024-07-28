using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;

namespace Library_Web_Application_NET.Server.src.service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
    }
}
