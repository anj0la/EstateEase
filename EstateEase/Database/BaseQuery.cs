using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Database;

namespace EstateEase.Database
{
    public abstract class BaseQuery(DatabaseConnector databaseConnection)
    {
        protected readonly DatabaseConnector _databaseConnection = databaseConnection;

    }
}
