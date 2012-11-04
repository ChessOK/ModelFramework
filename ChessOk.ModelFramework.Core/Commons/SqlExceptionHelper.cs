using System;
using System.Data.SqlClient;

namespace ChessOk.ModelFramework.Commands.Filters
{
    /// <summary>
    /// Предоставляет единственный метод <see cref="IsDeadlock"/>.
    /// </summary>
    public class SqlExceptionHelper
    {
        /// <summary>
        /// Возвращает true, если указанное исключение является
        /// <see cref="SqlException"/> с номером ошибки 1205.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
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