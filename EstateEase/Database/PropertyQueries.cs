using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Models;

namespace EstateEase.Database
{
    public class PropertyQueries(DatabaseConnector databaseConnection) : BaseQuery(databaseConnection)
    {

    }
}
