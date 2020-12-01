using System.Data;

namespace TestMVC_HoteLandLyst.Interfaces
{
    public interface IDataAccess
    {
        DataTable ExecuteSP(string query);
        DataTable ExecuteSPParam(string query, int paramId);
    }
}