using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Interfaces
{
    public interface ISqlServerAccess : IDataAccess
    {
        SqlConnection GetSqlConnection();
        SqlCommand GetSqlCommand();
        DataTable GetDataTable();

        SqlDataAdapter GetAdapter();

        string Connectionstring { get; }
    }
}
