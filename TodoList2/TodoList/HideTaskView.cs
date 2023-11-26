using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace TodoList
{
    public static class  HideTaskView
    {
        // 隐藏任务视图
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public static void HideTaskViewPreview(this WindowInteropHelper windowInteropHelper)
        {
            IntPtr hWnd = windowInteropHelper.Handle;
            int extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            _ = SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle | WS_EX_TOOLWINDOW);
        }
    }
}
