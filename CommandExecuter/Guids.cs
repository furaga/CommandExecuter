// Guids.cs
// MUST match guids.h
using System;

namespace Company.CommandExecuter
{
    static class GuidList
    {
        public const string guidCommandExecuterPkgString = "67fcf4d3-2f05-4b2f-a855-352f94fd9790";
        public const string guidCommandExecuterCmdSetString = "bb8b5ea6-1718-4d63-b3fa-f3e956038ca9";
        public const string guidToolWindowPersistanceString = "908bc66b-2e72-4e32-9049-97fb71ba504e";

        public static readonly Guid guidCommandExecuterCmdSet = new Guid(guidCommandExecuterCmdSetString);
    };
}