<template>
  <n-grid cols="1" y-gap="5">
    <n-grid-item>
      <n-grid cols="2" style="width: 250px;">
        <n-grid-item>
          <span style="line-height: 34px; margin-left: 12px;">
            {{ label.language }}
          </span>
        </n-grid-item>
        <n-grid-item>
          <n-select
            v-model:value="formValue.language"
            :default-value="formValue.language"
            :options="languageOptions"
            @update:value="languageUpdateValue"
            style="width: 200px;"
          />
        </n-grid-item>
      </n-grid>
    </n-grid-item>
  </n-grid>


  <!-- <pre>{{ JSON.stringify(formValue, null, 2) }} </pre> -->
</template>

<script>
import {NGrid, NGridItem, NSelect} from 'naive-ui'
import { invoke } from "@tauri-apps/api/tauri";
import { confirm } from '@tauri-apps/api/dialog';
import { emit } from '@tauri-apps/api/event'

export default {
  emits: ['lang-updated'],
  components: {NGrid, NGridItem, NSelect},
  data() {
    return {
      formValue: {
        language: '',
      },
      label: {
        language: '',
      },
      languageOptions: this.$Langs.map(lang => ({is: lang.is, content: lang.language.content}))
        .map( item => ({ label: item.content, value: item.is })
      ),
    }
  },
  methods: {
    updateLabelLanuage() {
      this.label.language = this.$store.getters.getLang.language.label + ":"
    },
    async getFormValue() {
      // let iniObject = await readIniFile()
      // this.formValue.language = iniObject.settings.language


      let language = await invoke("read_from_config_user", {section: "settings", key: "language"});
      this.formValue.language = language
      
    },
    async languageUpdateValue(value, option) {
        // console.log(value + ":" + JSON.stringify(option)) // en:{"label":"English","value":"en"}
        const confirmed = await confirm(this.$store.getters.getLang.language.isChange.replace("{}", option.label), 'TodoList');
        if (confirmed) {
          this.$store.commit('setLang', value)
          // vue emit
          // this.$emit('lang-updated', value); // Settings.vue -- 修改tabs
          // tauri
          // rust -- backend emit
          await invoke("change_config_user", {updates: [['settings', 'language', value]]}); // rust设置ini文件
          await invoke("change_windows_language");   // rust设置windows标题和托盘菜单
          // frontend emit
          emit('change_language_frontend', value); // Change.vue -- 修改button // 修改change线的store
                                                    // Settings.vue
          this.updateLabelLanuage()
        }
    }
  },
  async created() {
    // console.log(this.$store.getters.getLang)
    this.getFormValue()
    this.updateLabelLanuage()
  }
}
</script>

<style>
</style>