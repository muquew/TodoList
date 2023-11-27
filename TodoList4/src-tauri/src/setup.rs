use tauri::{App, Manager};
use crate::get_lang;

#[cfg(windows)]
pub(crate) fn setup(app: &App) {
    let window = &app.get_window("main").unwrap();
    let _ = window.set_title(&get_lang::get_json_value("windowsTitle.main"));
}