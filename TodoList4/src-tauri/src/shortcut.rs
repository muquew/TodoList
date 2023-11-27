use tauri::AppHandle;
use crate::system_tray;

#[tauri::command]
pub(crate) async fn open_change_window(app_handle: AppHandle) {
    system_tray::create_change_window(&app_handle);
}