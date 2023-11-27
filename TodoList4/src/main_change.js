import { createApp } from "vue";
import "@/styles.css";
import Change from "@/pages/Change.vue";
import { Langs } from '@/utils/getLang.js'
import store from '@/store'
const appChange = createApp(Change);
appChange.config.globalProperties.$Langs = Langs;
appChange.use(store);
store.dispatch('initLang').then(() => {
    appChange.mount("#change");
});
