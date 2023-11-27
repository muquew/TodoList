// use std::time::{SystemTime, UNIX_EPOCH};
// let item = Item {
//     ischecked: false,
//     content: String::from("Example content"),
//     time: SystemTime::now()
//         .duration_since(UNIX_EPOCH)
//         .expect("Time went backwards")
//         .as_millis() as u64,
// };
// println!("{}", item.time); // Print the timestamp in milliseconds
use serde::{Serialize, Deserialize};

#[derive(Serialize, Deserialize, Debug, Clone)]
pub(crate) struct Item {
    is_check: bool,
    content: String,
    time: Option<u64>, // Representing timestamp in milliseconds or null
}

const PATH: &str = "APP/items.json";

#[tauri::command]
pub(crate) fn set_user_items(data: Vec<Item>) {
    // println!("{:?}", sort_items_by_time(data.clone()));
    write_items(data, PATH)
    // println!("{:?}", sort_items_by_time());
}

#[tauri::command]
pub(crate) fn get_user_items() -> Vec<Item> {
    read_items(PATH)
}


fn write_items(data: Vec<Item>, path: &str) {
    // 将结构体数组序列化为JSON字符串
    let json_string = serde_json::to_string_pretty(&data).unwrap();

    // 将JSON字符串写入文件
    std::fs::write(path, json_string).expect("Unable to write file");
}

fn read_items(path: &str) -> Vec<Item> {
    // 从文件中读取JSON字符串
    let read_json_string = std::fs::read_to_string(path).expect("Unable to read file");

    // 将JSON字符串反序列化为结构体数组
    // let deserialized_items: Vec<Item> = serde_json::from_str(&read_json_string).unwrap();

    // 打印反序列化得到的结构体数组
    // println!("{:?}", deserialized_items);
    serde_json::from_str(&read_json_string).unwrap()
}

// fn sort_items_by_time(items: Vec<Item>) -> Vec<Item> {
//     // 删除 time=None 的项
//     let mut sorted_items: Vec<Item> = items.into_iter()
//         .filter(|item| item.time.is_some())
//         .collect();
//     // 升序排序
//     sorted_items.sort_by(|a, b| {
//         a.time.unwrap_or_default().cmp(&b.time.unwrap_or_default())
//     });
//     sorted_items
// }

#[tauri::command]
pub(crate) fn sort_items_by_time() -> Vec<Item> {
    let items = get_user_items();
    // 删除 time=None 的项
    let mut sorted_items: Vec<Item> = items.into_iter()
        .filter(|item| item.time.is_some())
        .collect();
    // 升序排序
    sorted_items.sort_by(|a, b| {
        a.time.unwrap_or_default().cmp(&b.time.unwrap_or_default())
    });
    sorted_items
}