// Prevents additional console window on Windows in release, DO NOT REMOVE!!
#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

use tauri::{Manager, AppHandle};

mod system_tray;
mod setup;
mod mouse_penetration;
mod get_lang;
mod file_ini;
mod shortcut;
mod file_json;

#[tauri::command]
fn change_windows_language(app_handle: AppHandle) {
    if let Some(window) = app_handle.get_window("settings") {
        let _ = window.set_title(&get_lang::get_json_value("windowsTitle.settings"));
    }

    if let Some(window) = app_handle.get_window("change") {
        let _ = window.set_title(&get_lang::get_json_value("windowsTitle.change"));
    }

    if let Some(window) = app_handle.get_window("main") {
        let _ = window.set_title(&get_lang::get_json_value("windowsTitle.main"));
    }

    let _ = app_handle.tray_handle().set_menu(system_tray::tray_menu_items());
}



fn main() {
    tauri::Builder::default()
        .setup(| app | {
            setup::setup(app);
            mouse_penetration::set_ignore_cursor_events_app(app);
            Ok(())
        })
        // .on_window_event(|event| {
        //     match event.event() {
        //         tauri::WindowEvent::CloseRequested { api, .. } => {
        //             api.prevent_close();
        //             println!("66666")
        //         }
        //         _ => {}
        //     }
        // })
        .system_tray(system_tray::tray())
        .on_system_tray_event(|app, event| system_tray::handle_tray_event(app, event))
        .invoke_handler(
            tauri::generate_handler![
                file_ini::change_config_user,
                file_ini::read_from_config_user,
                change_windows_language,
                shortcut::open_change_window,
                file_json::set_user_items,
                file_json::get_user_items,
                file_json::sort_items_by_time
            ]
        )
        .run(tauri::generate_context!())
        .expect("error while running tauri application");

}
