use tauri::{
  SystemTray, SystemTrayEvent, Manager, AppHandle, 
  CustomMenuItem, SystemTrayMenu, SystemTrayMenuItem, 
  WindowBuilder, WindowUrl
};

use crate::mouse_penetration;
use crate::get_lang;

pub(crate) fn tray_menu_items() -> SystemTrayMenu {
  let change = CustomMenuItem::new("change".to_string(), get_lang::get_json_value("trayMenu.changeWindow"));
  let quit = CustomMenuItem::new("quit".to_string(), get_lang::get_json_value("trayMenu.quit"));
  let hide = CustomMenuItem::new("hide".to_string(), get_lang::get_json_value("trayMenu.hide"));
  let show = CustomMenuItem::new("show".to_string(), get_lang::get_json_value("trayMenu.show"));
  let hide_decorations = CustomMenuItem::new("hide_decorations".to_string(), get_lang::get_json_value("trayMenu.hideDecorations"));
  let show_decorations = CustomMenuItem::new("show_decorations".to_string(), get_lang::get_json_value( "trayMenu.showDecorations"));
  let setup = CustomMenuItem::new("setup".to_string(), get_lang::get_json_value("trayMenu.settings"));
  SystemTrayMenu::new()
    .add_item(change)
    .add_native_item(SystemTrayMenuItem::Separator)
    .add_item(hide)
    .add_item(show)
    .add_native_item(SystemTrayMenuItem::Separator)
    .add_item(hide_decorations)
    .add_item(show_decorations)
    .add_native_item(SystemTrayMenuItem::Separator)
    .add_item(setup)
    .add_native_item(SystemTrayMenuItem::Separator)
    .add_item(quit)
}


pub(crate) fn tray() -> SystemTray {
  SystemTray::new().with_menu(tray_menu_items())
}

pub(crate) fn handle_tray_event(app: &AppHandle, event: SystemTrayEvent) {
    match event {
        // SystemTrayEvent::LeftClick { position: _, size: _, .. } => {
        //   println!("system tray received a left click");
        // }
        // SystemTrayEvent::RightClick {  position: _, size: _, .. } => {
        //   println!("system tray received a right click");
        // }
        SystemTrayEvent::DoubleClick { position: _, size: _, .. } => {
          println!("system tray received a double click");
        }
        SystemTrayEvent::MenuItemClick { id, .. } => {
          if let Err(err) = handle_menu_item_click(app, id.as_str()) {
              eprintln!("Error handling menu item click: {}", err);
          }
        }
        _ => {}
    }
}

fn toggle_window_visibility(app: &AppHandle, show: bool) {
  let window = app.get_window("main").unwrap();
  if show {
    window.show().unwrap();
    window.set_focus().unwrap();
    mouse_penetration::set_ignore_cursor_events_apphandle(app);
  } else {
    window.hide().unwrap();
  }
}

fn toggle_decorations_visibility(app: &AppHandle, show: bool) {
  let window = app.get_window("main").unwrap();
  window.set_decorations(show).unwrap();
  if !show {
    mouse_penetration::set_ignore_cursor_events_apphandle(app);
  } else {
    window.set_focus().unwrap();
  }
}

fn create_settings_window(app: &AppHandle) {
  let settings_window =WindowBuilder::new(app, "settings", WindowUrl::App("/src/page-htmls/settings.html".into()))
    .title(get_lang::get_json_value("windowsTitle.settings"))
    .center()
    .inner_size(450 as f64, 200 as f64)
    .resizable(false)
    .minimizable(false)
    .build();
  if let Err(error) = settings_window {
    match error {
        tauri::Error::WindowLabelAlreadyExists(_) => app.get_window("settings").unwrap().set_focus().unwrap(),
        _ => {}
    }
  }
}

pub(crate) fn create_change_window(app: &AppHandle) {
  let change_window =WindowBuilder::new(app, "change", WindowUrl::App("/src/page-htmls/change.html".into()))
    .title(get_lang::get_json_value("windowsTitle.change"))
    .center()
    .inner_size(1000 as f64, 500 as f64)
    .resizable(false)
    .minimizable(false)
    .build();
  if let Err(error) = change_window {
    match error {
        tauri::Error::WindowLabelAlreadyExists(_) => app.get_window("change").unwrap().set_focus().unwrap(),
        _ => {
          eprintln!("Error creating change window: {:?}", error);
        }
    }
  }
}

fn handle_menu_item_click(app: &AppHandle, id: &str) -> Result<(), tauri::Error> {
  match id {
    "quit" => std::process::exit(0),
    "hide" => toggle_window_visibility(app, false),
    "show" => toggle_window_visibility(app, true),
    "hide_decorations" => toggle_decorations_visibility(app, false),
    "show_decorations" => toggle_decorations_visibility(app, true),
    "setup" => create_settings_window(app),
    "change" => create_change_window(app),
    _ => {}
  }
  Ok(())
}

// #[tauri::command]
// fn change_tray_lang(app: &AppHandle) {
//   let _ = app.tray_handle().set_menu(tray_menu_());
// }