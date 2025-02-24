// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace System.Formats.Tar
{
    internal static partial class TarHelpers
    {
        internal static SortedDictionary<string, UnixFileMode>? CreatePendingModesDictionary()
            => null;

        internal static void CreateDirectory(string fullPath, UnixFileMode? mode, bool overwriteMetadata, SortedDictionary<string, UnixFileMode>? pendingModes)
            => Directory.CreateDirectory(fullPath);

        internal static void SetPendingModes(SortedDictionary<string, UnixFileMode>? pendingModes)
            => Debug.Assert(pendingModes is null);
    }
}
