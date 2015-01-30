namespace Net_4._0_Extentions
{
    #region Using

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

    #endregion

    public static class GenericExecutors
    {
        /// <summary>
        ///     Runs the action in different thread and kills it after due time
        /// </summary>
        /// <param name="action"></param>
        /// <param name="terminateAfter">in seconds</param>
        public static bool ExecSafeInThread(Action action, int terminateAfter)
        {
            try
            {
                Thread shortTaskThread = new Thread(() => SafeExecCommand(action));
                shortTaskThread.Start();
                shortTaskThread.Join(TimeSpan.FromSeconds(terminateAfter));
                if (shortTaskThread.IsAlive)
                {
                    shortTaskThread.Abort();
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

            return true;
        }

        public static void SafeExecCommand(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public static void InvokeAndWaitIfRequired(this ISynchronizeInvoke control, MethodInvoker action)
        {
            InvokeIfRequired(control, action, true);
        }

        public static void InvokeNoWaitIfRequired(this ISynchronizeInvoke control, MethodInvoker action)
        {
            InvokeIfRequired(control, action, false);
        }

        private static void InvokeIfRequired(this ISynchronizeInvoke control, MethodInvoker action, bool sync)
        {
            if (control.InvokeRequired)
            {
                if (sync)
                {
                    control.Invoke(action, null);
                }
                else
                {
                    control.BeginInvoke(action, null);
                }
            }
            else
            {
                action();
            }
        }

        public static void CatchLogThrow(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}