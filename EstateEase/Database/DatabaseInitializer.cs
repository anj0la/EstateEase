using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Database;

namespace EstateEase.Database
{
    public class DatabaseInitializer(DatabaseConnector databaseConnector)
    {
        private readonly DatabaseConnector _databaseConnector = databaseConnector;

        public void InitializeDatabase()
        {
            using(var connection = _databaseConnector.GetConnection())
            {
                CreateTables(connection);
                AddTriggers(connection);
            }
        }

        private void CreateTables(SQLiteConnection connection)
        {
        
            string createUserTable = @"
                CREATE TABLE IF NOT EXISTS User (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    password_hash TEXT NOT NULL
            );";

            string createPropertyOwnerTable = @"
                CREATE TABLE IF NOT EXISTS PropertyOwner (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    country_code TEXT NOT NULL,
                    phone_number TEXT NOT NULL
            );";

            string createTenantTable = @"
                CREATE TABLE IF NOT EXISTS Tenant (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    country_code TEXT NOT NULL,
                    phone_number TEXT NOT NULL,
                    lease_start TEXT NOT NULL,
                    lease_end TEXT NOT NULL,
                    status INTEGER NOT NULL,
                    rating TEXT NOT NULL,
                    property_id INTEGER,
                    FOREIGN KEY(property_id) REFERENCES Property(id) ON DELETE SET NULL
            );";

            string createPropertyTable = @"
                CREATE TABLE IF NOT EXISTS Property (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    address TEXT NOT NULL,
                    address_number TEXT NOT NULL,
                    locality TEXT NOT NULL,
                    administrative_area TEXT NOT NULL,
                    country TEXT NOT NULL,
                    postal_code TEXT NOT NULL,
                    date_added TEXT NOT NULL,
                    date_listed TEXT NOT NULL,
                    rent REAL NOT NULL,
                    property_status INTEGER NOT NULL,
                    commission_rate REAL NOT NULL,
                    owner_id INTEGER NOT NULL,
                    FOREIGN KEY(owner_id) REFERENCES PropertyOwner(id) ON DELETE RESTRICT
            );";

            string createTransactionTable = @"
                CREATE TABLE IF NOT EXISTS Transaction (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    type TEXT NOT NULL,
                    category TEXT NOT NULL,
                    date TEXT NOT NULL,
                    amount REAL NOT NULL,
                    description TEXT NOT NULL,
                    property_id INTEGER,
                    FOREIGN KEY(property_id) REFERENCES Property(id) ON DELETE SET NULL
        );";

             string createMaintenanceTable = @"
                 CREATE TABLE IF NOT EXISTS Maintenance (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    date_requested TEXT NOT NULL,
                    date_completed TEXT NOT NULL,
                    description TEXT NOT NULL,
                    status INTEGER NOT NULL,
                    cost REAL NOT NULL,
                    vendor TEXT NOT NULL,
                    property_id INTEGER,
                    FOREIGN KEY(property_id) REFERENCES Property(id) ON DELETE SET NULL
        );";


            using (var command = new SQLiteCommand(createUserTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createPropertyOwnerTable, connection))
            {
                command.ExecuteNonQuery();

            }
            using (var command = new SQLiteCommand(createTenantTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createPropertyTable, connection))
            {
                command.ExecuteNonQuery();

            }
            using (var command = new SQLiteCommand(createTransactionTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createMaintenanceTable, connection))
            {
                command.ExecuteNonQuery();

            }
        }

        private void AddTriggers(SQLiteConnection connection)
        {
            string createEnforceOneActiveTenantTrigger = @"
                CREATE TRIGGER IF NOT EXISTS enforce_one_active_tenant
                BEFORE INSERT OR UPDATE ON Tenant
                FOR EACH ROW
                WHEN NEW.status = 1 -- '1' represents 'active' in the status column
                BEGIN
                    SELECT RAISE(ABORT, 'Only one active tenant allowed per property')
                    WHERE EXISTS (
                        SELECT 1
                        FROM Tenant
                        WHERE property_id = NEW.property_id
                          AND status = 1 -- Active tenant
                          AND id != NEW.id
                    );
                END;";

            using (var command = new SQLiteCommand(createEnforceOneActiveTenantTrigger, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

   
}