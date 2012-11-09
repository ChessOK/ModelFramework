using System;
using System.Collections.Generic;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Предоставляет общее для экземпляра <see cref="ModelContext"/>
    /// хранилище ключ-значение, которое можно использовать
    /// в качестве локального кэша.
    /// </summary>
    [Serializable]
    public class ModelContextCache : Dictionary<string, object>
    {
    }
}
