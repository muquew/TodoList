import { isRegistered, register, unregisterAll } from '@tauri-apps/api/globalShortcut';
import { invoke } from "@tauri-apps/api/tauri";

// emit an event that is only visible to the current window


const registerShortcut = async function() {
    // const isRegister = await isRegistered('CommandOrControl+P');
    // if(!isRegister) {
    //   await register('CommandOrControl+F1', () => {
    //     console.log('Shortcut triggered');
    //   });
    // } else {
    //   console.log(111)
    // }
    await register('CommandOrControl+F1', async () => {
        await invoke("open_change_window");
    });
}


const unregisterShortcut = async function() {
    await unregisterAll();
}

export {registerShortcut, unregisterShortcut};