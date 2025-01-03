using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The code in this file is based on a video from Code Monkey on YouTube (https://www.youtube.com/watch?v=RqgsGaMPZTw)
public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);


    //
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    //
    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    //const variables to allow the transparent window to be click-through
    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    //const to keep window on top
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private void Start()
    {
        #if !UNITY_EDITOR       //running this in the Unity Editor breaks the whole thing, so don't run unless it's a proper build
            IntPtr hWnd = GetActiveWindow();

            //negative margins value makes the background transparent
            MARGINS margins = new MARGINS { cxLeftWidth = -1 };
            DwmExtendFrameIntoClientArea(hWnd, ref margins);

            //make the window click-through so the main desktop can still be used
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

            //pin the window to the top layer
            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
        #endif
    }

    void Update()
    {
        //simple quit-out to allow for early build testing
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
