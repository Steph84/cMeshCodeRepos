﻿using System;

namespace jamGitHubGameOff
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var menu = new Main())
                menu.Run();
        }
    }
#endif
}
