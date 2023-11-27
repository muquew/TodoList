// import { createStore } from 'vuex'
// import {Langs, chooseLanguageIndex} from '../getLang'
// import readIniFile from '../fileIni'

// let iniObject = await readIniFile();

// const store = createStore({
//   state: {
//     Lang: Langs[chooseLanguageIndex(iniObject.settings.language)]
//   },
//   getters: {
//     getLang: (state) => {
//       return state.Lang;
//     }
//   },
//   mutations: {
//     setLang: (state, newLang) => {
//         state.Lang = Langs[chooseLanguageIndex(newLang)];
//     }
//   }
// })

// export default store


import { createStore } from 'vuex'
import { Langs, chooseLanguageIndex } from '@/utils/getLang'
// import readIniFile from '@/fileIni'
import { invoke } from '@tauri-apps/api'



const store = createStore({
  state: {
    Lang: null
  },
  getters: {
    getLang: (state) => {
      return state.Lang;
    }
  },
  mutations: {
    setLang: (state, newLang) => {
        state.Lang = Langs[chooseLanguageIndex(newLang)];
    }
  },
  actions: {
    async initLang({ commit }) {
      // let iniObject = await readIniFile();
      // commit('setLang', iniObject.settings.language)

      let language = await invoke("read_from_config_user", {section: "settings", key: "language"});
      commit('setLang', language)
    }
  }
})

export default store