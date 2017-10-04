using System.Collections.Generic;

namespace CheckRESTAPI.Services
{
    public interface IValuesService
    {
        IEnumerable<string> FindAll();

        string Find(int id);
    }
}