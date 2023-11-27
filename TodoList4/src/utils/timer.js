// main
import { invoke } from "@tauri-apps/api";
import sendn from "@/utils/notification";


let timerIds = []; // 用于存储所有定时器的ID


async function getItems() {
    let items = await invoke("sort_items_by_time");
    return items
}

async function setTimer() {
    let items = await getItems()
    let now = Date.now()
    items = items.filter(item => item.time >= now);

     // 清除所有之前的定时器
    clearAllTimers();

    function processItem(index) {
        if (index < items.length) {
            let item = items[index];
            let delay = item.time - now;

            let timerId = setTimeout(() => {
                console.log(1);
                sendn(item.content)

                // 继续监视下一个time
                processItem(index + 1);
            }, delay);

             // 存储定时器ID
             timerIds.push(timerId);
        } else {
            clearAllTimers()
        }
    }

    processItem(0)
}


// 清除所有定时器的函数
function clearAllTimers() {
    timerIds.forEach(timerId => clearTimeout(timerId));
    timerIds = []; // 清空数组
}


export {setTimer}