﻿using ScmBackup.Scm;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ScmBackup.Tests.Integration.Scm
{
    internal class FakeCommandLineScm : CommandLineScm, IScm
    {
        public FakeCommandLineScm()
        {
            string testAssemblyDir = DirectoryHelper.TestAssemblyDirectory();

            // some simple command with predictable result, to execute for testing
            // (probably different for each OS)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                this.FakeCommandName = Path.Combine(testAssemblyDir, @"Scm\FakeCommandLineScmTools\FakeCommandLineScm-Command-Windows.bat");
                this.FakeCommandArgs = "";
                this.FakeCommandResult = "Windows";

                this.FakeCommandNameNotExisting = Path.Combine(testAssemblyDir, "doesnt-exist");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new NotImplementedException();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// command to execute
        /// </summary>
        public string FakeCommandName { get; private set; }

        /// <summary>
        /// arguments (if necessary)
        /// </summary>
        public string FakeCommandArgs { get; private set; }

        /// <summary>
        /// the result should contain this string
        /// </summary>
        public string FakeCommandResult { get; private set; }

        /// <summary>
        /// "wrong" command which doesn't exist (to test error handling)
        /// </summary>
        public string FakeCommandNameNotExisting { get; private set; }

        public override string DisplayName
        {
            get { return "FakeDisplayName"; }
        }

        public override string ShortName
        {
            get { return "fake"; }
        }

        protected override string CommandName
        {
            get { return this.FakeCommandName; }
        }

        public string ExecuteCommandDirectly()
        {
            return this.ExecuteCommand(this.FakeCommandArgs);
        }

        protected override bool IsOnThisComputer()
        {
            string result = this.ExecuteCommand(this.FakeCommandArgs);
            return result.ToLower().Contains(this.FakeCommandResult.ToLower());
        }

        public override bool DirectoryIsRepository(string directory)
        {
            throw new NotImplementedException();
        }

        public override void CreateRepository(string directory)
        {
            throw new NotImplementedException();
        }
    }
}