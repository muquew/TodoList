// import zh_CN from './locales/zh_CN';
// import zh_TW from './locales/zh_TW';
// import en from './locales/en';

// console.log(window.__TAURI__.fs.readDir("./locales"))
import zh_CN from '@tauri/locales/zh_CN';
import zh_TW from '@tauri/locales/zh_TW';
import en from '@tauri/locales/en';


const Langs = [zh_CN, zh_TW, en];


function chooseLanguageIndex(x) {
    switch (x) {
        case 'zh_CN':
            return 0;
        case 'zh_TW':
            return 1;
        case 'en':
            return 2;
        default:
            return 0;
      }
}


export {Langs, chooseLanguageIndex}
