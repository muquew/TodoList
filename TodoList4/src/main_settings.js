import { createApp } from "vue";
import "@/styles.css";
import Settings from "@/pages/Settings.vue";
import { Langs } from '@/utils/getLang.js'
import store from '@/store'
const appSettings = createApp(Settings);
appSettings.config.globalProperties.$Langs = Langs;
appSettings.use(store);
store.dispatch('initLang').then(() => {
    appSettings.mount("#settings");
});
