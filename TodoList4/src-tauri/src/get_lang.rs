use std::fs::File;
use std::io::Read;
use serde_json::{Value, Error};

use crate::file_ini;

fn get_config_language() -> String {
    file_ini::read_from_config_user("settings", "language")
        .unwrap_or_else(|error| {
            eprintln!("Error reading configuration: {}", error);
            // Return a default language or handle the error in another way
            "default_language".to_string()
        })
}


pub(crate) fn get_json_value(key: &str) -> String {
    get_json_value_with_language(key, None)
}
pub(crate) fn get_json_value_with_language(key: &str, lan: Option<&str>) -> String {
// pub(crate) fn get_json_value<'a>(lan: &'a str, key: &str) -> String {
    // Get the current directory
    let current_dir = std::env::current_dir().expect("Unable to get current directory");

    // Determine the language to use
    let language = get_config_language();
    let language_to_use = lan.unwrap_or(&language);

    // Construct the path to the JSON file
    let path = current_dir.join("locales").join(format!("{}.json", language_to_use));
    // println!("{:?}", path);

    // Open the file
    let mut file = File::open(&path).expect("Unable to open file");

    // Read file content into a string
    let mut content = String::new();
    file.read_to_string(&mut content).expect("Unable to read file");

     // Parse JSON
     let parsed_json: Result<Value, Error> = serde_json::from_str(&content);

    // Check if parsing was successful
    match parsed_json {
        Ok(parsed) => {
            // Get the value using nested keys
            let nested_keys: Vec<&str> = key.split('.').collect();
            let mut current_value = &parsed;

            for nested_key in nested_keys {
                current_value = match current_value.get(nested_key) {
                    Some(value) => value,
                    None => {
                        println!("Key '{}' not found in the JSON file", key);
                        return String::new(); // Return an empty string to represent "null"
                    }
                };
            }

            if let Some(name) = current_value.as_str() {
                name.to_string()
            } else {
                println!("Value for key '{}' is not a string in the JSON file", key);
                String::new() // Return an empty string to represent "null"
            }
        }
        Err(err) => {
            println!("Error parsing JSON: {}", err);
            String::new() // Return an empty string to represent "null"
        }
    }
}