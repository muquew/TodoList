<template>
  <n-config-provider :theme-overrides="themeOverrides">
    <n-tabs type="card" size="small" class="card" pane-class="detail" :default-value="items[0].name">
      <n-tab-pane v-for="item in items" :key="item.name" :name="item.name" :tab="item.tab">
        <component :is="getComponent(item.name)"></component>
      </n-tab-pane>
    </n-tabs>
  </n-config-provider>
</template>

<script>
import { NTabs, NTabPane, NDivider, NConfigProvider } from 'naive-ui'
import General from '@/components/settings/General.vue'
import Interface from '@/components/settings/Interface.vue'
import Shortcut from '@/components/settings/Shortcut.vue'
import About from '@/components/settings/About.vue'
import Todo from '../components/settings/Todo.vue'
import { listen } from '@tauri-apps/api/event'
/**
 * js 文件下使用这个做类型提示
 * @type import('naive-ui').GlobalThemeOverrides
 */
const themeOverrides = {
  "Tabs": {
    "tabPaddingSmallCard": "0 2px",
    "panePaddingSmall": "3px 0",
    "tabTextColorCard": "rgba(19, 20, 22, 1)",
    "tabTextColorActiveCard": "#000",
    "tabColor": "#F3F3F3",
    "tabBorderColor": "#E5E5E5",
    "tabFontWeight": "400",
    "tabFontWeightActive": "600",
    "tabBorderRadius": "3px",
  }
}
export default {
    components: { NTabs, NTabPane, NDivider, NConfigProvider },
    data() {
      return {
        themeOverrides,
        items: []
      }
    },
    methods: {
      getComponent(name) {
        // Map item.name to corresponding components
        const componentMap = {
          "General": General,
          "Interface": Interface,
          "Shortcut": Shortcut,
          "About": About,
          "Todo": Todo
        }
        return componentMap[name] || null;
      },
      getData() {
        let tabs = this.$store.getters.getLang.settingsTabs;
        let keys = Object.keys(tabs);
        this.items = [];
        keys.forEach(key => {
          this.items.push({
            tab: tabs[key],
            name: key
          })
        });
        /* 
         * todo
        */
      },
      updateData() {
        let length = this.items.length
        let tabs = this.$store.getters.getLang.settingsTabs;
        for (let i=0; i<length; i++) {
          let item = this.items[i]
          item.tab = tabs[item.name]
        }
      }
    },
    async created() {
      // await this.$store.dispatch('initLang');
      // console.log(this.$Lang)
      // console.log(this.$Langs[chooseLanguageIndex(await readIniFile())])
      // this.$store.commit('setLang', )
      this.getData()
      // let x = await readIniFile()
      // console.log(x.settings.language)
      // console.log(this.$Langs[chooseLanguageIndex(x.settings.language)])

      await listen('change_language_frontend', (event) => {
        this.updateData()
      })
    }
}
</script>

<style scoped>
* {
  background-color: #f0f0f0;
}
.card {
  height: 100vh;
  padding: 10px;
}

.detail {
  border: #F9F9F9 solid 3px;
}
</style>

