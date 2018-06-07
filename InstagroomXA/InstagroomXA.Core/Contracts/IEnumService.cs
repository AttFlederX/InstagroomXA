using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for platform-specific services which provide enum values for view states, text style, etc.
    /// </summary>
    public interface IEnumService
    {
        int ViewStateVisible { get; }
        int ViewStateInvisible { get; }
        int ViewStateGone { get; }

        int TypefaceStyleNormal { get; }
        int TypefaceStyleBold { get; }
        int TypefaceStyleItalic { get; }
        int TypefaceStyleBoldItalic { get; }
    }
}
