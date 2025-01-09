using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateEase.Database;

namespace EstateEase.Database
{
    /// <summary>
    /// Class <c>DatabaseInitializer</c> initializes a database. There are two public functions, <c>CreateDatabaseFile</c>, which creates a database file, and <c>InitializeDatabase</c>, which initializes a database.
    /// </summary>
    public class DatabaseInitializer(DatabaseConnector databaseConnector)
    {
        private readonly DatabaseConnector _databaseConnector = databaseConnector;

        /// <summary>
        /// Method <c>CreateDatabaseFile</c> creates an SQLite3 database file in the application. The databasePath should be a valid path. 
        /// </summary>
        public static void CreateDatabaseFile(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
            }
        }

        /// <summary>
        /// Method <c>InitializeDatabase</c> initializes an SQLite database. Assumes the database has been initialized, otherwise, a connection will not be established and the database will not be initialized. 
        /// </summary>
        public void InitializeDatabase()
        {
            using var connection = _databaseConnector.GetConnection();
            CreateTables(connection);
            AddTriggers(connection);
        }

        /// <summary>
        /// Method <c>CreateTables</c> creates the necessary tables for the application: <c>Users</c>, <c>PropertyOwners</c>, <c>Tenants</c>, <c>Properties</c>, <c>Transcations</c>, <c>MaintenanceRequests</c>. Assumes the database has been initialized and a connection has been established.
        /// </summary>
        private static void CreateTables(SQLiteConnection connection)
        {
            string createUsersTable = @"
                CREATE TABLE IF NOT EXISTS Users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    password_hash TEXT NOT NULL
            );";

            string createPropertyOwnersTable = @"
                CREATE TABLE IF NOT EXISTS PropertyOwners (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    country_code TEXT NOT NULL,
                    phone_number TEXT NOT NULL
            );";

            string createTenantsTable = @"
                CREATE TABLE IF NOT EXISTS Tenants (
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
                    FOREIGN KEY(property_id) REFERENCES Properties(id) ON DELETE SET NULL
            );";

            string createPropertiesTable = @"
                CREATE TABLE IF NOT EXISTS Properties (
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
                    FOREIGN KEY(owner_id) REFERENCES PropertyOwners(id) ON DELETE RESTRICT
            );";

            string createTransactionsTable = @"
                CREATE TABLE IF NOT EXISTS Transactions (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    type TEXT NOT NULL,
                    category TEXT NOT NULL,
                    date TEXT NOT NULL,
                    amount REAL NOT NULL,
                    description TEXT NOT NULL,
                    property_id INTEGER,
                    FOREIGN KEY(property_id) REFERENCES Properties(id) ON DELETE SET NULL
            );";

            string createMaintenanceRequestsTable = @"
                CREATE TABLE IF NOT EXISTS MaintenanceRequests (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    date_requested TEXT NOT NULL,
                    date_completed TEXT NOT NULL,
                    description TEXT NOT NULL,
                    status INTEGER NOT NULL,
                    cost REAL NOT NULL,
                    vendor TEXT NOT NULL,
                    property_id INTEGER,
                    FOREIGN KEY(property_id) REFERENCES Properties(id) ON DELETE SET NULL
            );";

            using (var command = new SQLiteCommand(createUsersTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createPropertyOwnersTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createTenantsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createPropertiesTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createTransactionsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createMaintenanceRequestsTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method <c>AddTriggers</c> adds SQL triggers to the application. Assumes the database has been initialized and a connection has been established.
        /// </summary>
        private static void AddTriggers(SQLiteConnection connection)
        {
            string createEnforceOneActiveTenantTrigger = @"
                CREATE TRIGGER IF NOT EXISTS enforce_one_active_tenant
                BEFORE INSERT OR UPDATE ON Tenants
                FOR EACH ROW
                WHEN NEW.status = 1 -- '1' represents 'active' in the status column
                BEGIN
                    SELECT RAISE(ABORT, 'Only one active tenant allowed per property')
                    WHERE EXISTS (
                        SELECT 1
                        FROM Tenants
                        WHERE property_id = NEW.property_id
                          AND status = 1 -- Active tenant
                          AND id != NEW.id
                    );
                END;";

            using var command = new SQLiteCommand(createEnforceOneActiveTenantTrigger, connection);
            command.ExecuteNonQuery();
        }
    }
}
