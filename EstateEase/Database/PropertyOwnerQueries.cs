using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Models;

namespace EstateEase.Database
{
    public class PropertyOwnerQueries(DatabaseConnector databaseConnection) : BaseQuery(databaseConnection)
    {
        public PropertyOwner? GetPropertyOwnerFromDatabase()
        {

        }
    }
}
