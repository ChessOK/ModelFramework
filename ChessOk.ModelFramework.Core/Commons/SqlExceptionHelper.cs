using System;
using System.Data.SqlClient;

namespace ChessOk.ModelFramework.Commands.Filters
{
    public class SqlExceptionHelper
    {
        public static bool IsDeadlock(Exception ex)
        {
            if (EmulateDeadlock) return true;

            var sqlException = ex as SqlException;
            if (sqlException == null)
            {
                return false;
            }

            return sqlException.Number == 1205;
        }

        internal static bool EmulateDeadlock { get; set; }
    }
}