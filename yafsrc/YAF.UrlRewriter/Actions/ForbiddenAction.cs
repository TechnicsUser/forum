// UrlRewriter - A .NET URL Rewriter module
// Version 2.0
//
// Copyright 2011 Intelligencia
// Copyright 2011 Seth Yates
// 

namespace YAF.UrlRewriter.Actions
{
    using System.Net;

    /// <summary>
    /// Returns a 403 Forbidden HTTP status code.
    /// </summary>
    public sealed class ForbiddenAction : SetStatusAction
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ForbiddenAction()
            : base(HttpStatusCode.Forbidden)
        {
        }
    }
}
