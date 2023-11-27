// // 在 Cargo.toml 中添加依赖
// // windows = { version = "0.43.0", features = [
// //     "Win32_Foundation",
// //     "Win32_UI_WindowsAndMessaging",
// // ] }
use tauri::{App, AppHandle, Manager};


pub(crate) fn set_ignore_cursor_events_app(app: &App) {
    set_window_styles(&app.get_window("main").unwrap());
}

pub(crate) fn set_ignore_cursor_events_apphandle(app: &AppHandle) {
    set_window_styles(&app.get_window("main").unwrap());
}

#[cfg(windows)]
fn set_window_styles(window: &tauri::Window) {
    use windows::Win32::Foundation::HWND;
    use windows::Win32::UI::WindowsAndMessaging::{GetWindowLongW, SetWindowLongA, 
        WINDOW_EX_STYLE, GWL_EXSTYLE, WS_EX_COMPOSITED, WS_EX_LAYERED, WS_EX_TRANSPARENT };

    let hwnd = HWND(window.hwnd().unwrap().0);
    unsafe {
        // Get the current window styles
        let mut style_ex = WINDOW_EX_STYLE(GetWindowLongW(hwnd, GWL_EXSTYLE) as u32);
        // Add the desired extended styles
        style_ex |= 
              WS_EX_COMPOSITED      // 在窗口和任何子窗口上启用硬件加速的双缓冲
            | WS_EX_LAYERED         // 启用窗口的分层特性
            | WS_EX_TRANSPARENT;    // 是窗口成为透明，可以穿透鼠标事件

        // Set the extended styles
        let nindex = GWL_EXSTYLE;
        let _pre_val = SetWindowLongA(hwnd, nindex, style_ex.0 as i32);
    }
}