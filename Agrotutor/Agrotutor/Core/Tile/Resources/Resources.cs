// <copyright file="Resources.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.IO;
using System.Reflection;

namespace Agrotutor.Core.Tile.Resources
{
    public static class Resources
    {
        public static Stream GetIIASATiles()
        {
            return Assembly.GetCallingAssembly().GetManifestResourceStream($"{GetPath()}.mexico-simple.mbtiles");
        }

        public static string GetPath()
        {
            return typeof(Resources).Namespace;
        }
    }
}