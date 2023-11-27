use ini::Ini;
use std::{env, path::PathBuf};

#[tauri::command]
pub(crate) fn change_config_user(updates: Vec<(Option<&str>, &str, &str)>) -> Result<(), String> {
    let (path, mut config) = get_config_user()?;
    for (section, key, value) in updates {
        if let Some(section) = section {
            config
                .with_section(Some(section))
                .set(key, value);
        }
    }
    write_to_config_user(&path, &config)?;
    Ok(())
}

fn write_to_config_user(ini_path: &PathBuf, config: &Ini) -> Result<(), String> {
    config
        .write_to_file(ini_path)
        .map_err(|e| format!("Failed to write to INI file: {}", e))
}

#[tauri::command]
pub(crate) fn read_from_config_user(section: &str, key: &str) -> Result<String, String> {
    let (_, config) = get_config_user()?;
    config
        .section(Some(section))
        .and_then(|s| s.get(key))
        .map(|value| value.to_owned())
        .ok_or_else(|| format!("Key not found in the specified section"))
}

#[allow(dead_code)]
fn get_config_defaults() -> Result<(PathBuf, Ini), String> {
    get_config_ini("APP/conf.ini.lock")
}

#[allow(dead_code)]
fn restore_config_defaults() {}

fn get_config_user() -> Result<(PathBuf, Ini), String> {
    get_config_ini("APP/conf.ini")
}

fn get_config_ini(file: &str) -> Result<(PathBuf, Ini), String> {
    let ini_path = get_current_dir()?.join(file);
    // Load the INI file
    Ini::load_from_file(&ini_path)
        .map(|ini| (ini_path, ini))
        .map_err(|e| format!("Failed to load ini file: {}", e))
}

fn get_current_dir() -> Result<PathBuf, String> {
    env::current_dir().map_err(|e| format!("Failed to get current directory: {}", e))
}
